using System;
using System.Collections.Generic;

public class Renovation
{
    private String id;
    private int numberOfExitRooms;
    private DateTime scheduledDateTime;
    private bool isRenovationFinished;

    private List<Room> entryRooms;

    public Renovation(string id, int numberOfExitRooms, DateTime scheduledDateTime, List<Room> entryRooms, bool isRenovationFinished)
    {
        this.id = id;
        this.numberOfExitRooms = numberOfExitRooms;
        this.scheduledDateTime = scheduledDateTime;
        this.EntryRooms = entryRooms;
        this.isRenovationFinished = isRenovationFinished;
    }

    public Renovation()
    {
    }

    public string Id { get => id; set => id = value; }
    public int NumberOfExitRooms { get => numberOfExitRooms; set => numberOfExitRooms = value; }
    public DateTime ScheduledDateTime { get => scheduledDateTime; set => scheduledDateTime = value; }

    public bool IsRenovationFinished { get => isRenovationFinished; set => isRenovationFinished = value; }

    public List<Room> EntryRooms { get => entryRooms; set => entryRooms = value; }

    public void AddEntryRooms(Room newRoom)
    {
        if (newRoom == null)
            return;
        if (this.EntryRooms == null)
            this.EntryRooms = new List<Room>();
        if (!this.EntryRooms.Contains(newRoom))
            this.EntryRooms.Add(newRoom);
    }

    public void RemoveEntryRooms(Room oldRoom)
    {
        if (oldRoom == null)
            return;
        if (this.EntryRooms != null)
            if (this.EntryRooms.Contains(oldRoom))
                this.EntryRooms.Remove(oldRoom);
    }

    public void RemoveAllEntryRooms()
    {
        if (EntryRooms != null)
            EntryRooms.Clear();
    }

    public static Renovation Parse(String id)
    {
        Renovation r = new Renovation();
        r.Id = id;
        return r;
    }
}