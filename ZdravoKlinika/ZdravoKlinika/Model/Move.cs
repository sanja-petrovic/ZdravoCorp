
using System;
using System.Collections.Generic;

public class Move
{
    private String moveId;
    private Room sourceRoom;
    private Room destinationRoom;
    private DateTime scheduledDateTime;

    private List<Equipment> equipmentToMove;

    public Move(string moveId, Room sourceRoom, Room destinationRoom, DateTime scheduledDateTime, List<Equipment> equipmentToMove)
    {
        this.moveId = moveId;
        this.sourceRoom = sourceRoom;
        this.destinationRoom = destinationRoom;
        this.scheduledDateTime = scheduledDateTime;
        this.equipmentToMove = equipmentToMove;
    }

    public Move()
    {
    }

    public List<Equipment> EquipmentToMove
    {
        get
        {
            if (equipmentToMove == null)
                equipmentToMove = new List<Equipment>();
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

    public string MoveId { get => moveId; set => moveId = value; }
    public Room SourceRoom { get => sourceRoom; set => sourceRoom = value; }
    public Room DestinationRoom { get => destinationRoom; set => destinationRoom = value; }
    public DateTime ScheduledDateTime { get => scheduledDateTime; set => scheduledDateTime = value; }

    public void AddEquipmentToMove(Equipment newEquipment)
    {
        if (newEquipment == null)
            return;
        if (this.equipmentToMove == null)
            this.equipmentToMove = new List<Equipment>();
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

    public static Move Parse(String id)
    {
        Move move = new Move();
        move.moveId = id;
        return move;
    }

}