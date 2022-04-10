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

    private List<Equipment> equipmentInRoom;

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

}