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
        Patient patient;
        Medication medication;
        private int amount;
        private int duration;
        private int frequency;
        private string singleDose;
        private string repeat;
        private string doctorsNote;
        private int id;

        public Prescription() { }

        public Prescription(Doctor doctor, Patient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, int id)
        {
            this.doctor = doctor;
            this.Patient = patient;
            this.medication = medication;
            this.amount = amount;
            this.duration = duration;
            this.frequency = frequency;
            this.singleDose = singleDose;
            this.repeat = repeat;
            this.doctorsNote = doctorsNote;
            this.id = id;
        }

        public Medication Medication { get => medication; set => medication = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public string SingleDose { get => singleDose; set => singleDose = value; }
        public string Repeat { get => repeat; set => repeat = value; }
        public string DoctorsNote { get => doctorsNote; set => doctorsNote = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public int Id { get => id; set => id = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }
        public Patient Patient { get => patient; set => patient = value; }

        public static Prescription Parse(int id) {
            Prescription prescription = new Prescription();
            prescription.Id = id;
            return prescription; 
        }

        public override string ToString()
        {
            return this.medication.ToString();
        }
    }
}
