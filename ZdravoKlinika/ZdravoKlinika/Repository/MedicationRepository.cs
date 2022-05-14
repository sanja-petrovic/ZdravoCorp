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
            if(medication.Reviewer != null)
            {
                medication.Reviewer = this.doctorRepository.GetById(medication.Reviewer.PersonalId);
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
                foreach (Medication m in this.medications)
                {
                    if (m.MedicationId.Equals(medication.MedicationId))
                    {
                        m.MedicationCode = medication.MedicationCode;
                        m.BrandName = medication.BrandName;
                        m.Dosage = medication.Dosage;
                        m.ActiveSubstances = medication.ActiveSubstances;
                        m.Form = medication.Form;
                        m.Note = medication.Note;
                        m.Allergens = medication.Allergens;
                        m.Validated = medication.Validated;
                        m.Alternatives = medication.Alternatives;
                        m.Classification = medication.Classification;
                        m.Indications = medication.Indications;
                        m.SideEffects = medication.SideEffects;
                        m.Reviewer = medication.Reviewer;
                        m.Comment = medication.Comment;
                        m.DosageInstructions = medication.DosageInstructions;
                        m.Amount = medication.Amount;
                    }
                }
            medicationDataHandler.Write(this.medications);
        }
    }
}
