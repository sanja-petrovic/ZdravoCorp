using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class MedicalRecordService
    {
        private MedicalRecordRepository medicalRecordRepository;


        public MedicalRecordService()
        {
            this.medicalRecordRepository = new MedicalRecordRepository();
        }


        public MedicalRecord GetById(String id) 
        {
            return this.medicalRecordRepository.GetById(id);
        }

        public void AddCurrentMedication(MedicalRecord record, Medication medication)
        {
            this.medicalRecordRepository.AddCurrentMedication(record, medication);
        }

        public void AddDiagnosis(String diagnosis, MedicalRecord record)
        {
            this.medicalRecordRepository.AddDiagnosis(diagnosis, record);
        }


        public List<string> GetDiagnosesAndAllergies(MedicalRecord medicalRecord)
        {
            return this.medicalRecordRepository.GetDiagnosesAndAllergies(medicalRecord);
        }
    }
}
