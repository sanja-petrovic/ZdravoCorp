using System;
using System.Collections.Generic;
using System.IO;

public class RoomRepository
{
    private RoomDataHandler roomDataHandler;
    private AppointmentRepository appointmentRepository;
    private List<Room> rooms;
    private List<Room> freeRooms;
    private List<Appointment> appointments;
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "room.json";

    public RoomRepository()
    {
        this.roomDataHandler = new RoomDataHandler(fileLocation);
        this.rooms = this.roomDataHandler.Read();
        this.freeRooms = new List<Room>();
        this.appointmentRepository = new AppointmentRepository();
        this.appointments = appointmentRepository.GetAll();
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

    public RoomDataHandler RoomDataHandler { get => roomDataHandler; set => roomDataHandler = value; }

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

    public List<Room> GetFreeRooms(DateTime enteredTime)
    {
        DateTime appointmentStart;
        DateTime appointmentEnd;

        foreach (Appointment app in appointments)
        {
            appointmentStart = app.DateAndTime;
            appointmentEnd = appointmentStart.AddMinutes(app.Duration);
            if ((enteredTime > appointmentStart) && (enteredTime < appointmentEnd))
            {
                //app.Room JE ZAUZETA U NAVEDENOM TERMINU
                app.Room.Free = false;
            }
            else
            {
                //app.Room JE SLOBODNA U NAVEDENOM TERMINU
                app.Room.Free = true;
            }
        }

        foreach (Room r in rooms)
        {
            if (r.Free)
            {
                freeRooms.Add(r);
            }
        }

        return freeRooms;

        /* STARI KOD
        foreach (Room r in this.rooms)
        {
            if(r.Status == RoomStatus.available)
            {
                freeRooms.Add(r);
            }
        }
        return freeRooms;*/
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
