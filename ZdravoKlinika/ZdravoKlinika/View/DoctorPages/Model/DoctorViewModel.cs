using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class DoctorViewModel : ViewModelBase
    {
        private Doctor doctor;
        private string name;
        private string specialty;
        private DoctorController controller;

        public DoctorViewModel(RegisteredUser doctor)
        {
            controller = new DoctorController();
            Doctor = controller.GetByEmail(doctor.Email);
            Name = Doctor.ToString();
            specialty = Doctor.Specialty;
            
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Specialty { get => specialty; set => SetProperty(ref specialty, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
    }
}
