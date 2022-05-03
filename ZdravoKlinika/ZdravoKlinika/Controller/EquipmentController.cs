
using System;
using System.Collections.Generic;

public class EquipmentController
{
    private EquipmentService equipmentService;

    public EquipmentService EquipmentService { get => equipmentService; set => equipmentService = value; }

    public EquipmentController()
    {
        this.equipmentService = new EquipmentService();
    }

    public List<Equipment> GetAll()
    {
        return this.equipmentService.GetAll();
    }

    public Equipment GetById(String id)
    {
        return this.equipmentService.GetById(id);
    }

    public List<Equipment> GetByExpendability(bool expendable)
    {
        return this.equipmentService.GetByExpendability(expendable);
    }

    public void CreateEquipment(String name, int amount, bool expendable)
    {
        this.equipmentService.CreateEquipment(name, amount, expendable);
    }

    public void UpdateEquipment(String id, String name, int amount, bool expendable)
    {
        this.equipmentService.UpdateEquipment(id, name, amount, expendable);
    }

    public void DeleteEquipment(String id)
    {
        this.equipmentService.DeleteEquipment(id);
    }

}