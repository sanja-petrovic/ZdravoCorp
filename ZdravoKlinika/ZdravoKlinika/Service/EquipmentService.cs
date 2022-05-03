
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

    public void CreateEquipment(String name, int amount, bool expendable)
    {
        List<Equipment> equipment = this.EquipmentRepository.GetAll();
        int newEquipmentId;
        if(equipment.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach(Equipment eq in equipment)
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

        Equipment eqa = new Equipment(newEquipmentId.ToString(), name, amount, expendable);
        this.EquipmentRepository.CreateEquipment(eqa);
    }

    public void UpdateEquipment(String id, String name, int amount, bool expendable)
    {
        Equipment eq = this.equipmentRepository.GetById(id);
        eq.Name = name;
        eq.Amount = amount;
        eq.Expendable = expendable;
        this.equipmentRepository.UpdateEquipment(eq);
    }

    public void DeleteEquipment(String id)
    {
        Equipment eq = this.equipmentRepository.GetById(id);
        this.equipmentRepository.DeleteEquipment(eq);
    }

}