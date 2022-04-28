
using System;

public class Move
{
    private String moveId;
    private Room sourceRoom;
    private Room destinationRoom;
    private DateTime scheduledDateTime;

    private System.Collections.Generic.List<Equipment> equipmentToMove;

    public System.Collections.Generic.List<Equipment> EquipmentToMove
    {
        get
        {
            if (equipmentToMove == null)
                equipmentToMove = new System.Collections.Generic.List<Equipment>();
            return equipmentToMove;
        }
        set
        {
            RemoveAllEquipmentToMove();
            if (value != null)
            {
                foreach (Equipment oEquipment in value)
                    AddEquipmentToMove(oEquipment);
            }
        }
    }

    public void AddEquipmentToMove(Equipment newEquipment)
    {
        if (newEquipment == null)
            return;
        if (this.equipmentToMove == null)
            this.equipmentToMove = new System.Collections.Generic.List<Equipment>();
        if (!this.equipmentToMove.Contains(newEquipment))
            this.equipmentToMove.Add(newEquipment);
    }

    public void RemoveEquipmentToMove(Equipment oldEquipment)
    {
        if (oldEquipment == null)
            return;
        if (this.equipmentToMove != null)
            if (this.equipmentToMove.Contains(oldEquipment))
                this.equipmentToMove.Remove(oldEquipment);
    }

    public void RemoveAllEquipmentToMove()
    {
        if (equipmentToMove != null)
            equipmentToMove.Clear();
    }

}