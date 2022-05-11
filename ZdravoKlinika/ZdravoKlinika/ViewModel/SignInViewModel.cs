using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.View;

namespace ZdravoKlinika.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {

        private RegisteredUser user;
        private RegisteredUserController registeredUserController;

        private string username;
        private string password;
        private bool remember;

        public RegisteredUser User { get => user; set => SetProperty(ref user, value); }
        public RegisteredUserController RegisteredUserController { get => registeredUserController; set => registeredUserController = value; }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public bool Remember { get => remember; set => SetProperty(ref remember, value); }

        public SignInViewModel()
        {
            username = "Npr. vaseime@zdravo.com";
            registeredUserController = new RegisteredUserController();
            if (this.SavedLogin())
            {
                User = registeredUserController.GetRememberedUser();
            } else
            {
                User = new RegisteredUser();
            }
        }

        public bool SavedLogin()
        {
            return registeredUserController.GetRememberedUser() != null;
        }

        public bool IsLoginSuccessful()
        {
            User = registeredUserController.GetUserByEmailAndPassword(Username, Password);
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
