
using System;
using System.Collections.Generic;

public class EquipmentService
{
    private EquipmentRepository equipmentRepository;

    public EquipmentRepository EquipmentRepository { get => equipmentRepository; set => equipmentRepository = value; }

    public EquipmentService()
    {
        this.EquipmentRepository = new EquipmentRepository();
    }

    public List<Equipment> GetAll()
    {
        return this.equipmentRepository.GetAll();
    }

    public Equipment GetById(String id)
    {
        return this.equipmentRepository.GetById(id);
    }

    public List<Equipment> GetByExpendability(bool expendable)
    {
        return this.equipmentRepository.GetByExpendability(expendable);
    }

    public void CreateEquipment(Equipment eq)
    {
        this.EquipmentRepository.CreateEquipment(new Equipment(GenerateEquipmentId().ToString(), eq.Name, eq.Amount, eq.Expendable));
    }

    public void UpdateEquipment(Equipment changedEquipment)
    {
        Equipment equipment = this.equipmentRepository.GetById(changedEquipment.Id);
        UpdateEquipmentValues(equipment, changedEquipment);      
        this.equipmentRepository.UpdateEquipment(equipment);
    }

    public void DeleteEquipment(String id)
    {
        Equipment eq = this.equipmentRepository.GetById(id);
        this.equipmentRepository.DeleteEquipment(eq);
    }

    private void UpdateEquipmentValues(Equipment equipmentToBeUpdated, Equipment updatingValues)
    {
        equipmentToBeUpdated.Name = updatingValues.Name;
        equipmentToBeUpdated.Amount = updatingValues.Amount;
        equipmentToBeUpdated.Expendable = updatingValues.Expendable;
    }

    private int GenerateEquipmentId()
    {
        List<Equipment> equipment = this.EquipmentRepository.GetAll();
        int newEquipmentId;
        if (equipment.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach (Equipment eq in equipment)
            {
                trenutniId = Int32.Parse(eq.Id);
                if (trenutniId > maxId) maxId = trenutniId;
            }
            newEquipmentId = maxId + 1;
        }
        else
        {
            newEquipmentId = 1;
        }
        return newEquipmentId;
    }
}