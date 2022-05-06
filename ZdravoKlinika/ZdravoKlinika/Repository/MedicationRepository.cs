using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;

namespace ZdravoKlinika.Repository
{
    internal class MedicationRepository
    {

        private MedicationDataHandler medicationDataHandler;
        private List<Medication> medications;
        private DoctorRepository doctorRepository;


        public MedicationRepository()
        {
            medicationDataHandler = new MedicationDataHandler();
            this.medications = medicationDataHandler.Read();
            foreach(Medication medication in medications)
            {
                Console.WriteLine(medication.MedicationId);
            }
            this.doctorRepository = new DoctorRepository();
        }

        public void UpdateReferences()
        {
            foreach (Medication medication in this.medications)
            {
                if(medication.ApprovedBy != null)
                {
                    medication.ApprovedBy = this.doctorRepository.GetById(medication.ApprovedBy.PersonalId);
                }
                if(medication.Alternatives != null)
                {
                    for (int i = 0; i < medication.Alternatives.Count; i++)
                    {
                        medication.Alternatives[i] = this.GetById(medication.Alternatives[i].MedicationId);
                    }
                }
            }
        }

        public List<Medication> GetAll()
        {
            return medicationDataHandler.Read();
        }

        public Medication GetById(String id)
        {
            foreach(Medication medication in medications)
            {
                if(medication.MedicationId == id)
                {
                    return medication;
                }
            }

            return null;
        }
    }
}
