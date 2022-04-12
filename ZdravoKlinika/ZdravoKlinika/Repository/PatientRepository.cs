using System;
using System.Collections.Generic;

public class PatientRepository
{
    private PatientDataHandler patientsDataHandler;
    private List<Patient> patients;
    public List<Patient> Patient
    {
        get
        {
            if (patients == null)
                patients = new List<Patient>();
            return patients;
        }
        set
        {
            DeleteAllPatients();
            if (value != null)
            {
                foreach (Patient oPatient in value)
                    CreatePatient(oPatient);
            }
        }
    }
    public PatientRepository() {
        // init
        patientsDataHandler = new PatientDataHandler();
        this.patients = patientsDataHandler.Read();
    }

    public PatientDataHandler PatientsDataHandler { get => patientsDataHandler; set => patientsDataHandler = value; }

    public List<Patient> GetAll()
    {
        return patients;
    }

    public Patient? GetById(String id)
    {
        foreach (Patient patient in this.patients) 
        {
            if (patient.PersonalId.Equals(id)) 
            {
                return patient;
            }
        }
        return null;
    }

    public void CreatePatient(Patient patient)
    {
        if (patient == null)
            return;
        if (this.patients == null)
            this.patients = new List<Patient>();

        this.patients.Add(patient);
        PatientsDataHandler.Write(patients);
        return;
    }

    public void DeletePatient(Patient patient)
    {
        if (patient == null)
            return;
        if (this.patients != null)
            if (this.patients.Contains(patient))
                this.patients.Remove(patient);

        PatientsDataHandler.Write(patients);
        return;
    }
    public void DeleteAllPatients()
    {
        if (patients != null)
            patients.Clear();
    }

    public void UpdatePatient(Patient patient)
    {
        int index = -1;
        foreach (Patient patientObject in this.patients)
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
        PatientsDataHandler.Write(patients);

        return;
    }

}