using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    internal class Prescription
    {
        Medication medication;
        private int amount;
        private string doctorsNote;
        private int duration;
        private int frequency;
        private string repeat;
        private int singleDose;

        public Medication Medication { get => medication; set => medication = value; }
        public int Amount { get => amount; set => amount = value; }
        public string DoctorsNote { get => doctorsNote; set => doctorsNote = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public string Repeat { get => repeat; set => repeat = value; }
        public int SingleDose { get => singleDose; set => singleDose = value; }

        public Prescription() { }

        public Prescription(Medication medication, int amount, string doctorsNote, int duration, int frequency, string repeat, int singleDose)
        {
            Medication = medication;
            Amount = amount;
            DoctorsNote = doctorsNote;
            Duration = duration;
            Frequency = frequency;
            Repeat = repeat;
            SingleDose = singleDose;
        }
    }
}
