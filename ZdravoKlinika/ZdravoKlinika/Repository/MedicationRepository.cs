using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    internal class MedicationRepository : IMedicationRepository
    {

        private MedicationDataHandler medicationDataHandler;
        private List<Medication> medications;
        private DoctorRepository doctorRepository;
        private List<Medication> approvedValueList;

        public MedicationRepository()
        {
            medicationDataHandler = new MedicationDataHandler();
            ReadDataFromFile();

            this.doctorRepository = new DoctorRepository();
            this.approvedValueList = new List<Medication>();
        }

        private void ReadDataFromFile()
        {
            medications = medicationDataHandler.Read();
            if (medications == null) medications = new List<Medication>();
        }

        private void UpdateReferences(Medication medication)
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

        private void UpdateDoctor(Medication medication)
        {

            if (medication.Validator != null)
            {
                medication.Validator = doctorRepository.GetById(medication.Validator.PersonalId);
            }
        }

        public List<Medication> GetAll()
        {
            ReadDataFromFile();
            foreach (Medication medication in this.medications)
            {
                UpdateReferences(medication);
            }

            return medications;
        }

        public Medication GetById(String id)
        {
            ReadDataFromFile();
            Medication? medicationToReturn = null;
            foreach (Medication medication in medications)
            {
                if(medication.MedicationId.Equals(id))
                {
                    UpdateDoctor(medication);
                    medicationToReturn = medication;
                    break;
                }
            }

            return medicationToReturn;
        }
      
        public Medication GetByCodeAndName(string medicationCode, string brandName)
        {
            ReadDataFromFile();
            Medication? medicationToReturn = null;
            foreach (Medication medication in medications)
            {
                if (medication.MedicationCode.Equals(medicationCode) && medication.BrandName.Equals(brandName))
                {
                    UpdateReferences(medication);
                    medicationToReturn = medication;
                    break;
                }
            }

            return medicationToReturn;
        }

        public List<Medication> GetByApprovedValue(bool approved)
        {
            this.approvedValueList.Clear();

            foreach (Medication m in this.medications)
            {
                UpdateReferences(m);
                if (m.Validated == approved)
                {
                    approvedValueList.Add(m);
                }
            }

            return this.approvedValueList;
        }
      
        public List<Medication> GetAlternatives(Medication medication)
        {
            UpdateReferences(medication);
            return medication.Alternatives;
        }

        public void Add(Medication medication)
        {
            this.medications.Add(medication);
            medicationDataHandler.Write(this.medications);
        }

        public void Remove(Medication medication)
        {
            if (medication == null)
                return;
            if (this.medications != null)
                if (this.medications.Contains(medication))
                    this.medications.Remove(medication);
            medicationDataHandler.Write(this.medications);
        }

        public void Update(Medication medication)
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

        private int GetIndex(Medication medication)
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

        public void RemoveAll()
        {
            this.medications.Clear();
            this.medicationDataHandler.Write(this.medications);

        }
    }
}
