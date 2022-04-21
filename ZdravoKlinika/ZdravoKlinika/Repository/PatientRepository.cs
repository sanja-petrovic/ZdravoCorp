using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class PatientRepository
    {
        private List<Patient> patients;
        private PatientDataHandler patientDataHandler;
        private RegisteredPatientRepository registeredPatientRepository;
        private GuestPatientRepository guestPatientRepository;

        public PatientDataHandler PatientDataHandler { get => patientDataHandler; set => patientDataHandler = value; }
        public List<Patient> Patients
        {
            get
            {
                if (patients == null)
                    patients = new List<Patient>();
                return patients;
            }
            set
            {
                RemoveAllPatient();
                if (value != null)
                {
                    foreach (Patient oPatient in value)
                        AddPatient(oPatient);
                }
            }
        }

        public RegisteredPatientRepository RegisteredPatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }
        public GuestPatientRepository GuestPatientRepository { get => guestPatientRepository; set => guestPatientRepository = value; }

        public PatientRepository()
        {
            PatientDataHandler = new PatientDataHandler();
            RegisteredPatientRepository = new RegisteredPatientRepository();
            GuestPatientRepository = new GuestPatientRepository();
            updateReferences();
        }

        private void updateReferences()
        {
            foreach (Patient pat in patients)
            {
                if (pat.PatientType == PatientType.Guest)
                {
                    pat.GuestPatient = GuestPatientRepository.GetById(pat.GuestPatient.PersonalId);
                }
                else
                {
                    pat.RegisteredPatient = RegisteredPatientRepository.GetById(pat.RegisteredPatient.PersonalId);
                }
            }
        }

        public void AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return;
            if (this.patients == null)
                this.patients = new List<Patient>();
            if (!this.patients.Contains(newPatient))
                this.patients.Add(newPatient);
        }
        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.patients != null)
                if (this.patients.Contains(oldPatient))
                    this.patients.Remove(oldPatient);
        }
        public void RemoveAllPatient()
        {
            if (patients != null)
                patients.Clear();
        }
        public Patient GetById(String id)
        {
            foreach (Patient patient in Patients)
            {
                if (patient.PatientType == PatientType.Registered)
                {
                    if (patient.RegisteredPatient.PersonalId.Equals(id))
                        return patient;
                }
                else 
                {
                    if (patient.GuestPatient.PersonalId.Equals(id))
                        return patient;
                }
            }
            return null;
        }

        public void CreatePatient(String id)
        {
            Patient pat = new Patient();
            pat.GuestPatient = GuestPatientRepository.GetById(id);
            pat.RegisteredPatient = RegisteredPatientRepository.GetById(id);
            if (pat.RegisteredPatient == null)
            { 
                pat.PatientType = PatientType.Guest;
            }
            else if (GuestPatientRepository == null) 
            {
                pat.PatientType = PatientType.Registered;
            }
        }
    }
}
