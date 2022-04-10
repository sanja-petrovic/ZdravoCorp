using System;
using System.Collections.Generic;

public class Room
{
    private String roomId;
    private String type;
    private String name;

    private List<Equipment> equipment;
    public string RoomId { get => roomId; set => roomId = value; }
    public string Type { get => type; set => type = value; }
    public string Name { get => name; set => name = value; }
    public List<Equipment> Equipment
    {
        get
        {
            if (equipment == null)
                equipment = new List<Equipment>();
            return equipment;
        }
        set
        {
            RemoveAllEquipment();
            if (value != null)
            {
                foreach (Equipment oEquipment in value)
                    AddEquipment(oEquipment);
            }
        }
    }



    public void AddEquipment(Equipment newEquipment)
    {
        if (newEquipment == null)
            return;
        if (this.equipment == null)
            this.equipment = new List<Equipment>();
        if (!this.equipment.Contains(newEquipment))
            this.equipment.Add(newEquipment);
    }
    public void RemoveEquipment(Equipment oldEquipment)
    {
        if (oldEquipment == null)
            return;
        if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
                this.equipment.Remove(oldEquipment);
    }
    public void RemoveAllEquipment()
    {
        if (equipment != null)
            equipment.Clear();
    }

}