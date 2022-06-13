using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class PrescriptionService
    {
        private PrescriptionRepository prescriptionRepository;
        private MedicalRecordRepository medicalRecordRepository;
        private RegisteredPatientRepository registeredPatientRepository;

        public PrescriptionService()
        {
            prescriptionRepository = new PrescriptionRepository();
            this.registeredPatientRepository = new RegisteredPatientRepository();
            medicalRecordRepository = new MedicalRecordRepository();   

        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionRepository.GetAll();
        }

        public List<Prescription> GetByPatient(RegisteredPatient patient)
        {
            return this.prescriptionRepository.GetByPatient(patient);
        }

        public Prescription GetById(int id)
        {
            return this.prescriptionRepository.GetById(id);
        }


        public void Prescribe(Prescription prescription)
        {
            int prescriptionId = (this.prescriptionRepository.GetAll().Count > 0) ? this.prescriptionRepository.GetAll().Last().Id + 1 : 1;
            prescription.Id = prescriptionId;
            if(prescription.Patient.GetPatientType().Equals(PatientType.Registered))
            {

                RegisteredPatient reg = registeredPatientRepository.GetById(prescription.Patient.GetPatientId());
                medicalRecordRepository.AddCurrentMedication(reg.MedicalRecord, prescription.Medication);
                this.registeredPatientRepository.RecordUpdated(reg);
            }
            this.prescriptionRepository.Prescribe(prescription);
        }
    }
}
