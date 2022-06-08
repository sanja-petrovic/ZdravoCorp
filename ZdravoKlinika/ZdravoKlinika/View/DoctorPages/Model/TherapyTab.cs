using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class TherapyTab : ViewModelBase, ITabViewModel
    {
        private string header;
        private int appointmentId;

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }

        private string patientName;
        private string patientId;

        private string doctorName;
        private string doctorSpecialty;

        private Medication medication;
        private int amount;
        private bool noAlt;
        private bool emergency;
        private int duration;
        private int frequency;
        private string singleDose;
        private string repeat;
        private string doctorsNote;
        private int selectedIndex;

        private AppointmentController appointmentController;
        private Appointment appointment;

        private RegisteredPatientController registeredPatientController;
        private MedicationController medicationController;
        private PrescriptionController prescriptionController;
        private PatientMedicationNotificationController notificationController;
        private List<Medication> medications;
        private List<String> medicationsDisplay;
        private List<String> repeatDisplay;
        private Doctor doctor;
        private IPatient patient;
        private ObservableCollection<Medication> prescribedMedList;
        private ObservableCollection<PrescriptionViewModel> prescribedList;
        private Visibility allergyVisibility;

        public MyICommand ConfirmCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        
        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public Medication Medication { get => medication; set  { SetProperty(ref medication, value);
            if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); } } }
        public int Amount { get => amount; set { SetProperty(ref amount, value); if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); } } }
        public bool NoAlt { get => noAlt; set => SetProperty(ref noAlt, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public int Duration { get => duration; set
            {
                SetProperty(ref duration, value);
                if(AddCommand != null) { if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); } }
            }
        }
        public int Frequency { get => frequency; set {
                SetProperty(ref frequency, value);
                if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
            } }
        public string SingleDose { get => singleDose; set {
                SetProperty(ref singleDose, value);
                if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
            } }
        public string Repeat { get => repeat; set {
                SetProperty(ref repeat, value);
                if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
            } }
        public string DoctorsNote
        {
            get => doctorsNote; set
            {
                SetProperty(ref doctorsNote, value);
                if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
            }
        }
        public List<Medication> Medications { get => medications; set => medications = value; }
        public List<string> MedicationsDisplay { get => medicationsDisplay; set => medicationsDisplay = value; }
        public Doctor Doctor
        {
            get => doctor; set
            {
                SetProperty(ref doctor, value);
            }
        }
        public IPatient Patient { get => patient; set => SetProperty(ref patient, value); }
        public List<string> RepeatDisplay { get => repeatDisplay; set => SetProperty(ref repeatDisplay, value); }
        public int SelectedIndex { get => selectedIndex; set {
                SetProperty(ref selectedIndex, value);
                if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
            } }
        public ObservableCollection<PrescriptionViewModel> PrescribedList { get => prescribedList; set => SetProperty(ref prescribedList, value); }
        public ObservableCollection<Medication> PrescribedMedList { get => prescribedMedList; set => SetProperty(ref prescribedMedList, value); }
        public Visibility AllergyVisibility { get => allergyVisibility; set => SetProperty(ref allergyVisibility, value); }

        public TherapyTab()
        {
            this.appointmentController = new AppointmentController();
            this.medicationController = new MedicationController();
            this.registeredPatientController = new RegisteredPatientController();
            this.prescriptionController = new PrescriptionController();
            this.notificationController = new PatientMedicationNotificationController();
            this.repeatDisplay = new List<string>();
            this.repeatDisplay.Add("dnevno");
            this.repeatDisplay.Add("nedeljno");
            this.repeatDisplay.Add("mesečno");

            Medications = medicationController.GetByApprovedValue(true);
            this.MedicationsDisplay = new List<String>();
            foreach (Medication m in this.medications)
            {
                MedicationsDisplay.Add(m.BrandName + " " + m.Dosage + ", " + m.Form);
            }
            this.PrescribedMedList = new ObservableCollection<Medication>();
            this.PrescribedList = new ObservableCollection<PrescriptionViewModel>();
            Doctor = RegisteredUserController.UserToDoctor(App.User);
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
            AddCommand = new MyICommand(ExecuteAdd, CanAdd);
        } 

        public void ExecuteConfirm()
        {
            Save();
            DialogHelper.DialogService.CloseDialog(this);
            Messenger.Messenger.SuccessMessage("Uspešno ste prepisali lek!");
        }

        public bool CanAdd()
        {
            return AllergyCheck() && Duration > 0 && Frequency > 0 && SingleDose != null && Amount > 0 && Repeat != null;
        }

        public void Reset()
        {
            Duration = 0;
            Frequency = 0;
            SingleDose = null;
            Medication = null;
            Amount = 0;
            Emergency = false;
            Repeat = null;
            DoctorsNote = null;
            if(AddCommand != null) { AddCommand.RaiseCanExecuteChanged(); }
        }

        public void ExecuteAdd()
        {
            int i = PrescribedMedList.ToList().FindIndex(m => m.MedicationId == Medication.MedicationId);
            if (i != -1)
            {
                PrescribedList.RemoveAt(i);
                PrescribedMedList.RemoveAt(i);
            }
            Add();
            Reset();
        }

        public void Delete()
        {
            int i = PrescribedMedList.ToList().FindIndex(m => m.MedicationId == Medication.MedicationId);
            if (i != -1)
            {
                PrescribedList.RemoveAt(i);
                PrescribedMedList.RemoveAt(i);
            }
            Reset();
        }

        public void ExecuteGiveUp()
        {
            DialogHelper.DialogService dialogService = new DialogHelper.DialogService();
            dialogService.ShowPrompt("Odustanak od prepisivanja", "Da li ste sigurni da želite da odustanete od prepisivanja leka?", Close, "Da, želim", "Ne, vrati me nazad");
        }

        public void Close()
        {
            DialogHelper.DialogService.CloseDialog(this);

        }
        public void Save()
        {
            foreach(PrescriptionViewModel p in PrescribedList)
            {
                this.prescriptionController.Prescribe(p.Prescription);
                this.notificationController.CreateNotification(p.Prescription.Doctor, registeredPatientController.GetById(p.Prescription.Patient.GetPatientId()), "", p.Prescription, "", DateTime.Now.AddHours(1));
            }
        }

        public bool AllergyCheck()
        {
            bool retVal = true;
            if(Medication != null)
            {
                if (patient.GetPatientType().Equals(PatientType.Registered))
                {
                    retVal = !(registeredPatientController.IsAllergic(Medication.MedicationId, patientId));
                }
            }

            AllergyVisibility = retVal == false ? Visibility.Visible : Visibility.Collapsed;

            return retVal;
        }

        public void LoadPrescription(Prescription prescription)
        {
            Medication = prescription.Medication;
            Amount = prescription.Amount;
            Frequency = prescription.Frequency;
            Duration = prescription.Duration;
            SingleDose = prescription.SingleDose;
            DoctorsNote = prescription.DoctorsNote;
            Repeat = prescription.Repeat;
        }


        public void LoadFromRecord(string patientId)
        {
            this.patient = registeredPatientController.GetById(patientId);
            this.patientId = this.patient.GetPatientId();
            this.patientName = this.patient.GetPatientFullName();
            this.doctorName = this.doctor.ToString();
        }

        public void Load()
        {
            this.appointment = appointmentController.GetAppointmentById(appointmentId);
            this.doctor = appointment.Doctor;
            this.patient = appointment.Patient;
            this.patientId = this.appointment.Patient.GetPatientId();
            this.patient = this.appointment.Patient;
            this.patientName = this.appointment.Patient.GetPatientFullName();
            this.doctorName = this.appointment.Doctor.ToString();
            this.doctorSpecialty = this.appointment.Doctor.Specialty;
        }


        public void Add()
        {
            PrescribedMedList.Add(Medication);
            Prescription p = new Prescription(Doctor, Patient, Medication, this.amount, this.duration, this.frequency, this.singleDose, this.repeat, this.doctorsNote, -1);
            PrescribedList.Add(new PrescriptionViewModel { Prescription = p, Parent = this });
        }
    }
}
