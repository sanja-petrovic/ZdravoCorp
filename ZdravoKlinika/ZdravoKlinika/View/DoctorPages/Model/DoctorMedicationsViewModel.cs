using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DoctorMedicationsViewModel : ViewModelBase
    {
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public bool SelectAll { get => selectAll; set => SetProperty(ref selectAll, value); }
        public ObservableCollection<MedViewModel> PendingMeds { get => _pendingMeds; set => SetProperty(ref _pendingMeds, value); }
        public ObservableCollection<MedViewModel> ApprovedMeds { get => _approvedMeds; set => SetProperty(ref _approvedMeds, value); }
        public MedViewModel Selected { get => selected; set => SetProperty(ref selected, value); }

        private MedViewModel selected;
        private MedViewModel requestSelected;
        private int selectedTabIndex;

        private ObservableCollection<MedViewModel> _pendingMeds;
        private ObservableCollection<MedViewModel> _approvedMeds;

        public MyICommand Authorize { get; set; }
        public MyICommand View { get; set; }
        public MyICommand SwitchTab { get; set; }
        public MedViewModel RequestSelected { get => requestSelected; set => SetProperty(ref requestSelected, value); }
        public int SelectedTabIndex { get => selectedTabIndex; set => SetProperty(ref selectedTabIndex, value); }

        private static bool needsUpdating;
        private bool selectAll;
        private Doctor doctor;
        private MedicationController medicationController;
        private MedApprovalRequestController medApprovalRequestController;

        public DoctorMedicationsViewModel()
        {
            SelectedTabIndex = 0;
            this.doctor = RegisteredUserController.UserToDoctor(App.User);
            medicationController = new MedicationController();
            medApprovalRequestController = new MedApprovalRequestController();
            ApprovedMeds = new ObservableCollection<MedViewModel>();
            PendingMeds = new ObservableCollection<MedViewModel>();
            Authorize = new MyICommand(ExecuteAuthorize);
            View = new MyICommand(ExecuteView);
            SwitchTab = new MyICommand(ExecuteSwitch);
            Load();
        }

        public void ExecuteSwitch()
        {
            SelectedTabIndex = Math.Abs(SelectedTabIndex - 1);
        }

        public void ExecuteView()
        {
            Selected.ExecuteView();
        }

        public void ExecuteAuthorize()
        {
            if(RequestSelected != null)
            {
                RequestSelected.ExecuteAuthorization();
            }
        }

        public void Load()
        {
            LoadApproved();
            LoadPending();
        }

        public void LoadApproved()
        {
            ApprovedMeds = new ObservableCollection<MedViewModel>();
            foreach (Medication m in medicationController.GetByApprovedValue(true))
            {
                MedViewModel mViewModel = new MedViewModel();
                mViewModel.LoadMed(m);
                mViewModel.ParentViewModel = this;
                ApprovedMeds.Add(mViewModel);
            }
            Selected = ApprovedMeds.First();
        }

        public void LoadPending()
        {
            PendingMeds = new ObservableCollection<MedViewModel>();
            foreach (MedApprovalRequest r in medApprovalRequestController.GetPendingRequestsByReviewer(Doctor.PersonalId))
            {
                MedViewModel mViewModel = new MedViewModel();
                mViewModel.LoadRequest(r);
                mViewModel.ParentViewModel = this;
                PendingMeds.Add(mViewModel);
            }
            RequestSelected = PendingMeds.Count > 0 ? PendingMeds.First() : null;
        }

    }
}
