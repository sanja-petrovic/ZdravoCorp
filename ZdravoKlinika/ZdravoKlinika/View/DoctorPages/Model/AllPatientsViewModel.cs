using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AllPatientsViewModel : ViewModelBase
    {
        public ObservableCollection<PatientViewModel> Patients { get; set; }
        private RegisteredPatientController controller;

        public AllPatientsViewModel()
        {
            this.controller = new RegisteredPatientController();
            Patients = new ObservableCollection<PatientViewModel>();
            foreach(RegisteredPatient patient in this.controller.GetAll())
            {
                Patients.Add(new PatientViewModel(patient));
            }
        }

    }
}
