using System;
using System.Collections.Generic;

public class RoomController
{
    private RoomService roomService;

    public RoomService RoomService { get => roomService; set => roomService = value; }

    public List<Room> GetAll()
    {
        throw new NotImplementedException();
    }

    public Room GetById(String id)
    {
        throw new NotImplementedException();
    }

    public void CreateRoom(String roomId, String name, RoomType type, RoomStatus status, int level, int number)
    {
        throw new NotImplementedException();
    }

    public void UpdateRoom(String roomId, String name, RoomType type, RoomStatus status, int level, int number)
    {
        throw new NotImplementedException();
    }

    public void DeleteRoom(String roomId)
    {
        throw new NotImplementedException();
    }

}