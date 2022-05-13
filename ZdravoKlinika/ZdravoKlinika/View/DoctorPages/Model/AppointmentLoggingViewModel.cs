using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class AppointmentLoggingViewModel : ViewModelBase
    {
        private AppointmentController appointmentController;
        private Appointment appointment;
        private int appointmentId;
        private string patientName;
        private string patientId;
        private string doctorName;
        private string doctorSpecialty;
        private string dateTime;
        private string room;
        private string prescription;

        private string diagnoses;
        private string doctorsNote;

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public string DateTime { get => dateTime; set => SetProperty(ref dateTime, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string DoctorsNote { get => doctorsNote; set => SetProperty(ref doctorsNote, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public AppointmentController AppointmentController { get => appointmentController; set => SetProperty(ref appointmentController, value); }
        public Appointment Appointment { get => appointment; set => SetProperty(ref appointment, value); }
        public string Prescription { get => prescription; set => SetProperty(ref prescription, value);  }
        public DoctorController DoctorController { get => doctorController; set => doctorController = value; }
        public RegisteredPatientController RegisteredPatientController { get => registeredPatientController; set => registeredPatientController = value; }
        internal MedicationController MedicationController { get => medicationController; set => medicationController = value; }
        internal PrescriptionController PrescriptionController { get => prescriptionController; set => prescriptionController = value; }
        public List<Medication> Medications { get => medications; set => medications = value; }
        public List<string> MedicationsDisplay { get => medicationsDisplay; set => medicationsDisplay = value; }
        public List<string> RepeatDisplay { get => repeatDisplay; set => repeatDisplay = value; }
        public List<Prescription> Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string Medication { get => medication; set => SetProperty(ref medication, value);  }
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public int Frequency { get => frequency; set => SetProperty(ref frequency, value); }
        public string SingleDose { get => singleDose; set => SetProperty(ref singleDose, value); }
        public string Repeat { get => repeat; set => SetProperty(ref repeat, value); }
        public string PrescriptionNote { get => prescriptionNote; set => SetProperty(ref prescriptionNote, value); }
        public List<Medication> PrescribedMedication { get => prescribedMedication; set => SetProperty(ref prescribedMedication, value); }
        public string AddedItemDisplay { get => addedItemDisplay; set => SetProperty(ref addedItemDisplay, value); }
        public List<string> PrescriptionDisplay { get => prescriptionDisplay; set => SetProperty(ref prescriptionDisplay, value); }

        private List<Prescription> prescriptions;
        private List<String> prescriptionDisplay;
        private List<Medication> prescribedMedication;

        private DoctorController doctorController;
        private RegisteredPatientController registeredPatientController;
        private MedicationController medicationController;
        private PrescriptionController prescriptionController;
        private List<Medication> medications;
        private List<String> medicationsDisplay;
        private List<String> repeatDisplay;
        private string addedItemDisplay;

        private string medication;
        private int amount;
        private int duration;
        private int frequency;
        private string singleDose;
        private string repeat;
        private string prescriptionNote;

        public AppointmentLoggingViewModel()
        {
            this.AppointmentController = new AppointmentController();
            this.MedicationController = new MedicationController();
            this.DoctorController = new DoctorController();
            this.RegisteredPatientController = new RegisteredPatientController();
            this.PrescriptionController = new PrescriptionController();
            this.RepeatDisplay = new List<string>();
            this.RepeatDisplay.Add("dnevno");
            this.RepeatDisplay.Add("nedeljno");
            this.RepeatDisplay.Add("mesečno");

            Medications = MedicationController.GetAll();
            this.MedicationsDisplay = new List<String>();
            foreach (Medication m in this.Medications)
            {
                MedicationsDisplay.Add(m.BrandName + " " + m.Dosage + ", " + m.Form);
            }
            this.prescriptions = new List<Prescription>();
            this.prescribedMedication = new List<Medication>();
            this.prescriptionDisplay = new List<string>();
        }

        public void load()
        {
            this.appointment = appointmentController.GetAppointmentById(appointmentId);
            this.patientName = appointment.Patient.GetPatientFullName();
            this.patientId = appointment.Patient.GetPatientId();
            this.doctorName = "Dr " + appointment.Doctor.Name + " " + appointment.Doctor.Lastname;
            this.doctorSpecialty = appointment.Doctor.Specialty;
            this.dateTime = appointment.DateAndTime.ToString("dd.MM.yyyy HH:mm");
            this.room = "Soba " + appointment.Room.Name;
            this.diagnoses = appointment.Diagnoses;
            this.doctorsNote = appointment.DoctorsNotes;
        }

        public void PrescriptionAdded(int selectedIndex, string repeat, string note)
        {
            Prescription prescription = new Prescription(doctorController.GetById("456"), registeredPatientController.GetById(patientId),
                medicationController.GetById(this.medications[selectedIndex].MedicationId), this.amount, this.duration, this.frequency, this.singleDose, repeat, note, -1
                );
            this.prescriptions.Add(prescription);
            List<string> newDisplay = this.prescriptionDisplay;
            newDisplay.Add(prescription.Medication.ToString());
            this.prescriptionDisplay = newDisplay;
        }
        
        public void save(String note)
        {
            foreach(Prescription prescription in this.prescriptions)
            {
                prescriptionController.Prescribe(prescription);
                this.prescribedMedication.Add(prescription.Medication);
            }
            //appointment.Prescriptions = this.prescribedMedication;
            appointmentController.LogAppointment(appointment, Diagnoses, note);
        }


        public bool AllergyCheck(int selectedIndex)
        {
            if (registeredPatientController.IsAllergic(this.medications[selectedIndex], registeredPatientController.GetById(patientId)))
            {
                return false;
            }
            return true;
        }
    }
}
