
using System;
using System.Collections.Generic;

public class RenovationController
{
    private RenovationService renovationService;

    public RenovationService RenovationService { get => renovationService; set => renovationService = value; }

    public RenovationController()
    {
        this.renovationService = new RenovationService();
    }

    public List<Renovation> GetAll()
    {
        return this.renovationService.GetAll();
    }

    public Renovation GetById(String id)
    {
        return this.renovationService.GetById(id);
    }

    public void CreateRenovation(List<Room> entryRooms, int numberOfExitRooms, DateTime scheduledDateTime)
    {
        this.renovationService.CreateRenovation(entryRooms, numberOfExitRooms, scheduledDateTime);
    }

    public void UpdateRenovation(String id, List<Room> entryRooms, int numberOfExitRooms, DateTime scheduledDateTime)
    {
        this.renovationService.UpdateRenovation(id, entryRooms, numberOfExitRooms, scheduledDateTime);
    }

    public void DeleteRenovation(String id)
    {
        this.renovationService.DeleteRenovation(id);
    }

}