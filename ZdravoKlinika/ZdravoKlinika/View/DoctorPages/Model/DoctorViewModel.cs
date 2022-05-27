using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class DoctorViewModel : ViewModelBase
    {
        private Doctor doctor;
        private string name;
        private string specialty;

        public DoctorViewModel()
        {
            Doctor = RegisteredUserController.UserToDoctor(App.User);
            
        }

        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
    }
}
