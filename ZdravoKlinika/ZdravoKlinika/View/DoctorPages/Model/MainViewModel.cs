using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class MainViewModel : ViewModelBase
    {
        private Doctor doctor;
        private ViewModelBase selectedVm;
        private Visibility settingsVisibility;
        private int index;

        public MyICommand ToggleSettings { get; set; }

        public MyICommand HomeCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand MedicationsCommand { get; set; }
        public MyICommand PatientsCommand { get; set; }
        public MyICommand ScheduleCommand { get; set; }
        public MyICommand FeedbackCommand { get; set; }

        public MyICommand SignOut { get; set; }

        public MainViewModel()
        {
            Doctor = Controller.RegisteredUserController.UserToDoctor(App.User);
            GoToHome();
            settingsVisibility = Visibility.Collapsed;
            Index = 5;
            ToggleSettings = new MyICommand(ExecuteToggleSettings);

            HomeCommand = new MyICommand(GoToHome);
            ProfileCommand = new MyICommand(GoToProfile);
            MedicationsCommand = new MyICommand(GoToMedications);
            PatientsCommand = new MyICommand(GoToPatients);
            ScheduleCommand = new MyICommand(GoToSchedule);
            FeedbackCommand = new MyICommand(GoToFeedback);

            SignOut = new MyICommand(ExecuteSignOut, CanExecuteSignOut);
        }

        public void ExecuteToggleSettings()
        {
            SettingsVisibility = SettingsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            
        }

        public void GoToHome()
        {
            SelectedVm = new HomePageViewModel();
            Index = 0;
        }

        public void GoToProfile()
        {
            SelectedVm = new DoctorViewModel();
            Index = 1;
        }

        public void GoToMedications()
        {
            SelectedVm = new DoctorMedicationsViewModel();
            Index = 3;
        }

        public void GoToPatients()
        {
            SelectedVm = new AllPatientsViewModel();
            Index = 2;
        }

        public void GoToSchedule()
        {
            SelectedVm = new MonthlyViewModel();
            Index = 4;
        }

        public void GoToFeedback()
        {
            SelectedVm = new FeedbackViewModel();
            Index = 5;
        }

        public void ExecuteSignOut()
        {
            Controller.RegisteredUserController registeredUserController = new Controller.RegisteredUserController();
            registeredUserController.ForgetUser();
            DialogHelper.DialogService.CloseMainAndOpenSignIn();
        }

        public bool CanExecuteSignOut()
        {
            return SettingsVisibility == Visibility.Visible;
        }



        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public ViewModelBase SelectedVm { get => selectedVm; set => SetProperty(ref selectedVm, value); }
        public Visibility SettingsVisibility { get => settingsVisibility; set => SetProperty(ref settingsVisibility, value); }
        public int Index { get => index; set => SetProperty(ref index, value); }
    }
}
