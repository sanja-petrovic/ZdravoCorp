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
            UpdateReferences();
        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionDataHandler.Read();
        }

        public Prescription GetById(int id)
        {
            UpdateReferences();
            foreach(Prescription p in this.prescriptions)
            {
                if(p.Id == id)
                {
                    return p;
                }
            }

            return null;
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

        public void Prescribe(Prescription prescription)
        {
            this.prescriptions.Add(prescription);
            this.prescriptionDataHandler.Write(this.prescriptions);
            
        }
        
    }
}
