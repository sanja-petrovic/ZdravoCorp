using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class PrescriptionViewModel : ViewModelBase
    {
        private string patientName;
        private string patientId;

        private string doctorName;
        private string doctorSpecialty;

        private string medication;
        private string amount;
        private bool noAlt;
        private bool emergency;
        private string duration;
        private string frequency;
        private string singleDose;
        private string repeat;
        private string doctorsNote;

        private DoctorController doctorController;
        private RegisteredPatientController registeredPatientController;
        private MedicationController medicationController;
        private PrescriptionController prescriptionController;
        private List<Medication> medications;
        private List<String> medicationsDisplay;
        private Doctor doctor;
        private RegisteredPatient patient;

        public PrescriptionViewModel()
        {
            this.medicationController = new MedicationController();
            this.doctorController = new DoctorController();
            this.registeredPatientController = new RegisteredPatientController();
            this.prescriptionController = new PrescriptionController();
   
            Medications = medicationController.GetAll();
            this.MedicationsDisplay = new List<String>();
            foreach(Medication m in this.medications)
            {
                MedicationsDisplay.Add(m.BrandName + " " + m.Dosage + ", " + m.Form);
            }
        }

        public void init(string doctorId, string patientId)
        {
            this.patientId = patientId;
            Patient = this.registeredPatientController.GetById(patientId);
            Doctor = this.doctorController.GetById(doctorId);

            this.patientName = Patient.GetPatientFullName();
            this.doctorName = "Dr " + doctor.Name + " " + doctor.Lastname;
            this.doctorSpecialty = doctor.Specialty;

            this.amount = "Količina";
            this.singleDose = "Jedna doza";
            this.duration = "Trajanje (dani)";
            this.frequency = "Učestalost";
        }

        public void save(int selectedIndex)
        {
            /*this.prescriptionController.Prescribe(
                Doctor, Patient, this.medications[selectedIndex], this.amount, this.duration, this.frequency, this.singleDose, this.repeat, this.DoctorsNote, false, false

                );*/
        }

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value);  }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public string Medication { get => medication; set => SetProperty(ref medication, value); }
        public string Amount { get => amount; set => SetProperty(ref amount, value); }
        public bool NoAlt { get => noAlt; set => SetProperty(ref noAlt, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public string Duration { get => duration; set => SetProperty(ref duration, value); }
        public string Frequency { get => frequency; set => SetProperty(ref frequency, value); }
        public string SingleDose { get => singleDose; set => SetProperty(ref singleDose, value); }
        public string Repeat { get => repeat; set => SetProperty(ref repeat, value); }
        public string DoctorsNote { get => doctorsNote; set => SetProperty(ref doctorsNote, value); }
        public List<Medication> Medications { get => medications; set => medications = value; }
        public List<string> MedicationsDisplay { get => medicationsDisplay; set => medicationsDisplay = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public RegisteredPatient Patient { get => patient; set => patient = value; }
    }
}
