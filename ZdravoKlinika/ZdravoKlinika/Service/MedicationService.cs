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

        public Medication GetByCodeAndName(string medicationCode, string brandName)
        {
            return this.medicationRepository.GetByCodeAndName(medicationCode, brandName);
        }

        public List<Medication> GetByApprovedValue(bool approved)
        {
            return this.medicationRepository.GetByApprovedValue(approved);
        }

        public void CreateMedication(string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, String note, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, string dosageInstructions, int amount)
        {         
            Medication m = new Medication(GenerateId().ToString(), medicationCode, brandName, dosage, activeSubstances, form, note, allergens, validated, alternatives, classification, indications, sideEffects, dosageInstructions, amount);
            this.medicationRepository.Add(m);
        }

        public void UpdateMedication(string medicationId, string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, String note, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, string dosageInstructions, int amount)

        {
            Medication medication = new Medication(medicationId, medicationCode, brandName, dosage, activeSubstances, form, note, allergens, validated, alternatives, classification, indications, sideEffects, dosageInstructions, amount);
            this.medicationRepository.Update(medication);
        }

        public void UpdateMedication(Medication medication)
        {
            this.medicationRepository.Update(medication);
        }

        public void DeleteMedication(string medicationId)
        {
            Medication m = medicationRepository.GetById(medicationId);
            this.medicationRepository.Remove(m);
        }

        public int GenerateId()
        {
            List<Medication> medications = this.medicationRepository.GetAll();
            int newMedicationId = medications.Count > 0 ? Int32.Parse(medications.Last().MedicationId) + 1 : 1;
            
            return newMedicationId;
        }


        public List<Medication> GetAlternatives(Medication medication)
        {
            return this.medicationRepository.GetAlternatives(medication);
        }

    }
}
