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

        public List<Prescription> GetAll()
        {
            return this.prescriptionDataHandler.Read();
        }

        

        public void UpdateReferences()
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

        public void Prescribe(Doctor doctor, RegisteredPatient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency)
        {
            Prescription prescription = new Prescription(doctor, patient, medication, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency);
            RegisteredPatientRepository registeredPatientRepository = new RegisteredPatientRepository();
            registeredPatientRepository.GetById(patient.PersonalId).MedicalRecord.AddCurrentMedication(medication);
            
        }
        
    }
}
