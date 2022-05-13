
using System;
using System.Collections.Generic;
using System.IO;

public class RenovationRepository
{
    private RenovationDataHandler renovationDataHandler;
    private List<Renovation> renovations;
    
    public RenovationDataHandler RenovationDataHandler { get => renovationDataHandler; set => renovationDataHandler = value; }

    public RenovationRepository()
    {
        this.renovationDataHandler = new RenovationDataHandler();
        this.renovations = this.renovationDataHandler.Read();
    }

    public List<Renovation> Renovations
    {
        get
        {
            if (renovations == null)
                renovations = new List<Renovation>();
            return renovations;
        }
        set
        {
            RemoveAllRenovation();
            if (value != null)
            {
                foreach (Renovation oRenovation in value)
                    AddRenovation(oRenovation);
            }
        }
    }

    public void AddRenovation(Renovation newRenovation)
    {
        if (newRenovation == null)
            return;
        if (this.renovations == null)
            this.renovations = new List<Renovation>();
        if (!this.renovations.Contains(newRenovation))
            this.renovations.Add(newRenovation);
    }

    public void RemoveRenovation(Renovation oldRenovation)
    {
        if (oldRenovation == null)
            return;
        if (this.renovations != null)
            if (this.renovations.Contains(oldRenovation))
                this.renovations.Remove(oldRenovation);
    }

    public void RemoveAllRenovation()
    {
        if (renovations != null)
            renovations.Clear();
    }

    public List<Renovation> GetAll()
    {
        return this.renovations;
    }

    public Renovation GetById(String id)
    {
        foreach (Renovation r in this.renovations)
        {
            if (r.Id.Equals(id))
            {
                return r;
            }
        }

        return null;
    }

    public void CreateRenovation(Renovation renovation)
    {
        this.renovations.Add(renovation);
        renovationDataHandler.Write(this.renovations);
    }

    public void DeleteRenovation(Renovation renovation)
    {
        if (renovation == null)
            return;
        if (this.renovations != null)
            if (this.renovations.Contains(renovation))
                this.renovations.Remove(renovation);
        renovationDataHandler.Write(this.renovations);
    }

    public void UpdateRenovation(Renovation renovation)
    {
        if (renovation == null)
            return;
        if (this.renovations != null)
            foreach (Renovation r in this.renovations)
            {
                if (r.Id.Equals(renovation.Id))
                {
                    r.EntryRooms = renovation.EntryRooms;
                    r.NumberOfExitRooms = renovation.NumberOfExitRooms;
                    r.ScheduledDateTime = renovation.ScheduledDateTime;
                    r.IsRenovationFinished = renovation.IsRenovationFinished;
                }
            }
        renovationDataHandler.Write(this.renovations);
    }

}