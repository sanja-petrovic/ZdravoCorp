using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKlinika.Util;

public class RoomService
{
    private RoomRepository roomRepository;
    private AppointmentRepository appointmentRepository;

    public RoomService()
    {
        this.roomRepository = new RoomRepository();
        this.appointmentRepository = new AppointmentRepository();
    }

    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }
    public AppointmentRepository AppointmentRepository { get => appointmentRepository; set => appointmentRepository = value; }

    public List<Room> GetAll()
    {
        return this.roomRepository.GetAll();
    }

    public Room GetById(String id)
    {
        return this.roomRepository.GetById(id);
    }

    public List<Room> GetFreeRooms(DateTime enteredTime, RoomType roomType)
    {
        return this.roomRepository.GetFreeRooms(enteredTime, roomType);
    }

    public List<Room> GetRenovatableRooms()
    {
        return this.roomRepository.GetRenovatableRooms();
    }


    public List<Room> GetAvailableRooms(DateBlock period, RoomType roomType)
    {
        List<Room> rooms = this.GetAll().FindAll(r => r.Type.Equals(roomType));

        foreach (Appointment appointment in AppointmentRepository.GetAppointmentsOnDate(period.Start.Date).OrderBy(o => o.DateAndTime).ToList())
        {
            DateBlock apptDateBlock = new DateBlock(appointment.DateAndTime, appointment.DateAndTime.AddMinutes(appointment.Duration));
            if (DateBlock.DateBlocksIntersect(period, apptDateBlock))
            {
                Room r = rooms.Find(x => x.RoomId.Equals(appointment.Room.RoomId));
                if (r != null)
                    rooms.Remove(r);
            }
        }

        return rooms;
    }

    public void CreateRoom(Room room)
    {
        this.roomRepository.CreateRoom(new Room(GenerateRoomId().ToString(), "R"+GenerateRoomId().ToString(), room.Type, room.Level, room.Number, room.Status, room.Free));
    }

    public void UpdateRoom(Room changedRoom)
    {
        Room room = this.roomRepository.GetById(changedRoom.RoomId);
        UpdateRoomValues(room, changedRoom);
        this.roomRepository.UpdateRoom(room);
    }

    public void UpdateRoom(Room changedRoom, List<Equipment> equipmentInRoom)
    {
        Room room = this.roomRepository.GetById(changedRoom.RoomId);
        UpdateRoomValues(room, changedRoom);
        room.EquipmentInRoom = equipmentInRoom;
        this.roomRepository.UpdateRoom(room);
    }

    public void DeleteRoom(String roomId)
    {
        Room r = this.roomRepository.GetById(roomId);
        this.roomRepository.DeleteRoom(r);
    }

    public void OccupyRoom(String roomId)
    {
        Room r = this.roomRepository.GetById(roomId);
        this.roomRepository.OccupyRoom(r);
    }

    public void FreeRoom(String roomId)
    {
        Room r = this.roomRepository.GetById(roomId);
        this.roomRepository.FreeRoom(r);
    }

    public void RenovateRoom(String roomId)
    {
        Room r = this.roomRepository.GetById(roomId);
        this.roomRepository.RenovateRoom(r);
    }
    private void UpdateRoomValues(Room roomToBeUpdated, Room updatingValues)
    {
        roomToBeUpdated.Name = updatingValues.Name;
        roomToBeUpdated.Type = updatingValues.Type;
        roomToBeUpdated.Status = updatingValues.Status;
        roomToBeUpdated.Level = updatingValues.Level;
        roomToBeUpdated.Number = updatingValues.Number;
        roomToBeUpdated.Free = updatingValues.Free;
    }

    private int GenerateRoomId()
    {
        List<Room> rooms = this.roomRepository.GetAll();
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
}

