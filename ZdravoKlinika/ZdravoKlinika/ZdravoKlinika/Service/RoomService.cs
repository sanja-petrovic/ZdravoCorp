using System;
using System.Collections.Generic;

public class RoomService
{
    private RoomRepository roomRepository;

    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }

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