using System;
using System.Collections.Generic;
using System.IO;

public class RoomRepository
{
    private RoomDataHandler roomDataHandler;
    private RenovationDataHandler renovationDataHandler;
    private List<Room> rooms;
    private List<Room> freeRooms;
    private List<Room> renovatableRooms;
    private RenovationRepository renovationRepository;
    private List<Renovation> renovations;
    
    public RoomDataHandler RoomDataHandler { get => roomDataHandler; set => roomDataHandler = value; }

    public RoomRepository()
    {
        this.roomDataHandler = new RoomDataHandler();
        this.rooms = this.roomDataHandler.Read();

        this.freeRooms = new List<Room>();    
        this.renovatableRooms = new List<Room>();

        this.renovationRepository = new RenovationRepository();
        this.renovationDataHandler = new RenovationDataHandler();
        this.renovations = this.renovationRepository.GetAll();

        UpdateFinishedRenovations();
    }

    private void UpdateFinishedRenovations()
    {
        if (this.renovations != null)
        {
            foreach (Renovation r in this.renovations)
            {
                if (r.IsRenovationFinished)
                {
                    continue;
                }
                if (r.ScheduledDateTime <= DateTime.Now)
                {
                    if (r.EntryRooms.Count == 1 && r.NumberOfExitRooms == 1)
                    {
                        //U PITANJU JE OBICNO RENOVIRANJE
                        foreach (Room room in rooms)
                        {
                            if (room.RoomId.Equals(r.EntryRooms[0].RoomId))
                            {
                                this.FreeRoom(this.GetById(room.RoomId));
                            }
                        }

                    }
                    else if (r.EntryRooms.Count == 1 && r.NumberOfExitRooms > 1)
                    {
                        //U PITANJU JE DELJENJE PROSTORIJE NA VISE MANJIH
                        this.DeleteRoom(this.GetById(r.EntryRooms[0].RoomId));
                        for (int i = 0; i < r.NumberOfExitRooms; i++)
                        {
                            Room r1 = new Room(GenerateId().ToString(), "R" + GenerateId(), RoomType.checkup, 1, 1, RoomStatus.available, true);
                            this.CreateRoom(r1);
                        }

                    }
                    else if (r.EntryRooms.Count > 1 && r.NumberOfExitRooms == 1)
                    {
                        //U PITANJU JE SPAJANJE PROSTORIJA U JEDNU
                        foreach (Room room in r.EntryRooms)
                        {
                            this.DeleteRoom(this.GetById(room.RoomId));
                        }
                        Room r1 = new Room(GenerateId().ToString(), "R" + GenerateId(), RoomType.checkup, 1, 1, RoomStatus.available, true);
                        this.CreateRoom(r1);

                    }

                    r.IsRenovationFinished = true;
                }
            }
            this.renovationDataHandler.Write(this.renovations);
        }

    }

    public int GenerateId()
    {
        List<Room> rooms = this.GetAll();
        int newRoomId;
        if (rooms.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach (Room room in rooms)
            {
                trenutniId = Int32.Parse(room.RoomId);
                if (trenutniId > maxId) maxId = trenutniId;
            }

            newRoomId = maxId + 1;
        }
        else
        {
            newRoomId = 1;
        }
        return newRoomId;
    }

    public List<Room> Rooms
    {
        get
        {
            if (rooms == null)
                rooms = new List<Room>();
            return rooms;
        }
        set
        {
            RemoveAllRoom();
            if (value != null)
            {
                foreach (Room oRoom in value)
                    AddRoom(oRoom);
            }
        }
    }
   
    public void AddRoom(Room newRoom)
    {
        if (newRoom == null)
            return;
        if (this.rooms == null)
            this.rooms = new List<Room>();
        if (!this.rooms.Contains(newRoom))
            this.rooms.Add(newRoom);
    }

    public void RemoveRoom(Room oldRoom)
    {
        if (oldRoom == null)
            return;
        if (this.rooms != null)
            if (this.rooms.Contains(oldRoom))
                this.rooms.Remove(oldRoom);
    }

    public void RemoveAllRoom()
    {
        if (rooms != null)
            rooms.Clear();
    }

    public List<Room> GetAll()
    {
        return this.rooms;
    }

    public Room GetById(String id)
    {
        foreach (Room r in this.rooms)
        {
            if (r.RoomId.Equals(id))
            {
                return r;
            }
        }

        return null;
    }
    public List<Room> GetFreeRooms(DateTime enteredTime, RoomType roomType)
    {
        freeRooms.Clear();
        AppointmentRepository appointmentRepository = new AppointmentRepository();
        List<Appointment> appointments = appointmentRepository.GetFutureAppointments();
        Room? room = null;

        DateTime appointmentStart;
        DateTime appointmentEnd;

        foreach (Appointment app in appointments)
        {
            appointmentStart = app.DateAndTime;
            appointmentEnd = appointmentStart.AddMinutes(app.Duration);
            room = this.rooms.Find(r => r.RoomId.Equals(app.Room.RoomId));

            if (room != null)
            {
                if ((enteredTime > appointmentStart) && (enteredTime < appointmentEnd))
                {
                    //app.Room JE ZAUZETA U NAVEDENOM TERMINU
                    room.Free = false;
                }
                else
                {
                    //app.Room JE SLOBODNA U NAVEDENOM TERMINU
                    room.Free = true;
                }
            }
                
        }

        foreach (Room r in rooms)
        {
            if (r.Free && r.Type.Equals(roomType))
            {
                freeRooms.Add(r);
            }
        }

        return freeRooms;
    }

    public List<Room> GetRenovatableRooms()
    {
        renovatableRooms.Clear();
        foreach (Room r in rooms)
        {
            if(r.Status == RoomStatus.occupied || r.Status == RoomStatus.available)
            {
                renovatableRooms.Add(r);
            }
        }
        return renovatableRooms;
    }

    public void CreateRoom(Room room)
    {
        this.rooms.Add(room);
        roomDataHandler.Write(this.rooms);
    }

    public void DeleteRoom(Room room)
    {
        if (room == null)
            return;
        if (this.rooms != null)
            if (this.rooms.Contains(room))
                this.rooms.Remove(room);
        roomDataHandler.Write(this.rooms);
    }

    public void UpdateRoom(Room room)
    {
        if (room == null)
            return;
        if (this.rooms != null)
            foreach (Room r in this.rooms)
            {
                if (r.RoomId.Equals(room.RoomId))
                {
                    r.Name = room.Name;
                    r.Type = room.Type;
                    r.Level = room.Level;
                    r.Number = room.Number;
                    r.Status = room.Status;
                    r.Free = room.Free;
                    r.EquipmentInRoom = room.EquipmentInRoom;
                }
            }
        roomDataHandler.Write(this.rooms);
    }

    public void OccupyRoom(Room room)
    {
        if (room == null)
            return;
        room.Status = RoomStatus.occupied;
        room.Free = false;
        UpdateRoom(room);
    }

    public void FreeRoom(Room room)
    {
        if (room == null)
            return;
        room.Status = RoomStatus.available;
        room.Free = true;
        UpdateRoom(room);
    }

    public void RenovateRoom(Room room)
    {
        if (room == null)
            return;
        room.Status = RoomStatus.renovation;
        room.Free = false;
        UpdateRoom(room);
    }
}
