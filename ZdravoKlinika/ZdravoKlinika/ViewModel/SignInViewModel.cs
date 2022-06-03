using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.View;
using ZdravoKlinika.View.Navigation;
using GalaSoft.MvvmLight.CommandWpf;

namespace ZdravoKlinika.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        private RegisteredUser user;
        private RegisteredUserController registeredUserController;
        private RegisteredPatientController registeredPatientController;

        private string username;
        private string password;
        private bool remember;
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand LogInCommand { get; set; }
        public MyICommand RememberMe { get; set; }

        public RegisteredUser User { get => user; set => SetProperty(ref user, value); }
        public RegisteredUserController RegisteredUserController { get => registeredUserController; set => registeredUserController = value; }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public bool Remember { get => remember; set => SetProperty(ref remember, value); }
        public RegisteredPatientController RegisteredPatientController { get => registeredPatientController; set => registeredPatientController = value; }



        public SignInViewModel()
        {
            LogInCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(LogIn, CanLogIn);
            RememberMe = new MyICommand(ExecuteRemember);
            username = "Npr. vaseime@zdravo.com";
            registeredUserController = new RegisteredUserController();
            RegisteredPatientController = new RegisteredPatientController();
            if (SavedLogin())
            {
                User = registeredUserController.GetRememberedUser();
                LogIn();
            }
            else
            {
                User = new RegisteredUser();
            }
        }

        public void ExecuteRemember()
        {
            Remember = !Remember;
            LogInCommand.RaiseCanExecuteChanged();
        }

        public bool CanLogIn()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && IsLoginSuccessful();
        }

        public void LogIn()
        {
            {
                App.User = User;
                switch (User.UserType)
                {
                    case UserType.Patient:
                        Navigator.ShowPatientWindow(RegisteredPatientController);
                        break;
                    case UserType.Secretary:
                        Navigator.ShowSecretaryWindow();
                        break;
                    case UserType.Doctor:
                        Navigator.ShowDoctorWindow();
                        break;
                    case UserType.Manager:
                        Navigator.ShowManagerWindow();
                        break;
                    default:
                        break;
                }
                if (Remember)
                {
                    RememberUser();
                }

            }
        }

        public bool SavedLogin()
        {
            return registeredUserController.GetRememberedUser() != null;
        }

        public bool IsLoginSuccessful()
        {
            User = registeredUserController.GetUserByEmailAndPassword(Username.Trim(), Password);
            return User != null;
        }

        public RegisteredUser GetUser()
        {
            return User;
        }

        public void RememberUser()
        {
            registeredUserController.RememberUser(User);
        }

    }
}
