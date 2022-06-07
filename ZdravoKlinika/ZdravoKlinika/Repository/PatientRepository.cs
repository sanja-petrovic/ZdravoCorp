using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class PatientRepository : Interfaces.IPatientRepository
    {
        private List<IPatient> patients;
        private RegisteredPatientRepository registeredPatientRepository;
        private GuestPatientRepository guestPatientRepository;
        public RegisteredPatientRepository RegisteredPatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }
        public GuestPatientRepository GuestPatientRepository { get => guestPatientRepository; set => guestPatientRepository = value; }
        public List<IPatient> Patients { get => patients; set => patients = value; }

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

        public void Add(IPatient newPatient)
        {
            if (newPatient == null)
                return;
            if (this.Patients == null)
                this.Patients = new List<IPatient>();
            if (!this.Patients.Contains(newPatient))
                this.Patients.Add(newPatient);
        }
        public void Remove(IPatient oldPatient)
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
        public IPatient GetById(String id)
        {
            foreach (IPatient patient in Patients)
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

        public List<IPatient> GetAll()
        {
            return Patients;
        }

        public void Update(IPatient item)
        {
            int index = GetIndex(item.GetPatientId());
            if (index != -1)
            {
                patients[index] = item;
            }
        }

        private int GetIndex(String id)
        {
            int indexToRemove = -1;
            foreach (IPatient patient in patients)
            {
                if (patient.GetPatientId().Equals(id))
                {
                    indexToRemove = patients.IndexOf(patient);
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("Patient does not exist");
            }
            return indexToRemove;
        }
    }
}
