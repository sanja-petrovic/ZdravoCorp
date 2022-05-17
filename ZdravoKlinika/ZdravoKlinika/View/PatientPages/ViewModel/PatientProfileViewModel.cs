using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.PatientPages.ViewModel
{
    internal class PatientProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        private string lastName;
        private string fullName;
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

        private RegisteredPatientController controller;
        private MedicalRecordController medicalRecordController;
        private Controller.PatientMedicationNotificationController notifController;
        private List<PatientMedicationNotification> notifs;

        private ObservableCollection<String> notificationTexts;
        public PatientProfileViewModel(String patientId)
        {
            controller = new RegisteredPatientController();
            medicalRecordController = new MedicalRecordController();
            notifController = new Controller.PatientMedicationNotificationController();
            notificationTexts = new ObservableCollection<string>();
            RegisteredPatient patient = controller.GetById(patientId);
            if(patient != null)
            {
                this.Name = patient.Name;
                this.LastName = patient.Lastname;
                this.DateOfBirth = patient.DateOfBirth.ToString();
                this.Gender = patient.Gender.ToString();
                this.PhoneNumber = patient.Phone;
                this.Email = patient.Email;
                this.FullName = patient.Name + " " + patient.Lastname;
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
                this.alergies = medicalRecordController.GetById(patientId).Allergies;
                this.diagnoses = medicalRecordController.GetById(patientId).Diagnoses;

            }
            else
            {
                this.Name = "nepostojeci pacijent";
            }
            Notifs = NotifController.GetAll();
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
        public List<PatientMedicationNotification> Notifs { get => notifs; set => notifs = value; }
        public ObservableCollection<string> NotificationTexts 
        { 
            get => notificationTexts;
            set
            {
                notificationTexts = value;
                NotifyPropertyChanged("notifTexts");
            }
        }

        public string FullName { get => fullName; set => fullName = value; }
        public List<string> Alergies { get => alergies; set => alergies = value; }
        public List<string> Diagnoses { get => diagnoses; set => diagnoses = value; }
        internal PatientMedicationNotificationController NotifController { get => notifController; set => notifController = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
