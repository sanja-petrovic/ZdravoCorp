using System;
using System.Collections.Generic;

public class Renovation
{
    private String id;
    private int numberOfExitRooms;
    private DateTime scheduledDateTime;

    private List<Room> entryRooms;

    public string Id { get => id; set => id = value; }
    public int NumberOfExitRooms { get => numberOfExitRooms; set => numberOfExitRooms = value; }
    public DateTime ScheduledDateTime { get => scheduledDateTime; set => scheduledDateTime = value; }

/*    public List<Room> EntryRooms
    {
        get
        {
            if (EntryRooms == null)
                EntryRooms = new List<Room>();
            return EntryRooms;
        }
        set
        {
            RemoveAllEntryRooms();
            if (value != null)
            {
                foreach (Room oRoom in value)
                    AddEntryRooms(oRoom);
            }
        }
    }*/

    public List<Room> EntryRooms { get => entryRooms; set => entryRooms = value; }

    public Renovation(string id, int numberOfExitRooms, DateTime scheduledDateTime, List<Room> entryRooms)
    {
        this.id = id;
        this.numberOfExitRooms = numberOfExitRooms;
        this.scheduledDateTime = scheduledDateTime;
        this.EntryRooms = entryRooms;
    }

    public Renovation()
    {
    }

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