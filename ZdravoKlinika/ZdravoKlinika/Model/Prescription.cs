﻿using System;
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
        private int id;

        public Prescription(Doctor doctor, RegisteredPatient registeredPatient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, int id)

        public Prescription()
        {
        }

        public Prescription() { }

        public Medication Medication { get => medication; set => medication = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public string SingleDose { get => singleDose; set => singleDose = value; }
        public string Repeat { get => repeat; set => repeat = value; }
        public string DoctorsNote { get => doctorsNote; set => doctorsNote = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public RegisteredPatient RegisteredPatient { get => registeredPatient; set => registeredPatient = value; }
        public int Id { get => id; set => id = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }

        public static Prescription Parse(string id)
        {
            Prescription prescription = new Prescription();
            int intId;
            if(int.TryParse(id, out intId))
            {
                prescription.Id = intId;
            }
            return prescription;
        }

    }
}
