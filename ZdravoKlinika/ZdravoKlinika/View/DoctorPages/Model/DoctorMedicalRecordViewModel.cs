using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DoctorMedicalRecordViewModel : ViewModelBase
    {
        private RegisteredPatientController patientController;
        private RegisteredPatient patient;
        string name;
        string id;
        string gender;
        string dateOfBirth;
        string email;
        string phone;
        string address;
        string bloodType;
        string emergancyContactName;
        string emergancyContactPhone;
        private List<String> medicationDisplay;
        List<string> diagnosisDisplay;


        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Id { get => id; set => SetProperty(ref id, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string DateOfBirth { get => dateOfBirth; set => SetProperty(ref dateOfBirth, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string BloodType { get => bloodType; set => SetProperty(ref bloodType, value); }
        public string EmergancyContactName { get => emergancyContactName; set => SetProperty(ref emergancyContactName, value); }
        public string EmergancyContactPhone { get => emergancyContactPhone; set => SetProperty(ref emergancyContactPhone, value); }
        public RegisteredPatientController PatientController { get => patientController; set => patientController = value; }
        public List<string> DiagnosisDisplay { get => diagnosisDisplay; set => diagnosisDisplay = value; }
        public List<string> MedicationDisplay { get => medicationDisplay; set => SetProperty(ref medicationDisplay, value); }

        public DoctorMedicalRecordViewModel()
        {
            this.patientController = new RegisteredPatientController();
            this.MedicationDisplay = new List<string>();
            
        }

        public void init(string patientId)
        {
            this.id = patientId;
            this.patient = patientController.GetById(this.id);
            this.name = this.patient.GetPatientFullName();
            this.phone = this.patient.Phone;
            this.address = this.patient.Address.ToString();
            this.email = this.patient.Email;
            this.bloodType = this.patient.BloodType.ToString();
            this.gender = this.patient.Gender.ToString();
            this.emergancyContactName = this.patient.EmergencyContactName;
            this.emergancyContactPhone = this.patient.EmergencyContactPhone;
            this.dateOfBirth = this.patient.DateOfBirth.ToString("dd.MM.yyyy.");
            foreach(Medication m in this.patient.MedicalRecord.CurrentMedication)
            {
                this.MedicationDisplay.Add(m.ToString());
            }

        }

        public void MedicationAdded()
        {
            List<string> newMedList = new List<string>();
            this.patient = patientController.GetById(this.id);
            foreach (Medication m in this.patient.MedicalRecord.CurrentMedication)
            {
                newMedList.Add(m.ToString());
            }

            MedicationDisplay = newMedList;
        }
    }
}
