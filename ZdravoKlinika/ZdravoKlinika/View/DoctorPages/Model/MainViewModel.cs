using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class MainViewModel : ViewModelBase
    {
        private Doctor doctor;
        private ViewModelBase selectedVm;
        private Visibility settingsVisibility;
        private Visibility notifsVisibility;
        private Visibility notifsOpened;
        private int index;
        private Controller.EmployeeNotificationController notificationController;
        private NotifPanelViewModel notifPanelViewModel;
        private bool openedNotifs;

        public MyICommand ToggleSettings { get; set; }
        public MyICommand ToggleNotifs { get; set; }

        public MyICommand HomeCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand MedicationsCommand { get; set; }
        public MyICommand PatientsCommand { get; set; }
        public MyICommand ScheduleCommand { get; set; }
        public MyICommand FeedbackCommand { get; set; }

        public MyICommand SignOut { get; set; }

        public MainViewModel()
        {
            openedNotifs = false;
            this.notificationController = new Controller.EmployeeNotificationController();
            Doctor = Controller.RegisteredUserController.UserToDoctor(App.User);
            SettingsVisibility = Visibility.Collapsed;
            NotifsVisibility = Visibility.Collapsed;
            NotifsOpened = notificationController.HasEveryNotifBeenRead(Doctor.PersonalId) ? Visibility.Collapsed : Visibility.Visible;
            GoToHome();
            Index = 5;
            ToggleSettings = new MyICommand(ExecuteToggleSettings);
            ToggleNotifs = new MyICommand(ExecuteToggleNotifs);

            HomeCommand = new MyICommand(GoToHome);
            ProfileCommand = new MyICommand(GoToProfile);
            MedicationsCommand = new MyICommand(GoToMedications);
            PatientsCommand = new MyICommand(GoToPatients);
            ScheduleCommand = new MyICommand(GoToSchedule);
            FeedbackCommand = new MyICommand(GoToFeedback);
            NotifPanelViewModel = new NotifPanelViewModel();

            SignOut = new MyICommand(ExecuteSignOut, CanExecuteSignOut);
        }

        public void ExecuteToggleSettings()
        {
            SettingsVisibility = SettingsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            NotifsVisibility = Visibility.Collapsed;
        }

        public void ExecuteToggleNotifs()
        {
            SettingsVisibility = Visibility.Collapsed;
            NotifsVisibility = NotifsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            if(notifsOpened != Visibility.Collapsed)
            {
                this.notificationController.MarkAllPersonalNotificationsAsRead(Doctor.PersonalId);
            } else
            {
                NotifPanelViewModel.MarkAllAsRead();
            }
            NotifsOpened = Visibility.Collapsed;
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
            Navigation.Navigator.CloseMainAndOpenSignIn();
        }

        public bool CanExecuteSignOut()
        {
            return SettingsVisibility == Visibility.Visible;
        }



        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public ViewModelBase SelectedVm { get => selectedVm; set => SetProperty(ref selectedVm, value); }
        public Visibility SettingsVisibility { get => settingsVisibility; set => SetProperty(ref settingsVisibility, value); }
        public int Index { get => index; set => SetProperty(ref index, value); }
        public Visibility NotifsVisibility { get => notifsVisibility; set => SetProperty(ref notifsVisibility, value); }
        public Visibility NotifsOpened { get => notifsOpened; set => SetProperty(ref notifsOpened, value); }
        public NotifPanelViewModel NotifPanelViewModel { get => notifPanelViewModel; set => notifPanelViewModel = value; }
        public bool OpenedNotifs { get => openedNotifs; set => openedNotifs = value; }
    }
}
