using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IMedicalRecordRepository : IRepositoryBase<Model.MedicalRecord, int>
    {
        public void UpdateReferences(Model.MedicalRecord medicalRecord);
        public void AddCurrentMedication(String medicalRecordId, Medication medication);
        public int FindIndexInList(string id);

    }
}
