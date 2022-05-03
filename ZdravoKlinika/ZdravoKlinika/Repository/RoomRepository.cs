using System;
using System.Collections.Generic;
using System.IO;

public class RoomRepository
{
    private RoomDataHandler roomDataHandler;
    private List<Room> rooms;
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "room.json";

    public RoomRepository()
    {
        this.roomDataHandler = new RoomDataHandler(fileLocation);
        this.rooms = this.roomDataHandler.Read();
    }

    public List<Room> Room
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
            if (this.rooms.Contains(room))
                this.rooms.Remove(room);
        this.CreateRoom(room);
    }

}
