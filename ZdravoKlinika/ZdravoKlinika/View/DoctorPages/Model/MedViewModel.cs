using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DialogHelper;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class MedViewModel : ViewModelBase
    {
        private static DoctorMedicationsViewModel parentViewModel;
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
        private string comment;
        private int amount;
        private MedicationController medicationController;
        private MedApprovalRequestController approvalRequestController;
        private int requestId;

        public MyICommand View { get; set; }
        public MyICommand Authorize { get; set; }

        public MyICommand Approve { get; set; }
        public MyICommand Deny { get; set; }
        public MyICommand Edit { get; set; }
        public MyICommand GiveUp { get; set; }

        private DialogHelper.DialogService dialogService;

        private Visibility visibility1;
        private Visibility visibility2;
        private bool editable;

        public MedViewModel()
        {
            this.MedicationController = new MedicationController();
            DialogService = new DialogHelper.DialogService();
            this.ApprovalRequestController = new MedApprovalRequestController();
            Authorize = new MyICommand(ExecuteAuthorization);
            Approve = new MyICommand(ApproveRequest);
            Deny = new MyICommand(DenyRequest);
            Edit = new MyICommand(ExecuteEdit);
            GiveUp = new MyICommand(ExecuteClose);
            View = new MyICommand(ExecuteView);
            Visibility1 = Visibility.Visible;
            Visibility2 = Visibility.Collapsed;
            Editable = false;

        }

        public void ExecuteView()
        {
            DialogService.ShowMedication(this);
        }

        public void ExecuteAuthorization()
        {
            DialogService.ShowMedRequestDialog(requestId);
        }

        public void ExecuteEdit()
        {
            Visibility2 = Visibility.Visible;
            Visibility1 = Visibility.Collapsed;
            Editable = true;
        }

        public void ExecuteClose()
        {
            ParentViewModel.Load();
            DialogService.CloseDialog(this);
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
        public string Comment { get => comment; set => SetProperty(ref comment, value); }
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
        public int RequestId { get => requestId; set => SetProperty(ref requestId, value); }
        internal MedicationController MedicationController { get => medicationController; set => medicationController = value; }
        public Visibility Visibility1 { get => visibility1; set => SetProperty(ref visibility1, value); }
        public Visibility Visibility2 { get => visibility2; set => SetProperty(ref visibility2, value); }
        public bool Editable { get => editable; set => SetProperty(ref editable, value); }
        public DialogService DialogService { get => dialogService; set => dialogService = value; }
        public DoctorMedicationsViewModel ParentViewModel { get => parentViewModel; set => parentViewModel = value; }

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
            ExecuteClose();
        }

        public void ApproveRequest()
        {
            this.approvalRequestController.ApproveRequest(RequestId);
            ExecuteClose();
        }
    }
}
