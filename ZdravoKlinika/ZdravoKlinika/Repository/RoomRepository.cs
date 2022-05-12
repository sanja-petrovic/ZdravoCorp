using System;
using System.Collections.Generic;
using System.IO;

public class RoomRepository
{
    private RoomDataHandler roomDataHandler;
    private List<Room> rooms;
    private List<Room> freeRooms;
    private List<Room> renovatableRooms;
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "room.json";

    public RoomDataHandler RoomDataHandler { get => roomDataHandler; set => roomDataHandler = value; }

    public RoomRepository()
    {
        this.roomDataHandler = new RoomDataHandler(fileLocation);
        this.rooms = this.roomDataHandler.Read();
        this.freeRooms = new List<Room>();    
        this.renovatableRooms = new List<Room>();
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
        List<Appointment> appointments = appointmentRepository.GetAppointmentsOnDate(enteredTime);
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
                if ((enteredTime.TimeOfDay >= appointmentStart.TimeOfDay) && (enteredTime.TimeOfDay < appointmentEnd.TimeOfDay))
                {
                    //app.Room JE ZAUZETA U NAVEDENOM TERMINU
                    room.Free = false;
                    
                }
                else
                {
                    //app.Room JE SLOBODNA U NAVEDENOM TERMINU
                    room.Free = true;
                }
                if(room.Free == false)
                {
                    break;
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

    public List<Room> GetOccupiedRooms(DateTime dateAndTime, RoomType type)
    {
        List<Room> rooms = new List<Room>();

        AppointmentRepository appointmentRepository = new AppointmentRepository();

        return rooms;
    }

    public RoomType GetRoomTypeForAppointmentType(AppointmentType type)
    {
        RoomType result;
        if(type == AppointmentType.Regular)
        {
            result = RoomType.checkup;
        } else
        {
            result = RoomType.operating;
        }

        return result;
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
