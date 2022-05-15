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
        private List<Medication> approvedValueList;


        public MedicationRepository()
        {
            medicationDataHandler = new MedicationDataHandler();
            ReadDataFromFiles();

            this.doctorRepository = new DoctorRepository();
            this.approvedValueList = new List<Medication>();
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

        public Medication GetByCodeAndName(string medicationCode, string brandName)
        {
            ReadDataFromFiles();
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
                if (m.Validated == approved)
                {
                    approvedValueList.Add(m);
                }
            }

            return this.approvedValueList;
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
    }
}
