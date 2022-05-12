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
            ReadDataFromFiles();

            this.doctorRepository = new DoctorRepository();
        }

        private void ReadDataFromFiles()
        {
            medications = medicationDataHandler.Read();
            if (medications == null) medications = new List<Medication>();
        }

        public void UpdateReferences(Medication medication)
        {
            if(medication.ApprovedBy != null)
            {
                medication.ApprovedBy = this.doctorRepository.GetById(medication.ApprovedBy.PersonalId);
            }
            if (medication.Alternatives != null)
            {
                for (int i = 0; i < medication.Alternatives.Count; i++)
                {
                    medication.Alternatives[i] = this.GetById(medication.Alternatives[i].MedicationId);
                }
            }
        }

        public List<Medication> GetAll()
        {
            ReadDataFromFiles();
            foreach (Medication medication in this.medications)
            {
                UpdateReferences(medication);
            }

            return medications;
        }

        public Medication GetById(String id)
        {
            ReadDataFromFiles();
            Medication? medicationToReturn = null;
            foreach (Medication medication in medications)
            {
                if(medication.MedicationId == id)
                {
                    UpdateReferences(medication);
                    medicationToReturn = medication;
                    break;
                }
            }

            return medicationToReturn;
        }
    }
}
