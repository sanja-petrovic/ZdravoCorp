﻿using System;
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
                Finalize(r);         
            }
            this.renovationDataHandler.Write(this.renovations);
        }

    }

    private void Finalize(Renovation r)
    {
        if (!r.IsRenovationFinished)
        {
            if (r.ScheduledDateTime <= DateTime.Now)
            {
                if (r.EntryRooms.Count == 1 && r.NumberOfExitRooms == 1)
                {
                    //REGULAR RENOVATION
                    FinalizeRenovation(r);
                }
                else if (r.EntryRooms.Count == 1 && r.NumberOfExitRooms > 1)
                {
                    //SPLITTING ONE ROOM INTO MULTIPLE
                    FinalizeSplit(r);
                }
                else if (r.EntryRooms.Count > 1 && r.NumberOfExitRooms == 1)
                {
                    //MERGING MULTIPLE ROOMS INTO ONE
                    FinalizeMerge(r);                 
                }
            }
        }
    }

    private void FinalizeRenovation(Renovation r)
    {
        foreach (Room room in rooms)
        {
            if (room.RoomId.Equals(r.EntryRooms[0].RoomId))
            {
                this.FreeRoom(this.GetById(room.RoomId));
            }
        }
        r.IsRenovationFinished = true;
    }

    private void FinalizeSplit(Renovation r)
    {
        this.Remove(this.GetById(r.EntryRooms[0].RoomId));
        for (int i = 0; i < r.NumberOfExitRooms; i++)
        {
            Room r1 = new Room(GenerateRoomId().ToString(), "R" + GenerateRoomId(), RoomType.checkup, 1, 1, RoomStatus.available, true);
            this.Add(r1);
        }
        r.IsRenovationFinished = true;
    }

    private void FinalizeMerge(Renovation r)
    {
        foreach (Room room in r.EntryRooms)
        {
            this.Remove(this.GetById(room.RoomId));
        }
        Room r1 = new Room(GenerateRoomId().ToString(), "R" + GenerateRoomId(), RoomType.checkup, 1, 1, RoomStatus.available, true);
        this.Add(r1);
        r.IsRenovationFinished = true;
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

        foreach (Appointment app in appointments)
        {
            FindAndMarkFreeRooms(app, enteredTime);       
        }

        foreach (Room r in rooms)
        {
            FillFreeRoomsList(r, roomType);
        }

        return freeRooms;
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

    public void Add(Room room)
    {
        this.rooms.Add(room);
        roomDataHandler.Write(this.rooms);
    }

    public void Remove(Room room)
    {
        if (room == null)
            return;
        if (this.rooms != null)
            if (this.rooms.Contains(room))
                this.rooms.Remove(room);
        roomDataHandler.Write(this.rooms);
    }

    public void Update(Room room)
    {
        if (room == null)
            return;
        if (this.rooms != null)
            foreach (Room r in this.rooms)
            {
                if (r.RoomId.Equals(room.RoomId))
                {
                    UpdateRoomValues(r, room);
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
        Update(room);
    }

    public void FreeRoom(Room room)
    {
        if (room == null)
            return;
        room.Status = RoomStatus.available;
        room.Free = true;
        Update(room);
    }

    public void RenovateRoom(Room room)
    {
        if (room == null)
            return;
        room.Status = RoomStatus.renovation;
        room.Free = false;
        Update(room);
    }

    private int GenerateRoomId()
    {
        List<Room> rooms = this.GetAll();
        int newRoomId;
        if (rooms.Count > 0)
        {
            int maxId = 0;
            int trenutniId;
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

    private void UpdateRoomValues(Room roomToBeUpdated, Room updatingValues)
    {
        roomToBeUpdated.Name = updatingValues.Name;
        roomToBeUpdated.Type = updatingValues.Type;
        roomToBeUpdated.Level = updatingValues.Level;
        roomToBeUpdated.Number = updatingValues.Number;
        roomToBeUpdated.Status = updatingValues.Status;
        roomToBeUpdated.Free = updatingValues.Free;
        roomToBeUpdated.EquipmentInRoom = updatingValues.EquipmentInRoom;
    }

    //Finds and marks whether or not the Room is Available (checks if enteredTime is in interval [appointmentStart, appointmentStart+Duration])
    private void FindAndMarkFreeRooms(Appointment app, DateTime enteredTime)
    {
        DateTime appointmentStart = app.DateAndTime;
        DateTime appointmentEnd = appointmentStart.AddMinutes(app.Duration);
        Room? room = this.rooms.Find(r => r.RoomId.Equals(app.Room.RoomId));

        if (room != null)
        {
            if ((enteredTime.TimeOfDay >= appointmentStart.TimeOfDay) && (enteredTime.TimeOfDay < appointmentEnd.TimeOfDay))
            {
                //app.Room IS UNAVAILABLE IN THIS BLOCK OF CODE
                room.Free = false;

            }
            else
            {
                //app.Room IS AVAILABLE IN THIS BLOCK OF CODE
                room.Free = true;
            }
        }
    }

    private void FillFreeRoomsList(Room r, RoomType roomType)
    {
        if (r.Free && r.Type.Equals(roomType))
        {
            freeRooms.Add(r);
        }
    }
}
