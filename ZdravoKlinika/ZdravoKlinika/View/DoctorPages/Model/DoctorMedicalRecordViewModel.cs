using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DoctorMedicalRecordViewModel : ViewModelBase
    {
        private RegisteredPatientController patientController;
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

        public DoctorMedicalRecordViewModel()
        {
            this.patientController = new RegisteredPatientController();
        }

        public void init(string patientId)
        {
            this.id = patientId;
            RegisteredPatient patient = patientController.GetById(this.id);
            this.name = patient.GetPatientFullName();
            this.phone = patient.Phone;
            this.address = patient.Address.ToString();
            this.email = patient.Email;
            this.bloodType = patient.BloodType.ToString();
            this.gender = patient.Gender.ToString();
            this.emergancyContactName = patient.EmergencyContactName;
            this.emergancyContactPhone = patient.EmergencyContactPhone;
            this.dateOfBirth = patient.DateOfBirth.ToString("dd.MM.yyyy.");

        }
    }
}
