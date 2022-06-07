
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

public class EquipmentRepository : IEquipmentRepository
{
    private EquipmentDataHandler equipmentDataHandler;
    private OrderDataHandler orderDataHandler;
    private List<Equipment> equipment;
    private List<Equipment> expendabilityList; //contains either expendable or unexpendable equipment
                                               //based on param bool expendable
    public EquipmentDataHandler EquipmentDataHandler { get => equipmentDataHandler; set => equipmentDataHandler = value; }
    public OrderDataHandler OrderDataHandler { get => orderDataHandler; set => orderDataHandler = value; }


    public EquipmentRepository()
    {
        EquipmentDataHandler = new EquipmentDataHandler();
        OrderDataHandler = new OrderDataHandler();

        this.equipment = EquipmentDataHandler.Read();
        this.expendabilityList = new List<Equipment>();
        
        UpdateStorageForOrders();
    }

    private void UpdateStorageForOrders()
    {
        // goes through all the orders and checks if theres any that got into storage
        List<Order> orders = OrderDataHandler.Read();
        if (orders != null) 
        {
            foreach (Order order in orders)
            {
                FinalizeOrder(order);
            }
            OrderDataHandler.Write(orders);
            EquipmentDataHandler.Write(equipment);
        }
    }

    private void FinalizeOrder(Order order)
    {
        if (!order.IsOrderFinished)
        {
            DateTime deliveryDate = order.CreationDate.AddDays(3);
            if (deliveryDate < DateTime.Now)
            {
                AddEquipmentInOrder(order);
                order.IsOrderFinished = true;
            }
        } 
    }

    private void AddEquipmentInOrder(Order order)
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
        Equipment? returnValue = null;
        foreach (Equipment eq in this.equipment)
        {
            if (eq.Id.Equals(id))
            {
                returnValue = eq;
            }
        }
        return returnValue;
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

    public void Add(Equipment eq)
    {
        this.equipment.Add(eq);
        EquipmentDataHandler.Write(this.equipment);
    }

    public void Remove(Equipment eq)
    {
        if (eq == null)
            return;
        if (this.equipment != null)
            if (this.equipment.Contains(eq))
                this.equipment.Remove(eq);
        EquipmentDataHandler.Write(this.equipment);
    }

    public void Update(Equipment eq)
    {
        if (eq == null)
            return;
        if (this.equipment != null)
            foreach (Equipment eqIterate in this.equipment)
            {
                if (eqIterate.Id.Equals(eq.Id))
                {
                    UpdateEquipmentValues(eqIterate, eq);
                }
            }
        EquipmentDataHandler.Write(this.equipment);
    }

    private void UpdateEquipmentValues(Equipment equipmentToBeUpdated, Equipment updatingValues)
    {
        equipmentToBeUpdated.Name = updatingValues.Name;
        equipmentToBeUpdated.Amount = updatingValues.Amount;
        equipmentToBeUpdated.Expendable = updatingValues.Expendable;
    }

    public void RemoveAll()
    {
        this.equipment.Clear();
        this.equipmentDataHandler.Write(this.equipment);

    }

}