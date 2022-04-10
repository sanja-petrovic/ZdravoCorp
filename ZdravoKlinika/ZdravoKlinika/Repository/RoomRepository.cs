using System;
using System.Collections.Generic;

public class RoomRepository
{
    private RoomDataHandler roomDataHandler;
    private List<Room> room;

    public List<Room> Room
    {
        get
        {
            if (room == null)
                room = new List<Room>();
            return room;
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
        if (this.room == null)
            this.room = new System.Collections.Generic.List<Room>();
        if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
    }

    public void RemoveRoom(Room oldRoom)
    {
        if (oldRoom == null)
            return;
        if (this.room != null)
            if (this.room.Contains(oldRoom))
                this.room.Remove(oldRoom);
    }

    public void RemoveAllRoom()
    {
        if (room != null)
            room.Clear();
    }

    public List<Room> GetAll()
    {
        throw new NotImplementedException();
    }

    public Room GetById(String id)
    {
        throw new NotImplementedException();
    }

    public void CreateRoom(Room room)
    {
        throw new NotImplementedException();
    }

    public void DeleteRoom(Room room)
    {
        throw new NotImplementedException();
    }

    public void UpdateRoom(Room room)
    {
        throw new NotImplementedException();
    }

}