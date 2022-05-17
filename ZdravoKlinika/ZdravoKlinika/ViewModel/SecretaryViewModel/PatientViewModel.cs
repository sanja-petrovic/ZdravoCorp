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

        public int ChoosePatient(String id, String name, String lastname) 
        {
            patientController = new PatientController();
            Patient patient = patientController.GetById(id);
            if (patient == null)
            {
                return 2;
            }
            else 
            {
                if (patient.GetName().Equals(name) && patient.GetLastname().Equals(lastname)) 
                {
                    selectedPatient = patient;
                    return 0;
                }
                return 1;
            }
        }

        public PatientViewModel()
        {
            init();
        }
        public void init()
        {
            selectedPatient = null;
        }
        public void CreateGuest(String id, String name, String lastname) 
        {
            patientController.CreateNewGuestPatient(id, name, lastname);
            selectedPatient = patientController.GetById(id);
        }

        internal void UpdatePatient()
        {
            patientController = new PatientController();
            Patient patient = patientController.GetById(SelectedPatient.GetPatientId());
            selectedPatient = patient;
        }
    }
}
