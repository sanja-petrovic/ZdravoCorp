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
        private Controller.PatientNotesController notesController;
        private List<PatientMedicationNotification> notifs;
        private RegisteredPatient patient;
        private List<PatientNotes> notes;

        private ObservableCollection<String> notificationTexts;
        public PatientProfileViewModel(String patientId)
        {
            controller = new RegisteredPatientController();
            medicalRecordController = new MedicalRecordController();
            notifController = new Controller.PatientMedicationNotificationController();
            notesController = new Controller.PatientNotesController();
            notificationTexts = new ObservableCollection<string>();
            Patient = controller.GetById(patientId);
            if(Patient != null)
            {
                this.Name = Patient.Name;
                this.LastName = Patient.Lastname;
                this.DateOfBirth = Patient.DateOfBirth.ToString();
                this.Gender = Patient.Gender.ToString();
                this.PhoneNumber = Patient.Phone;
                this.Email = Patient.Email;
                this.FullName = Patient.Name + " " + Patient.Lastname;
                if (Patient.ProfilePicture != null)
                {
                    this.ProfilePictureLocation = "/Resources/Images/burger-bar.png";
                }
                else
                {
                    this.ProfilePictureLocation = null;
                }
                this.BloodType = Patient.BloodType.ToString();
                this.Ocupation = Patient.Occupation.ToString();
                this.EmergacyContact = Patient.EmergencyContactName;
                this.EmergencyNumber = Patient.EmergencyContactPhone;
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
        public PatientNotesController NotesController { get => notesController; set => notesController = value; }
        public List<PatientNotes> Notes { get => notes; set => notes = value; }
        public RegisteredPatient Patient { get => patient; set => patient = value; }
        internal PatientMedicationNotificationController NotifController { get => notifController; set => notifController = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
