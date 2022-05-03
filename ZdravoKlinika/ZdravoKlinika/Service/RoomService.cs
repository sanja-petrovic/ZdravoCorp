﻿using System;
using System.Collections.Generic;

public class RoomService
{
    private RoomRepository roomRepository;

    public RoomService()
    {
        this.roomRepository = new RoomRepository();
    }

    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }

    public List<Room> GetAll()
    {
        return this.roomRepository.GetAll();
    }

    public Room GetById(String id)
    {
        return this.roomRepository.GetById(id);
    }

    public List<Room> GetFreeRooms(DateTime enteredTime)
    {
        return this.roomRepository.GetFreeRooms(enteredTime);
    }

    public List<Room> GetRenovatableRooms()
    {
        return this.roomRepository.GetRenovatableRooms();
    }

    public void CreateRoom(String name, RoomType type, RoomStatus status, int level, int number, bool free)
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
        Room r = new Room(newRoomId.ToString(), "R"+newRoomId, type, level, number, status, free);
        this.roomRepository.CreateRoom(r);
    }

    public void UpdateRoom(String roomId, String name, RoomType type, RoomStatus status, int level, int number, bool free)
    {
        Room r = this.roomRepository.GetById(roomId);
        r.Name = name;
        r.Type = type;
        r.Status = status;
        r.Level = level;
        r.Number = number;
        r.Free = free;
        this.roomRepository.UpdateRoom(r);
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

}

