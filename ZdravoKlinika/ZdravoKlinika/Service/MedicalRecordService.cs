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

        public void AddCurrentMedication(String id, Medication medication)
        {
            this.medicalRecordRepository.AddCurrentMedication(id, medication);
        }
    }
}
