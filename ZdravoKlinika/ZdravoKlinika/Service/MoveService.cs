
using System;
using System.Collections.Generic;

public class MoveService
{
    private MoveRepository moveRepository;

    public MoveService()
    {
        this.MoveRepository = new MoveRepository();
    }

    public MoveRepository MoveRepository { get => moveRepository; set => moveRepository = value; }

    public List<Move> GetAll()
    {
        return this.moveRepository.GetAll();
    }

    public Move GetById(String id)
    {
        return this.moveRepository.GetById(id);
    }

    public void CreateMove(Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        List<Move> moves = this.moveRepository.GetAll();
        int newMoveId;
        if (moves.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach (Move move in moves)
            {
                trenutniId = Int32.Parse(move.MoveId);
                if (trenutniId > maxId) maxId = trenutniId;
            }

            newMoveId = maxId + 1;
        }
        else
        {
            newMoveId = 1;
        }
        Move m = new Move(newMoveId.ToString(), sourceRoom, destinationRoom, scheduledDateTime, equipmentToMove);
        this.moveRepository.CreateMove(m);
    }

    public void UpdateMove(String moveId, Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        Move m = this.moveRepository.GetById(moveId);
        m.SourceRoom = sourceRoom;
        m.DestinationRoom = destinationRoom;
        m.ScheduledDateTime = scheduledDateTime;
        m.EquipmentToMove = equipmentToMove;
        this.moveRepository.UpdateMove(m);
    }

    public void DeleteMove(String moveId)
    {
        Move m = moveRepository.GetById(moveId);
        this.moveRepository.DeleteMove(m);
    }

}