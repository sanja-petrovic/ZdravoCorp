using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class MedViewModel : ViewModelBase
    {
        private bool isChecked;
        private string id;
        private string brandName;
        private string dosage;
        private string actives;
        private string form;
        private string classification;
        private string indications;
        private string instructions;
        private string sideEffects;
        private string allergens;
        private string code;
        private string note;
        private string reviewer;
        private Doctor doctor;
        private string comment;
        private int amount;
        private MedicationController medicationController;
        private MedApprovalRequestController approvalRequestController;
        private int requestId;

        public MedViewModel()
        {
            this.MedicationController = new MedicationController();
            this.ApprovalRequestController = new MedApprovalRequestController();
        }

        public string Id { get => id; set => SetProperty(ref id, value); }
        public string BrandName { get => brandName; set => SetProperty(ref brandName, value); }
        public string Dosage { get => dosage; set => SetProperty(ref dosage, value); }
        public string Actives { get => actives; set => SetProperty(ref actives, value); }
        public string Form { get => form; set => SetProperty(ref form, value); }
        public string Classification { get => classification; set => SetProperty(ref classification, value); }
        public string Indications { get => indications; set => SetProperty(ref indications, value); }
        public string Instructions { get => instructions; set => SetProperty(ref instructions, value); }
        public string SideEffects { get => sideEffects; set => SetProperty(ref sideEffects, value); }
        public string Allergens { get => allergens; set => SetProperty(ref allergens, value); }
        public string Code { get => code; set => SetProperty(ref code, value); }
        public string Note { get => note; set => SetProperty(ref note, value); }
        public string Reviewer { get => reviewer; set => SetProperty(ref reviewer, value); }
        public MedApprovalRequestController ApprovalRequestController { get => approvalRequestController; set => approvalRequestController = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public string Comment { get => comment; set => SetProperty(ref comment, value); }
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
        public int RequestId { get => requestId; set => SetProperty(ref requestId, value); }
        internal MedicationController MedicationController { get => medicationController; set => medicationController = value; }

        public void LoadMed(Medication medication)
        {
            Id = medication.MedicationId;
            BrandName = medication.BrandName;
            Dosage = medication.Dosage;
            foreach(string a in medication.ActiveSubstances)
            {
                Actives += a;
                if(a != medication.ActiveSubstances.Last())
                {
                    Actives += ", ";
                }
            }
            Form = medication.Form;
            Classification = medication.Classification;
            Indications = medication.Indications;
            Instructions = medication.DosageInstructions;
            SideEffects = medication.SideEffects;
            foreach (string a in medication.Allergens)
            {
                Allergens += a;
                if (a != medication.Allergens.Last())
                {
                    Allergens += ", ";
                }
            }
            Code = medication.MedicationCode;
            Note = medication.Note;
            if(medication.Validated)
            {
                Reviewer = medication.Validator.ToString();
            } else
            {
                Reviewer = "/";
            }
            Amount = medication.Amount;
        }

        public void LoadMed(string medId)
        {
            LoadMed(this.medicationController.GetById(medId));
        }

        public List<string> GetActivesAsList()
        {
            string[] actives = Actives.Split(',');
            return actives.ToList();
        }

        public List<string> GetAllergensAsList()
        {
            string[] allergens = Allergens.Split(',');
            return allergens.ToList();
         }

        public void LoadRequest(MedApprovalRequest request)
        {
            RequestId = request.Id;

            LoadMed(request.Medication);
        }

        public void LoadRequest(int id)
        {
            LoadRequest(this.approvalRequestController.GetById(id));
        }

        public void DenyRequest()
        {
            this.medicationController.UpdateMedication(Id, Code, BrandName, Dosage, GetActivesAsList(), Form, Note, GetAllergensAsList(), false, Classification, Indications, SideEffects, Instructions, Amount);
            this.approvalRequestController.DenyRequest(RequestId, Comment);
        }

        public void ApproveRequest()
        {
            this.approvalRequestController.ApproveRequest(RequestId);
        }
    }
}
