using System;
using System.Collections.Generic;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Repository 
{
    public class RegisteredPatientRepository : Interfaces.IRegisteredPatientRepository
    {
        private RegisteredPatientDataHandler patientsDataHandler;
        private MedicalRecordRepository medicalRecordRepository;
        private List<RegisteredPatient> patients;


        public RegisteredPatientRepository()
        {
            patientsDataHandler = new RegisteredPatientDataHandler();
            MedicalRecordRepository = new MedicalRecordRepository();
            ReadDataFromFile();
        }

        private void ReadDataFromFile()
        {
            Patients = patientsDataHandler.Read();
            if (Patients == null) Patients = new List<RegisteredPatient>();
        }

        private void UpdateReferences(RegisteredPatient pat)
        {
            pat.MedicalRecord = MedicalRecordRepository.GetById(pat.MedicalRecord.MedicalRecordId);
        }

        public void RecordUpdated(RegisteredPatient p)
        {
            foreach (RegisteredPatient patient in this.Patients)
            {
                if (patient.PersonalId.Equals(p.PersonalId))
                {
                    patient.MedicalRecord = MedicalRecordRepository.GetById(patient.MedicalRecord.MedicalRecordId);
                }
            }

        }

        public RegisteredPatientDataHandler PatientsDataHandler { get => patientsDataHandler; set => patientsDataHandler = value; }
        public MedicalRecordRepository MedicalRecordRepository { get => medicalRecordRepository; set => medicalRecordRepository = value; }
        public List<RegisteredPatient> Patients { get => patients; set => patients = value; }

        public List<RegisteredPatient> GetAll()
        {
            ReadDataFromFile();
            foreach (RegisteredPatient pat in Patients)
            {
                UpdateReferences(pat);
            }
            return Patients;
        }

        public RegisteredPatient? GetById(String id)
        {
            ReadDataFromFile();
            RegisteredPatient registeredPatientToReturn = null;
            foreach (RegisteredPatient patient in this.Patients)
            {
                if (patient.PersonalId.Equals(id))
                {
                    UpdateReferences(patient);
                    registeredPatientToReturn = patient;
                    break;
                }
            }
            return registeredPatientToReturn;
        }

        public void Add(RegisteredPatient patient)
        {
            if (patient == null)
                return;
            if (this.Patients == null)
                this.Patients = new List<RegisteredPatient>();

            foreach (RegisteredPatient pat in Patients)
            {
                if (pat.PersonalId == patient.PersonalId)
                {
                    return;
                }
            }

            this.Patients.Add(patient);

            MedicalRecordRepository.Add(patient.MedicalRecord);
            PatientsDataHandler.Write(Patients);
            return;
        }

        public void Remove(RegisteredPatient patient)
        {
            if (patient == null)
                return;
            if (this.Patients != null)
                if (this.Patients.Contains(patient))
                    this.Patients.Remove(patient);

            MedicalRecordRepository.Remove(patient.MedicalRecord);
            PatientsDataHandler.Write(Patients);
            return;
        }
        public void RemoveAll()
        {
            if (Patients != null)
                Patients.Clear();
        }

        public void Update(RegisteredPatient patient)
        {
            int index = GetIndex(patient);

            if (index != -1)
            {
                Patients[index] = patient;

                MedicalRecordRepository.Update(patient.MedicalRecord);
                PatientsDataHandler.Write(Patients);
            }

            return;
        }
        private int GetIndex(RegisteredPatient patient)
        {
            int index = -1;
            foreach (RegisteredPatient patientObject in this.Patients)
            {
                if (patientObject.PersonalId.Equals(patient.PersonalId))
                {
                    index = Patients.IndexOf(patientObject);
                }
            }
            return index;
        }
        public bool IsBanned(RegisteredPatient patient)
        {
            return patient.Ban;
        }
    }
}
