using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Prescription
    {
        private DateTime dateOfCreation;
        Doctor doctor;
        RegisteredPatient registeredPatient;
        Medication medication;
        private int amount;
        private int duration;
        private int frequency;
        private string singleDose;
        private string repeat;
        private string doctorsNote;
        private bool noAlternatives;
        private bool emergency;
        private int id;

        public Prescription()
        {
        }

        public Prescription(Doctor doctor, RegisteredPatient registeredPatient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency, int id)
        {
            this.DateOfCreation = DateTime.Now;
            this.doctor = doctor;
            this.registeredPatient = registeredPatient;
            this.medication = medication;
            this.amount = amount;
            this.duration = duration;
            this.frequency = frequency;
            this.singleDose = singleDose;
            this.repeat = repeat;
            this.doctorsNote = doctorsNote;
            this.noAlternatives = noAlternatives;
            this.emergency = emergency;
            this.Id = id;
        }

        public Medication Medication { get => medication; set => medication = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public string SingleDose { get => singleDose; set => singleDose = value; }
        public string Repeat { get => repeat; set => repeat = value; }
        public string DoctorsNote { get => doctorsNote; set => doctorsNote = value; }
        public bool NoAlternatives { get => noAlternatives; set => noAlternatives = value; }
        public bool Emergency { get => emergency; set => emergency = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public RegisteredPatient RegisteredPatient { get => registeredPatient; set => registeredPatient = value; }
        public int Id { get => id; set => id = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }

        public static Prescription Parse(String id)
        {
            Prescription prescription = new Prescription();
            prescription.Id = Int32.Parse(id);
            return prescription;
        }
    }
}
