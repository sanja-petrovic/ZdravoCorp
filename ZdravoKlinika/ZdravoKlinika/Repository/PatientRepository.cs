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
        private RegisteredPatientRepository registeredPatientRepository;
        private GuestPatientRepository guestPatientRepository;

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
            RegisteredPatientRepository = new RegisteredPatientRepository();
            GuestPatientRepository = new GuestPatientRepository();

            List<RegisteredPatient> rpats = RegisteredPatientRepository.GetAll();
            foreach (RegisteredPatient pat in rpats) 
            {
                this.AddPatient(pat);
            }
            List<GuestPatient> gpats = GuestPatientRepository.GetAll();
            if (gpats != null)
            {
                foreach (GuestPatient pat in gpats)
                {
                    this.AddPatient(pat);
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
                if(patient.IsPatientById(id))
                    return patient;
            }
            return null;
        }

        public void CreatePatient(String id, PatientType type)
        {
            if (type == PatientType.Registered)
            {
                Patient pat = RegisteredPatientRepository.GetById(id);
                AddPatient(pat);
            }
            else if (type == PatientType.Guest)
            {
                Patient pat = GuestPatientRepository.GetById(id);
                AddPatient(pat);
            }
        }

        public List<Patient> GetAll()
        {
            return patients;
        }
    }
}
