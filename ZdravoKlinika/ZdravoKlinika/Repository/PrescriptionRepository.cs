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
        private MedicationDataHandler medicationDataHandler;

        public PrescriptionRepository()
        {
            prescriptionDataHandler = new PrescriptionDataHandler();
            this.prescriptions = prescriptionDataHandler.Read();
            this.medicationDataHandler = new MedicationDataHandler();
        }

        public void updateReferences()
        {
            List<Medication> medications = medicationDataHandler.Read();
            foreach(Medication medication in medications)
            {
                foreach(Prescription prescription in this.prescriptions)
                {
                    if(prescription.Medication.MedicationId.Equals(medication.MedicationId))
                    {
                        prescription.Medication = medication;
                        break;
                    }
                }
            }
        }

        public void Prescribe(Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency)
        {
            Prescription prescription = new Prescription(medication, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency);
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
        }
        
    }
}
