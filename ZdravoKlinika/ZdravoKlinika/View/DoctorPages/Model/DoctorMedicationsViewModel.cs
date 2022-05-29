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

        private ObservableCollection<MedViewModel> _pendingMeds;
        private ObservableCollection<MedViewModel> _approvedMeds;

        private static bool needsUpdating;
        private bool selectAll;
        private Doctor doctor;
        private MedicationController medicationController;
        private MedApprovalRequestController medApprovalRequestController;

        public DoctorMedicationsViewModel()
        {
            this.doctor = RegisteredUserController.UserToDoctor(App.User);
            medicationController = new MedicationController();
            medApprovalRequestController = new MedApprovalRequestController();
            ApprovedMeds = new ObservableCollection<MedViewModel>();
            PendingMeds = new ObservableCollection<MedViewModel>();
            Load();
        }

        public void Load()
        {
            LoadApproved();
            LoadPending();
        }

        public void LoadApproved()
        {
            ApprovedMeds = new ObservableCollection<MedViewModel>();
            foreach (Medication m in medicationController.GetApproved())
            {
                MedViewModel mViewModel = new MedViewModel();
                mViewModel.LoadMed(m);
                mViewModel.ParentViewModel = this;
                ApprovedMeds.Add(mViewModel);
            }
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
        }

    }
}
