using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    internal class PrescriptionRepository
    {

        private List<Prescription> prescriptions;
        private PrescriptionDataHandler prescriptionDataHandler;

        public PrescriptionRepository()
        {
            prescriptionDataHandler = new PrescriptionDataHandler();
            this.prescriptions = prescriptionDataHandler.Read();
        }

        public void Prescribe(string medicalRecordId, Medication medication, int amount, string doctorsNote, int duration, int frequency, string repeat, int singleDose)
        {
            Prescription prescription = new Prescription(medication, amount, doctorsNote, duration, frequency, repeat, singleDose);
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            medicalRecordRepository.GetById(medicalRecordId).CurrentMedication.Add(medication);
        }
        
    }
}
