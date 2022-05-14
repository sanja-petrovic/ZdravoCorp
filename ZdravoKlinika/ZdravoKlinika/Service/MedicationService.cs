using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class MedicationService
    {
        private MedicationRepository medicationRepository;


        public MedicationService()
        {
            this.medicationRepository = new MedicationRepository();
        }

        public List<Medication> GetAll()
        {
            return this.medicationRepository.GetAll();
        }

        public Medication GetById(string medId)
        {
            return this.medicationRepository.GetById(medId);
        }

        public void CreateMedication(string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, List<String> notes, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, Doctor reviewer, string note, string dosageInstructions)
        {         
            Medication m = new Medication(GenerateId().ToString(), medicationCode, brandName, dosage, activeSubstances, form, notes, allergens, validated, alternatives, classification, indications, sideEffects, reviewer, note, dosageInstructions);
            this.medicationRepository.CreateMedication(m);
        }

        public void UpdateMedication(string medicationId, string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, List<String> notes, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, Doctor reviewer, string note, string dosageInstructions)
        {
            Medication m = this.medicationRepository.GetById(medicationId);

            m.MedicationCode = medicationCode;
            m.BrandName = brandName;
            m.Dosage = dosage;
            m.ActiveSubstances = activeSubstances;
            m.Form = form;
            m.Notes = notes;
            m.Allergens = allergens;
            m.Validated = validated;
            m.Alternatives = alternatives;
            m.Classification = classification;
            m.Indications = indications;
            m.SideEffects = sideEffects;
            m.Reviewer = reviewer;
            m.Note = note;
            m.DosageInstructions = dosageInstructions;

            this.medicationRepository.UpdateMedication(m);
        }

        public void DeleteMedication(string medicationId)
        {
            Medication m = medicationRepository.GetById(medicationId);
            this.medicationRepository.DeleteMedication(m);
        }

        public int GenerateId()
        {
            List<Medication> medications = this.medicationRepository.GetAll();
            int newMedicationId;
            if (medications.Count > 0)
            {
                int maxId = 0;
                int trenutniId = 0;
                foreach (Medication m in medications)
                {
                    trenutniId = Int32.Parse(m.MedicationId);
                    if (trenutniId > maxId) maxId = trenutniId;
                }

                newMedicationId = maxId + 1;
            }
            else
            {
                newMedicationId = 1;
            }
            return newMedicationId;
        }
    }
}
