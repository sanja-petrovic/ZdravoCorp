using System;
using System.Collections.Generic;

public class Room
{
    private String roomId;
    private RoomType type;
    private String name;
    private int level;
    private int number;
    private RoomStatus status;
    private bool free;

    private List<Equipment> equipmentInRoom;

    public Room(String roomId, String name, RoomType type, int level, int number, RoomStatus status, bool free)
    {
        this.roomId = roomId;
        this.name = name;
        this.type = type;
        this.level = level;
        this.number = number;
        this.status = status;
        this.free = free;
    }

    public Room()
    {
    }

    public List<Equipment> EquipmentInRoom
    {
        get
        {
            if (equipmentInRoom == null)
                equipmentInRoom = new List<Equipment>();
            return equipmentInRoom;
        }
        set
        {
            RemoveAllEquipmentInRoom();
            if (value != null)
            {
                foreach (Equipment oEquipment in value)
                    AddEquipmentInRoom(oEquipment);
            }
        }
    }

    public string RoomId { get => roomId; set => roomId = value; }
    public RoomType Type { get => type; set => type = value; }
    public string Name { get => name; set => name = value; }
    public int Level { get => level; set => level = value; }
    public int Number { get => number; set => number = value; }
    public RoomStatus Status { get => status; set => status = value; }
    public bool Free { get => free; set => free = value; }

    public void AddEquipmentInRoom(Equipment newEquipment)
    {
        if (newEquipment == null)
            return;
        if (this.equipmentInRoom == null)
            this.equipmentInRoom = new List<Equipment>();
        if (!this.equipmentInRoom.Contains(newEquipment))
            this.equipmentInRoom.Add(newEquipment);
    }

    public void RemoveEquipmentInRoom(Equipment oldEquipment)
    {
        if (oldEquipment == null)
            return;
        if (this.equipmentInRoom != null)
            if (this.equipmentInRoom.Contains(oldEquipment))
                this.equipmentInRoom.Remove(oldEquipment);
    }

    public void RemoveAllEquipmentInRoom()
    {
        if (equipmentInRoom != null)
            equipmentInRoom.Clear();
    }

    public static Room Parse(String id)
    {
        Room room = new Room();
        room.roomId = id;
        return room;
    }

}