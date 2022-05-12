
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

public class EquipmentRepository
{
    private EquipmentDataHandler equipmentDataHandler;
    private OrderDataHandler orderDataHandler;
    private List<Equipment> equipment;
    private List<Equipment> expendabilityList;
    
    public EquipmentDataHandler EquipmentDataHandler { get => equipmentDataHandler; set => equipmentDataHandler = value; }

    public EquipmentRepository()
    {
        this.EquipmentDataHandler = new EquipmentDataHandler();
        OrderDataHandler = new OrderDataHandler();

        this.equipment = EquipmentDataHandler.Read();
        this.expendabilityList = new List<Equipment>();

        UpdateStorageForOrders();
    }

    private void UpdateStorageForOrders()
    {
        // goes thorugh all the orders and checks if theres any that got into storage
        List<Order> orders = OrderDataHandler.Read();
        if (orders != null) 
        {
            foreach (Order order in orders)
            {
                if (order.IsOrderFinished)
                {
                    continue;
                }
                DateTime deliveryDate = order.CreationDate.AddDays(3);
                if (deliveryDate < DateTime.Now)
                {
                    AddNewEqupment(order);
                    order.IsOrderFinished = true;
                }
            }
            OrderDataHandler.Write(orders);
            EquipmentDataHandler.Write(equipment);
        }
       
        
    }

    private void AddNewEqupment(Order order)
    {
        List<Equipment> equipmentToAdd = order.EquipmentToOrder;
        foreach (Equipment equipmentInOrder in equipmentToAdd)
        {
            foreach (Equipment equipmentInStorage in Equipment)
            {
                if (equipmentInOrder.Id.Equals(equipmentInStorage.Id))
                { 
                    equipmentInStorage.Amount += equipmentInOrder.Amount;
                    break;
                }
            }
        }
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

    public OrderDataHandler OrderDataHandler { get => orderDataHandler; set => orderDataHandler = value; }

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