using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class ChoosePatientViewModel : ViewModelBase
    {
        private String name;
        private String lastname;
        private String pid;
        public ChoosePatientViewModel() 
        {
            
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Lastname { get => lastname; set => SetProperty(ref lastname, value); }
        public string Pid { get => pid; set => SetProperty(ref pid, value); }
    }
}
