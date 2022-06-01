using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PatientViewModel : ViewModelBase
    {
        private RegisteredPatient patient;
        private string fullName;
        private string gender;
        private string id;
        public MyICommand RecordCommand { get; set; }

        public PatientViewModel(RegisteredPatient patient)
        {
            this.Patient = patient;
            this.fullName = patient.GetPatientFullName();
            this.gender = patient.GenderToString();
            this.id = patient.GetPatientId();
            RecordCommand = new MyICommand(GoToRecord);
        }

        public RegisteredPatient Patient { get => patient; set => SetProperty(ref patient, value); }
        public string FullName { get => fullName; set => SetProperty(ref fullName, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string Id { get => id; set => SetProperty(ref id, value); }

        public void GoToRecord()
        {
            Navigation.Navigator navigator = new Navigation.Navigator();
            navigator.ShowMedicalRecord(Id);
        }
    }
}
