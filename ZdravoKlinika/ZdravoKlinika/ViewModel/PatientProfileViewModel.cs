using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.ViewModel
{
    internal class PatientProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        private string lastName;
        private string parentName;
        private string dateOfBirth;
        private string gender;
        private string phoneNumber;
        private string email;
        private string profilePictureLocation;
        private string bloodType;
        private string ocupation;
        private string emergacyContact;
        private string emergencyNumber;

        private List<String> alergies;
        private List<String> diagnoses;

        private RegisteredPatientController controller = new RegisteredPatientController();
        
        //private List<PatientMedicationNotification> notifs;

        public PatientProfileViewModel(String patientId)
        {
            RegisteredPatient patient = controller.GetById(patientId);
            if(patient != null)
            {
                this.Name = patient.Name;
                this.LastName = patient.Lastname;
                this.DateOfBirth = patient.DateOfBirth.ToString();
                this.Gender = patient.Gender.ToString();
                this.PhoneNumber = patient.Phone;
                this.Email = patient.Email;
                if (patient.ProfilePicture != null)
                {
                    this.ProfilePictureLocation = ProfilePictureLocation;
                }
                else
                {
                    this.ProfilePictureLocation = null;
                }
                this.BloodType = patient.BloodType.ToString();
                this.Ocupation = patient.Occupation.ToString();
                this.EmergacyContact = patient.EmergencyContactName;
                this.EmergencyNumber = patient.EmergencyContactPhone;
            }
            else
            {
                this.Name = "nepostojeci pacijent";
            }

        }

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ParentName { get => parentName; set => parentName = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string ProfilePictureLocation { get => profilePictureLocation; set => profilePictureLocation = value; }
        public string BloodType { get => bloodType; set => bloodType = value; }
        public string Ocupation { get => ocupation; set => ocupation = value; }
        public string EmergacyContact { get => emergacyContact; set => emergacyContact = value; }
        public string EmergencyNumber { get => emergencyNumber; set => emergencyNumber = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
