
using System;
using System.Collections.Generic;
using System.IO;

public class MoveRepository
{
    private MoveDataHandler moveDataHandler;
    private List<Move> moves;
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "move.json";

    public MoveRepository()
    {
        this.MoveDataHandler = new MoveDataHandler(fileLocation);
        this.moves = this.MoveDataHandler.Read();
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

    public MoveDataHandler MoveDataHandler { get => moveDataHandler; set => moveDataHandler = value; }

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
        foreach (Move m in this.moves)
        {
            if (m.MoveId.Equals(id))
            {
                return m;
            }
        }

        return null;
    }

    public void CreateMove(Move move)
    {
        this.moves.Add(move);
        moveDataHandler.Write(this.moves);
    }

    public void DeleteMove(Move move)
    {
        if (move == null)
            return;
        if (this.moves != null)
            if (this.moves.Contains(move))
                this.moves.Remove(move);
        moveDataHandler.Write(this.moves);
    }

    public void UpdateMove(Move move)
    {
        if (move == null)
            return;
        if (this.moves != null)
            foreach (Move m in this.moves)
            {
                if (m.MoveId.Equals(move.MoveId))
                {
                    m.SourceRoom = move.SourceRoom;
                    m.DestinationRoom = move.DestinationRoom;
                    m.ScheduledDateTime = move.ScheduledDateTime;
                }
            }
        moveDataHandler.Write(this.moves);
    }

}