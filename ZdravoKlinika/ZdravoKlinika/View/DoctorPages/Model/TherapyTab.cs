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

        private string medication;
        private int amount;
        private bool noAlt;
        private bool emergency;
        private int duration;
        private int frequency;
        private string singleDose;
        private string repeat;
        private string doctorsNote;

        private AppointmentController appointmentController;
        private Appointment appointment;

        private RegisteredPatientController registeredPatientController;
        private MedicationController medicationController;
        private PrescriptionController prescriptionController;
        private List<Medication> medications;
        private List<String> medicationsDisplay;
        private List<String> repeatDisplay;
        private Doctor doctor;
        private IPatient patient;
        public ObservableCollection<Medication> PrescribedMedList { get; set; }
        public ObservableCollection<Prescription> PrescribedList { get; set; } 

        public MyICommand ConfirmCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public string Medication { get => medication; set => SetProperty(ref medication, value); }
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
        public bool NoAlt { get => noAlt; set => SetProperty(ref noAlt, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public int Frequency { get => frequency; set => SetProperty(ref frequency, value); }
        public string SingleDose { get => singleDose; set => SetProperty(ref singleDose, value); }
        public string Repeat { get => repeat; set => SetProperty(ref repeat, value); }
        public string DoctorsNote { get => doctorsNote; set => SetProperty(ref doctorsNote, value); }
        public List<Medication> Medications { get => medications; set => medications = value; }
        public List<string> MedicationsDisplay { get => medicationsDisplay; set => medicationsDisplay = value; }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public IPatient Patient { get => patient; set => SetProperty(ref patient, value); }
        public List<string> RepeatDisplay { get => repeatDisplay; set => SetProperty(ref repeatDisplay, value); }

        public TherapyTab()
        {
            this.appointmentController = new AppointmentController();
            this.medicationController = new MedicationController();
            this.registeredPatientController = new RegisteredPatientController();
            this.prescriptionController = new PrescriptionController();
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
            this.PrescribedList = new ObservableCollection<Prescription>();
            Doctor = RegisteredUserController.UserToDoctor(App.User);
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
        } 

        public void ExecuteConfirm()
        {
            Save();
            DialogHelper.DialogService.CloseDialog(this);
        }

        public void ExecuteGiveUp()
        {
            DialogHelper.DialogService.CloseDialog(this);
        }

        public void Save()
        {
            foreach(Prescription p in PrescribedList)
            {
                this.prescriptionController.Prescribe(p);
            }
        }

        public bool AllergyCheck(int selectedIndex)
        {
            if(patient.GetPatientType().Equals(PatientType.Registered))
            {
                return !(registeredPatientController.IsAllergic(this.medications[selectedIndex].MedicationId, patientId));
            } else
            {
                return true;
            }
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


        public void Add(int selectedIndex)
        {
            PrescribedMedList.Add(this.medications[selectedIndex]);
            PrescribedList.Add(new Prescription(Doctor, Patient, this.medications[selectedIndex], this.amount, this.duration, this.frequency, this.singleDose, this.repeat, this.doctorsNote, -1));
        }
    }
}
