
using System;
using System.Collections.Generic;

public class MoveService
{
    private MoveRepository moveRepository;

    public List<Move> GetAll()
    {
        throw new NotImplementedException();
    }

    public Move GetById(String id)
    {
        throw new NotImplementedException();
    }

    public void CreateMove(Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        throw new NotImplementedException();
    }

    public void UpdateMove(String moveId, Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        throw new NotImplementedException();
    }

    public void DeleteMove(String roomId)
    {
        throw new NotImplementedException();
    }

}