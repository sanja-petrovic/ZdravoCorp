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
        public ObservableCollection<MedViewModel> ApprovedMeds { get; set; }
        public ObservableCollection<MedViewModel> PendingMeds { get; set; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        

        private Doctor doctor;
        private MedicationController medicationController;
        private MedApprovalRequestController medApprovalRequestController;

        public DoctorMedicationsViewModel(Doctor doctor)
        {
            this.doctor = doctor;
            medicationController = new MedicationController();
            medApprovalRequestController = new MedApprovalRequestController();
            ApprovedMeds = new ObservableCollection<MedViewModel>();
            PendingMeds = new ObservableCollection<MedViewModel>();
            Load();
        }

        public void Load()
        {
            foreach(Medication m in medicationController.GetApproved())
            {
                MedViewModel mViewModel = new MedViewModel();
                mViewModel.LoadMed(m);
                ApprovedMeds.Add(mViewModel);
            }

            foreach(MedApprovalRequest r in medApprovalRequestController.GetPendingRequestsByReviewer(Doctor.PersonalId))
            {
                MedViewModel mViewModel = new MedViewModel();
                mViewModel.LoadRequest(r);
                PendingMeds.Add(mViewModel);
            }
        }
    }
}
