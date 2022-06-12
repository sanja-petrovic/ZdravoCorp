using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public  class AddPatientViewModel : ViewModelBase
    {
        private string name;
        private string lastname;
        private string pid;
        private string email;
        private string password;
        private string phone;

        public AddPatientViewModel() 
        {
        
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Lastname { get => lastname; set => SetProperty(ref lastname, value); }
        public string Pid { get => pid; set => SetProperty(ref pid, value); }
        public string Email { get => email; set => SetProperty(ref email, value);}
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value);}
    }
}
