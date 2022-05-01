using System;
using System.Collections.Generic;
using ZdravoKlinika.Repository;

public class RegisteredPatientRepository
{
    private RegisteredPatientDataHandler patientsDataHandler;
    private MedicalRecordRepository medicalRecordRepository;
    private List<RegisteredPatient> patients;
    public List<RegisteredPatient> Patient
    {
        get
        {
            if (patients == null)
                patients = new List<RegisteredPatient>();
            return patients;
        }
        set
        {
            DeleteAllPatients();
            if (value != null)
            {
                foreach (RegisteredPatient oPatient in value)
                    CreatePatient(oPatient);
            }
        }
    }
    public RegisteredPatientRepository() {
        // init
        patientsDataHandler = new RegisteredPatientDataHandler();
        MedicalRecordRepository = new MedicalRecordRepository();
        this.patients = patientsDataHandler.Read();
        updateReferences();
    }

    private void updateReferences()
    {
       
        foreach (RegisteredPatient pat in patients)
        {
            pat.MedicalRecord = MedicalRecordRepository.GetById(pat.MedicalRecord.MedicalRecordId); 
        }
    }


    public RegisteredPatientDataHandler PatientsDataHandler { get => patientsDataHandler; set => patientsDataHandler = value; }
    public MedicalRecordRepository MedicalRecordRepository { get => medicalRecordRepository; set => medicalRecordRepository = value; }

    public List<RegisteredPatient> GetAll()
    {
        updateReferences();
        return patients;
    }

    public RegisteredPatient? GetById(String id)
    {
        updateReferences();
        foreach (RegisteredPatient patient in this.patients) 
        {
            if (patient.PersonalId.Equals(id)) 
            {
                return patient;
            }
        }
        return null;
    }

    public void CreatePatient(RegisteredPatient patient)
    {
        if (patient == null)
            return;
        if (this.patients == null)
            this.patients = new List<RegisteredPatient>();

        foreach (RegisteredPatient pat in patients)
        {
            if (pat.PersonalId == patient.PersonalId)
            {
                return;
            }
        }

        this.patients.Add(patient);

        MedicalRecordRepository.CreateMedicalRecord(patient.MedicalRecord);
        PatientsDataHandler.Write(patients);
        return;
    }

    public void DeletePatient(RegisteredPatient patient)
    {
        if (patient == null)
            return;
        if (this.patients != null)
            if (this.patients.Contains(patient))
                this.patients.Remove(patient);

        MedicalRecordRepository.DeleteMedicalRecord(patient.MedicalRecord);
        PatientsDataHandler.Write(patients);
        return;
    }
    public void DeleteAllPatients()
    {
        if (patients != null)
            patients.Clear();
    }

    public void UpdatePatient(RegisteredPatient patient)
    {
        int index = -1;
        foreach (RegisteredPatient patientObject in this.patients)
        {
            if (patientObject.PersonalId.Equals(patient.PersonalId))
            {
                index = patients.IndexOf(patientObject);
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Error");
            return;
        }

        patients[index] = patient;

        MedicalRecordRepository.UpdateMedicalRecord(patient.MedicalRecord);
        PatientsDataHandler.Write(patients);
        
        return;
    }

}