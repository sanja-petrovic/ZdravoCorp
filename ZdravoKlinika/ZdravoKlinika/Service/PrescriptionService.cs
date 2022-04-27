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
        private MedicalRecordService medicalRecordService;

        public PrescriptionService()
        {
            prescriptionRepository = new PrescriptionRepository();
            medicalRecordService = new MedicalRecordService();
        }


        public List<Prescription> GetAll()
        {
            return this.prescriptionRepository.GetAll();
        }

        public Prescription GetById(int id)
        {
            return this.prescriptionRepository.GetById(int id);
        }

        public void Prescribe(Doctor doctor, RegisteredPatient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency)
        {
            int prescriptionId = 1;
            if(this.GetAll().Count > 0)
            {
                prescriptionId = this.GetAll().Last().Id + 1;
            }

            Prescription prescription = new Prescription(doctor, patient, medication, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency, prescriptionId);
            medicalRecordService.AddCurrentMedication(patient.MedicalRecord.MedicalRecordId, medication);
            this.prescriptionRepository.Prescribe(prescription);

        }
    }
}
