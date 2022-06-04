
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKlinika.Repository.Interfaces;

public class RenovationRepository : IRenovationRepository
{
    private RenovationDataHandler renovationDataHandler;
    private List<Renovation> renovations;
    
    public RenovationDataHandler RenovationDataHandler { get => renovationDataHandler; set => renovationDataHandler = value; }

    public RenovationRepository()
    {
        RenovationDataHandler = new RenovationDataHandler();
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
        Renovation? returnValue = null;
        foreach (Renovation r in this.renovations)
        {
            if (r.Id.Equals(id))
            {
                returnValue = r;
            }
        }

        return returnValue;
    }

    public void Add(Renovation renovation)
    {
        this.renovations.Add(renovation);
        renovationDataHandler.Write(this.renovations);
    }

    public void Remove(Renovation renovation)
    {
        if (renovation == null)
            return;
        if (this.renovations != null)
            if (this.renovations.Contains(renovation))
                this.renovations.Remove(renovation);
        renovationDataHandler.Write(this.renovations);
    }

    public void Update(Renovation renovation)
    {
        if (renovation == null)
            return;
        if (this.renovations != null)
            foreach (Renovation r in this.renovations)
            {
                if (r.Id.Equals(renovation.Id))
                {
                    UpdateRenovationValues(r, renovation);
                }
            }
        renovationDataHandler.Write(this.renovations);
    }

    private void UpdateRenovationValues(Renovation renovationToBeUpdated, Renovation updatingValues)
    {
        renovationToBeUpdated.EntryRooms = updatingValues.EntryRooms;
        renovationToBeUpdated.NumberOfExitRooms = updatingValues.NumberOfExitRooms;
        renovationToBeUpdated.ScheduledDateTime = updatingValues.ScheduledDateTime;
        renovationToBeUpdated.IsRenovationFinished = updatingValues.IsRenovationFinished;
    }

    public void RemoveAll()
    {
        this.renovations.Clear();
        this.renovationDataHandler.Write(this.renovations);
    }

}