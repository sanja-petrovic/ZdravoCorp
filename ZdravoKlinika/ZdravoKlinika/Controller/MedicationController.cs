using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class MedicationController
    {

        private MedicationService medicationService;


        public MedicationController()
        {
            this.medicationService = new MedicationService();
        }

        public List<Medication> GetAll()
        {
            return this.medicationService.GetAll();
        }

        public Medication GetById(string medId)
        {
            return this.medicationService.GetById(medId);
        }

        public Medication GetByCodeAndName(string medicationCode, string brandName)
        {
            return this.medicationService.GetByCodeAndName(medicationCode, brandName);
        }

        public List<Medication> GetByApprovedValue(bool approved)
        {
            return this.medicationService.GetByApprovedValue(approved);
        }

        public void CreateMedication(string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, String note, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, string dosageInstructions, int amount)
        {
            this.medicationService.CreateMedication(medicationCode, brandName, dosage, activeSubstances, form, note, allergens, validated, alternatives, classification, indications, sideEffects, dosageInstructions, amount);
        }

        public void UpdateMedication(string medicationId, string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, String note, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, string dosageInstructions, int amount)
        {
            this.medicationService.UpdateMedication(medicationId, medicationCode, brandName, dosage, activeSubstances, form, note, allergens, validated, alternatives, classification, indications, sideEffects, dosageInstructions, amount);

        }

        public void UpdateMedication(string medicationId, string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, String note, List<string> allergens, bool validated, string classification, string indications, string sideEffects, string dosageInstructions, int amount)
        {
            Medication medication = new Medication { MedicationId = medicationId, MedicationCode = medicationCode, BrandName = brandName, Dosage = dosage, ActiveSubstances = activeSubstances, Form = form, Note = note, Allergens = allergens, Validated = validated, Classification = classification, Indications = indications, SideEffects = sideEffects, DosageInstructions = dosageInstructions, Amount = amount };
            this.medicationService.UpdateMedication(medication);
        }

        public void DeleteMedication(string medicationId)
        {
            this.medicationService.DeleteMedication(medicationId);
        }

        public List<Medication> GetApproved()
        {
            return this.medicationService.GetApproved();
        }


        public List<Medication> GetAlternatives(String medicationId)
        {
            return this.medicationService.GetAlternatives(this.medicationService.GetById(medicationId));
        }

    }
}
