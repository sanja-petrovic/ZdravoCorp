using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IMedicalRecordRepository : IRepositoryBase<Model.MedicalRecord, string>
    {
        public void AddCurrentMedication(String medicalRecordId, Medication medication);

    }
}
