using System;
using System.Collections.Generic;
using ZdravoKlinika.Repository;

public class RegisteredPatientRepository
{
    private RegisteredPatientDataHandler patientsDataHandler;
    private MedicalRecordRepository medicalRecordRepository;
    private List<RegisteredPatient> patients;


    public RegisteredPatientRepository() {
        patientsDataHandler = new RegisteredPatientDataHandler();
        MedicalRecordRepository = new MedicalRecordRepository();
        ReadDataFromFiles();
    }

    private void ReadDataFromFiles()
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
        ReadDataFromFiles();
        foreach (RegisteredPatient pat in Patients)
        {
            UpdateReferences(pat);
        }
        return Patients;
    }

    public RegisteredPatient? GetById(String id)
    {
        ReadDataFromFiles();
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

    public void CreatePatient(RegisteredPatient patient)
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

        MedicalRecordRepository.CreateMedicalRecord(patient.MedicalRecord);
        PatientsDataHandler.Write(Patients);
        return;
    }

    public void DeletePatient(RegisteredPatient patient)
    {
        if (patient == null)
            return;
        if (this.Patients != null)
            if (this.Patients.Contains(patient))
                this.Patients.Remove(patient);

        MedicalRecordRepository.DeleteMedicalRecord(patient.MedicalRecord);
        PatientsDataHandler.Write(Patients);
        return;
    }
    public void DeleteAllPatients()
    {
        if (Patients != null)
            Patients.Clear();
    }

    public void UpdatePatient(RegisteredPatient patient)
    {
        int index = GetPatientIndex(patient);

        if (index != -1)
        {
            Patients[index] = patient;

            MedicalRecordRepository.UpdateMedicalRecord(patient.MedicalRecord);
            PatientsDataHandler.Write(Patients);
        }

        return;
    }
    public int GetPatientIndex(RegisteredPatient patient)
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
    public bool IsAllergic(Medication medication, RegisteredPatient patient)
    {
        List<string> allergies = patient.MedicalRecord.Allergies;
        bool isAlergic = false;
        foreach(string allergy in allergies)
        {
            if(medication.BrandName.Equals(allergy))
            {
                isAlergic = true;
                break;
            } 
            else
            {
                foreach(string allergen in medication.Allergens)
                {
                    if(allergen.Equals(allergy))
                    {
                        isAlergic = true;
                        break;
                    }
                }
                if (isAlergic) break;
            }
        }
        return false;
    }
    public bool IsBanned(RegisteredPatient patient)
    {
        return patient.Ban;
    }
    public void Ban(RegisteredPatient patient)
    {
        patient.Ban = true;
        UpdatePatient(patient);
    }
}