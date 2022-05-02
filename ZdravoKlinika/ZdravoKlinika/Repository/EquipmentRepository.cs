
using System;
using System.Collections.Generic;
using System.IO;

public class EquipmentRepository
{
    private EquipmentDataHandler equipmentDataHandler;
    private List<Equipment> equipment;
    private List<Equipment> expendabilityList;
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "equipment.json";

    public EquipmentDataHandler EquipmentDataHandler { get => equipmentDataHandler; set => equipmentDataHandler = value; }

    public EquipmentRepository()
    {
        this.EquipmentDataHandler = new EquipmentDataHandler(fileLocation);
        this.equipment = EquipmentDataHandler.Read();
        this.expendabilityList = new List<Equipment>();
    }

    public List<Equipment> Equipment
    {
        get
        {
            if (equipment == null)
                equipment = new List<Equipment>();
            return equipment;
        }
        set
        {
            RemoveAllEquipment();
            if (value != null)
            {
                foreach (Equipment oEquipment in value)
                    AddEquipment(oEquipment);
            }
        }
    }

    public void AddEquipment(Equipment newEquipment)
    {
        if (newEquipment == null)
            return;
        if (this.equipment == null)
            this.equipment = new List<Equipment>();
        if (!this.equipment.Contains(newEquipment))
            this.equipment.Add(newEquipment);
    }

    public void RemoveEquipment(Equipment oldEquipment)
    {
        if (oldEquipment == null)
            return;
        if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
                this.equipment.Remove(oldEquipment);
    }

    public void RemoveAllEquipment()
    {
        if (equipment != null)
            equipment.Clear();
    }

    public List<Equipment> GetAll()
    {
        return this.equipment;
    }

    public Equipment GetById(String id)
    {
        foreach (Equipment eq in this.equipment)
        {
            if (eq.Id.Equals(id))
            {
                return eq;
            }
        }

        return null;
    }

    public List<Equipment> GetByExpendability(bool expendable)
    {
        this.expendabilityList.Clear();

        foreach (Equipment eq in this.equipment)
        {
            if(eq.Expendable == expendable)
            {
                expendabilityList.Add(eq);
            }
        }

        return this.expendabilityList;
    }

    public void CreateEquipment(Equipment eq)
    {
        this.equipment.Add(eq);
        equipmentDataHandler.Write(this.equipment);
    }

    public void DeleteEquipment(Equipment eq)
    {
        if (eq == null)
            return;
        if (this.equipment != null)
            if (this.equipment.Contains(eq))
                this.equipment.Remove(eq);
        equipmentDataHandler.Write(this.equipment);
    }

    public void UpdateEquipment(Equipment eq)
    {
        if (eq == null)
            return;
        if (this.equipment != null)
            foreach (Equipment eqIterate in this.equipment)
            {
                if (eqIterate.Id.Equals(eq.Id))
                {
                    eqIterate.Name = eq.Name;
                    eqIterate.Amount = eq.Amount;
                    eqIterate.Expendable = eq.Expendable;
                }
            }
        equipmentDataHandler.Write(this.equipment);
    }

}