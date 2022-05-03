using System;
using System.Collections.Generic;

public class RoomController
{
    private RoomService roomService;

    public RoomService RoomService { get => roomService; set => roomService = value; }

    public RoomController()
    {
        this.roomService = new RoomService();
    }

    public List<Room> GetAll()
    {
        return this.roomService.GetAll();
    }

    public Room GetById(String id)
    {
        return this.roomService.GetById(id);
    }

    public List<Room> GetFreeRooms(DateTime enteredTime)
    {
        return this.roomService.GetFreeRooms(enteredTime);
    }

    public List<Room> GetRenovatableRooms() 
    {
        return this.roomService.GetRenovatableRooms();
    }

    public void CreateRoom(String name, RoomType type, RoomStatus status, int level, int number, bool free)
    {
        this.roomService.CreateRoom(name, type, status, level, number, free);
    }

    public void UpdateRoom(String roomId, String name, RoomType type, RoomStatus status, int level, int number, bool free)
    {
        this.roomService.UpdateRoom(roomId, name, type, status, level, number, free);
    }

    public void DeleteRoom(String roomId)
    {
        this.roomService.DeleteRoom(roomId);
    }

    public void OccupyRoom(String roomId)
    {
        this.roomService.OccupyRoom(roomId);
    }

    public void FreeRoom(String roomId)
    {
        this.roomService.FreeRoom(roomId);
    }

    public void RenovateRoom(String roomId)
    {
        this.roomService.RenovateRoom(roomId);
    }

}
