
using System;
using System.Collections.Generic;

public class MoveRepository
{
    private MoveDataHandler moveDataHandler;

    public List<Move> GetAll()
    {
        throw new NotImplementedException();
    }

    public Move GetById(String id)
    {
        throw new NotImplementedException();
    }

    public void CreateMove(Move move)
    {
        throw new NotImplementedException();
    }

    public void DeleteMove(Move move)
    {
        throw new NotImplementedException();
    }

    public void UpdateMove(Move move)
    {
        throw new NotImplementedException();
    }

    public System.Collections.Generic.List<Move> move;

    public System.Collections.Generic.List<Move> Move
    {
        get
        {
            if (move == null)
                move = new System.Collections.Generic.List<Move>();
            return move;
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
        if (this.move == null)
            this.move = new System.Collections.Generic.List<Move>();
        if (!this.move.Contains(newMove))
            this.move.Add(newMove);
    }

    public void RemoveMove(Move oldMove)
    {
        if (oldMove == null)
            return;
        if (this.move != null)
            if (this.move.Contains(oldMove))
                this.move.Remove(oldMove);
    }

    public void RemoveAllMove()
    {
        if (move != null)
            move.Clear();
    }

}