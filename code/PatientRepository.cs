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
            RemoveAllPatient();
            if (value != null)
            {
                foreach (Patient oPatient in value)
                    AddPatient(oPatient);
            }
        }
    }

    public PatientDataHandler PatientsDataHandler { get => patientsDataHandler; set => patientsDataHandler = value; }

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

    public List<Patient> GetAll()
    {
        throw new NotImplementedException();
    }

    public Patient GetById(string id)
    {
        throw new NotImplementedException();
    }

    public void CreatePatient(Patient patients)
    {
        throw new NotImplementedException();
    }

    public void DeletePatient(Patient patients)
    {
        throw new NotImplementedException();
    }

    public void UpdatePatient(Patient patients)
    {
        throw new NotImplementedException();
    }

}