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
            if (medication.Alternatives != null)
            {
                for (int i = 0; i < medication.Alternatives.Count; i++)
                {
                    medication.Alternatives[i] = this.GetById(medication.Alternatives[i].MedicationId);
                }
            }
            UpdateDoctor(medication);
        }

        public void UpdateDoctor(Medication medication)
        {

            if (medication.Validator != null)
            {
                medication.Validator = doctorRepository.GetById(medication.Validator.PersonalId);
            }
        }

        public List<Medication> GetAll()
        {
            ReadDataFromFiles();
            foreach (Medication medication in this.medicationDataHandler.Read())
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
                    //UpdateReferences(medication); (!)
                    //causes stack overflow in case of a circular reference
                    //instead if you need alternatives, call GetAlternatives(string medicationId)
                    UpdateDoctor(medication);
                    medicationToReturn = medication;
                    break;
                }
            }

            return medicationToReturn;
        }

        public List<Medication> GetAlternatives(Medication medication)
        {
            UpdateReferences(medication);
            return medication.Alternatives;
        }

        public void CreateMedication(Medication medication)
        {
            this.medications.Add(medication);
            medicationDataHandler.Write(this.medications);
        }

        public void DeleteMedication(Medication medication)
        {
            if (medication == null)
                return;
            if (this.medications != null)
                if (this.medications.Contains(medication))
                    this.medications.Remove(medication);
            medicationDataHandler.Write(this.medications);
        }

        public void UpdateMedication(Medication medication)
        {
            if (medication == null)
                return;
            if (this.medications != null)
            {
                int index = GetIndex(medication);
                this.medications[index] = medication;
                medicationDataHandler.Write(this.medications);
            }
        }

        public int GetIndex(Medication medication)
        {
            int index = -1;
            for(int i = 0; i < this.medications.Count; i++)
            {
                if(this.medications[i].MedicationId.Equals(medication.MedicationId))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public List<Medication> GetApproved()
        {
            List<Medication> list = new List<Medication>();

            foreach(Medication medication in this.medicationDataHandler.Read())
            {
                if(medication.Validated)
                {
                    UpdateReferences(medication);
                    list.Add(medication);
                }
            }

            return list;
        }

    }
}
