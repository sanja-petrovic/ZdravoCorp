using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IMedicalRecordRepository : IRepositoryBase<Model.MedicalRecord, string>
    {
        public void AddCurrentMedication(MedicalRecord record, Medication medication);
        public void AddDiagnosis(String diagnosis, MedicalRecord record);
        public List<string> GetDiagnosesAndAllergies(MedicalRecord medicalRecord);


    }
}
