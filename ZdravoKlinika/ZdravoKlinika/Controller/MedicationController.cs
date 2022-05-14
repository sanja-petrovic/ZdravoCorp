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

        public void CreateMedication(string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, List<String> notes, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, Doctor reviewer, string note, string dosageInstructions)
        {
            this.medicationService.CreateMedication(medicationCode, brandName, dosage, activeSubstances, form, notes, allergens, validated, alternatives, classification, indications, sideEffects, reviewer, note, dosageInstructions);
        }

        public void UpdateMedication(string medicationId, string medicationCode, String brandName, string dosage, List<String> activeSubstances, string form, List<String> notes, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, Doctor reviewer, string note, string dosageInstructions)
        {
            this.medicationService.UpdateMedication(medicationId, medicationCode, brandName, dosage, activeSubstances, form, notes, allergens, validated, alternatives, classification, indications, sideEffects, reviewer, note, dosageInstructions);
        }

        public void DeleteMedication(string medicationId)
        {
            this.medicationService.DeleteMedication(medicationId);
        }

    }
}
