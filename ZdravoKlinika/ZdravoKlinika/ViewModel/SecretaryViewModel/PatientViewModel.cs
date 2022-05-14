using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.ViewModel.SecretaryViewModel
{
    public class PatientViewModel : View.ViewModelBase
    {
        Patient selectedPatient;
        PatientController patientController;

        public Patient SelectedPatient { get => selectedPatient; set => selectedPatient = value; }
        public PatientController PatientController { get => patientController; set => patientController = value; }

        public bool ChoosePatient(String id, String name, String lastname) 
        {
            Patient patient = patientController.GetById(id);
            if (patient == null)
            {
                patientController.CreateNewGuestPatient(id, name, lastname);
                selectedPatient = patientController.GetById(id);
                return true;
            }
            else 
            {
                if (patient.GetName().Equals(name) && patient.GetLastname().Equals(lastname)) 
                {
                    selectedPatient = patient;
                }
            }
            return false;
        }

        public PatientViewModel()
        {
            init();
        }
        public void init() 
        {
            patientController = new PatientController();
            selectedPatient = null;
        }
    }
}
