using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DoctorMedicalRecordViewModel : ViewModelBase
    {
        private RegisteredPatientController patientController;
        private AppointmentController appointmentController;
        private RegisteredPatient patient;
        private PrescriptionController prescriptionController;
        private MedicalRecordController medicalRecordController;
        public ObservableCollection<PastViewModel> PastAppointments { get; set; }
        public ObservableCollection<UpcomingViewModel> UpcomingAppointments { get; set; }
        public ObservableCollection<PrescriptionViewModel> Prescriptions { get; set; }
        public ObservableCollection<string> Diagnoses { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
        string name;
        string id;
        string gender;
        string dateOfBirth;
        string email;
        string phone;
        string address;
        string bloodType;
        string emergencyContactName;
        string emergencyContactPhone;

        private PrescriptionViewModel selectedPrescription;

        public MyICommand DownloadPrescription { get; set; }
        public MyICommand SelectFirstPrescription { get; set; }


        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Id { get => id; set => SetProperty(ref id, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string DateOfBirth { get => dateOfBirth; set => SetProperty(ref dateOfBirth, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string BloodType { get => bloodType; set => SetProperty(ref bloodType, value); }
        public string EmergencyContactName { get => emergencyContactName; set => SetProperty(ref emergencyContactName, value); }
        public string EmergencyContactPhone { get => emergencyContactPhone; set => SetProperty(ref emergencyContactPhone, value); }
        public RegisteredPatientController PatientController { get => patientController; set => patientController = value; }
        public RegisteredPatient Patient { get => patient; set => SetProperty(ref patient, value); }
        public PrescriptionViewModel SelectedPrescription { get => selectedPrescription; set => SetProperty(ref selectedPrescription, value); }

        public DoctorMedicalRecordViewModel()
        {
            this.patientController = new RegisteredPatientController();
            this.appointmentController = new AppointmentController();
            PastAppointments = new ObservableCollection<PastViewModel>();
            this.UpcomingAppointments = new ObservableCollection<UpcomingViewModel>();
            this.prescriptionController = new PrescriptionController();
            this.medicalRecordController = new MedicalRecordController();
            Prescriptions = new ObservableCollection<PrescriptionViewModel>();
            DownloadPrescription = new MyICommand(ExecuteDownload);
            SelectFirstPrescription = new MyICommand(ExecuteSelectFirst);
        }

        public void ExecuteDownload()
        {
            if(SelectedPrescription == null)
            {
                SelectedPrescription = Prescriptions[0];
            }

            SelectedPrescription.ExecuteExport();
        }

        public void ExecuteSelectFirst()
        {
            SelectedPrescription = Prescriptions[0];
        }

        public void init(string patientId)
        {
            this.id = patientId;
            this.Patient = patientController.GetById(this.id);
            this.name = this.Patient.GetPatientFullName();
            this.phone = this.Patient.Phone;
            this.address = this.Patient.Address.ToString();
            this.email = this.Patient.Email;
            this.bloodType = this.Patient.BloodTypeToString();
            this.gender = this.Patient.GenderToString();
            this.emergencyContactName = this.Patient.EmergencyContactName;
            this.emergencyContactPhone = this.Patient.EmergencyContactPhone;
            this.dateOfBirth = this.Patient.DateOfBirth.ToString("dd.MM.yyyy.");

            foreach (Appointment a in this.appointmentController.GetPatientsPastAppointments(Patient))
            {
                PastViewModel past = new PastViewModel();
                past.Init(a);
                PastAppointments.Add(past);
            }

            foreach (Appointment a in this.appointmentController.GetPatientsUpcomingAppointments(Patient))
            {
                UpcomingViewModel upcoming = new UpcomingViewModel();
                upcoming.init(a);
                UpcomingAppointments.Add(upcoming);
            }

            foreach(Prescription p in this.prescriptionController.GetByPatient(patientId))
            {
                PrescriptionViewModel prescriptionViewModel = new PrescriptionViewModel(p);
                Prescriptions.Add(prescriptionViewModel);
            }

            Diagnoses = new ObservableCollection<string>(this.medicalRecordController.GetDiagnosesAndAllergies(Id));
            Medications = new ObservableCollection<Medication>(this.medicalRecordController.GetById(patientId).CurrentMedication);

        }

        public void MedicationAdded()
        {
            /*foreach (Medication m in Patient.MedicalRecord.CurrentMedication)
            {
                newMedList.Add(m.ToString());
            }*/

        }

        public void Edited()
        {
            ObservableCollection<PastViewModel> pastNew = new ObservableCollection<PastViewModel>();
            foreach (Appointment a in this.appointmentController.GetPatientsPastAppointments(Patient))
            {
                PastViewModel past = new PastViewModel();
                past.Init(a);
                pastNew.Add(past);
            }

            PastAppointments = pastNew;
        }

    }
}
