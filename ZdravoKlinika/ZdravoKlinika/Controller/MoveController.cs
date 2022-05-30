using System;
using System.Collections.Generic;

public class MoveController
{
    private MoveService moveService;

    public MoveService MoveService { get => moveService; set => moveService = value; }

    public MoveController()
    {
        this.moveService = new MoveService();
    }

    public List<Move> GetAll()
    {
        return this.moveService.GetAll();
    }

    public Move GetById(String id)
    {
        return this.moveService.GetById(id);
    }

    public void CreateMove(Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        this.moveService.CreateMove(new Move("0", sourceRoom, destinationRoom, scheduledDateTime, equipmentToMove));
    }

    public void UpdateMove(String moveId, Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        this.moveService.UpdateMove(new Move(moveId, sourceRoom, destinationRoom, scheduledDateTime, equipmentToMove));
    }

    public void DeleteMove(String moveId)
    {
        this.moveService.DeleteMove(moveId);
    }

}