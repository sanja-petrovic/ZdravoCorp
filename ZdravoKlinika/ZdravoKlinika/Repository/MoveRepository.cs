
using System;
using System.Collections.Generic;
using System.IO;

public class MoveRepository
{
    private MoveDataHandler moveDataHandler;
    private List<Move> moves;

    public MoveDataHandler MoveDataHandler { get => moveDataHandler; set => moveDataHandler = value; }

    public MoveRepository()
    {
        this.MoveDataHandler = new MoveDataHandler();
        this.moves = this.moveDataHandler.Read();
    }

    public List<Move> Moves
    {
        get
        {
            if (moves == null)
                moves = new List<Move>();
            return moves;
        }
        set
        {
            RemoveAllMove();
            if (value != null)
            {
                foreach (Move oMove in value)
                    AddMove(oMove);
            }
        }
    }

    public void AddMove(Move newMove)
    {
        if (newMove == null)
            return;
        if (this.moves == null)
            this.moves = new List<Move>();
        if (!this.moves.Contains(newMove))
            this.moves.Add(newMove);
    }

    public void RemoveMove(Move oldMove)
    {
        if (oldMove == null)
            return;
        if (this.moves != null)
            if (this.moves.Contains(oldMove))
                this.moves.Remove(oldMove);
    }

    public void RemoveAllMove()
    {
        if (moves != null)
            moves.Clear();
    }

    public List<Move> GetAll()
    {
        return this.moves;
    }

    public Move GetById(String id)
    {
        Move? returnValue = null;
        foreach (Move m in this.moves)
        {
            if (m.MoveId.Equals(id))
            {
                returnValue = m;
            }
        }

        return returnValue;
    }

    public void Add(Move move)
    {
        this.moves.Add(move);
        moveDataHandler.Write(this.moves);
    }

    public void Remove(Move move)
    {
        if (move == null)
            return;
        if (this.moves != null)
            if (this.moves.Contains(move))
                this.moves.Remove(move);
        moveDataHandler.Write(this.moves);
    }

    public void Update(Move move)
    {
        if (move == null)
            return;
        if (this.moves != null)
            foreach (Move m in this.moves)
            {
                if (m.MoveId.Equals(move.MoveId))
                {
                    UpdateMoveValues(m, move);
                }
            }
        moveDataHandler.Write(this.moves);
    }

    private void UpdateMoveValues(Move moveToBeUpdated, Move updatedValues)
    {
        moveToBeUpdated.SourceRoom = updatedValues.SourceRoom;
        moveToBeUpdated.DestinationRoom = updatedValues.DestinationRoom;
        moveToBeUpdated.ScheduledDateTime = updatedValues.ScheduledDateTime;
    }

}