using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ApptLogViewModel : ViewModelBase
    {
        public ObservableCollection<ITabViewModel> TabViewModels { get; set; }
        private int appointmentId;
        private ITabViewModel selectedViewModel;
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }
        public ApptLogViewModel()
        {
            TabViewModels = new ObservableCollection<ITabViewModel>();
        }

        public void Init()
        {
            TabViewModels.Add(new AnamnesisTab { Header = "Anamneza", AppointmentId = appointmentId }) ;
            TabViewModels.Add(new TherapyTab { Header = "Terapija", AppointmentId = appointmentId });
            TabViewModels.Add(new ReferralTab { Header = "Uput za specijalistu", AppointmentId = appointmentId });
            selectedViewModel = TabViewModels[0];
            foreach(ITabViewModel tabViewModel in TabViewModels)
            {
                tabViewModel.Load();
            }
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
        }

        public void ExecuteGiveUp()
        {
            DialogHelper.DialogService.CloseDialog(this);
        }
        
        public void ExecuteConfirm()
        {
            ((TherapyTab)TabViewModels[1]).Save();
            ((AnamnesisTab)TabViewModels[0]).Appointment.Prescriptions = ((TherapyTab)TabViewModels[1]).PrescribedList.ToList();
            ((AnamnesisTab)TabViewModels[0]).Save();
            DialogHelper.DialogService.CloseDialog(this);
            Messenger.Messenger.SuccessMessage("Uspešno ste upisali pregled!");
        }

        public ITabViewModel SelectedViewModel { get => selectedViewModel; set => SetProperty(ref selectedViewModel, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
    }
}
