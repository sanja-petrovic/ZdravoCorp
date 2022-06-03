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
        public RegisteredPatientRepository RegisteredPatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }
        public GuestPatientRepository GuestPatientRepository { get => guestPatientRepository; set => guestPatientRepository = value; }
        public List<Patient> Patients { get => patients; set => patients = value; }

        public PatientRepository()
        {
            RegisteredPatientRepository = new RegisteredPatientRepository();
            GuestPatientRepository = new GuestPatientRepository();

            LoadAllPatients();
        }

        private void LoadAllPatients()
        {
            List<RegisteredPatient> regPatients = RegisteredPatientRepository.GetAll();
            if (regPatients != null)
            {
                foreach (RegisteredPatient patient in regPatients)
                {
                    this.Add(patient);
                }
            }      
            List<GuestPatient> guestPatients = GuestPatientRepository.GetAll();
            if (guestPatients != null)
            {
                foreach (GuestPatient patient in guestPatients)
                {
                    this.Add(patient);
                }
            }
        }

        public void Add(Patient newPatient)
        {
            if (newPatient == null)
                return;
            if (this.Patients == null)
                this.Patients = new List<Patient>();
            if (!this.Patients.Contains(newPatient))
                this.Patients.Add(newPatient);
        }
        public void Remove(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.Patients != null)
                if (this.Patients.Contains(oldPatient))
                    this.Patients.Remove(oldPatient);
        }
        public void RemoveAll()
        {
            if (Patients != null)
                Patients.Clear();
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

        public void CreateNewGuestPatient(GuestPatient guestPatient)
        {
            Patients.Add(guestPatient);
            GuestPatientRepository.Add(guestPatient);
            return;
        }

        public List<Patient> GetAll()
        {
            return Patients;
        }
    }
}
