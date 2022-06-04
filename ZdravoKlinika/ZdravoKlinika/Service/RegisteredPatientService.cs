
using System;
using System.Collections.Generic;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

public class RegisteredPatientService
{
    private RegisteredPatientRepository registeredPatientRepository;

    public RegisteredPatientRepository PatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }


    public RegisteredPatientService() 
    { 
        this.registeredPatientRepository = new RegisteredPatientRepository();
    }

    public List<RegisteredPatient> GetAll()
    {
        return registeredPatientRepository.GetAll();
    }

    public RegisteredPatient? GetById(String id)
    {
        return registeredPatientRepository.GetById(id);
    }

    public void CreatePatient(RegisteredPatient patient)
    {
        if (IsPersonalIdFree(patient.PersonalId))
            registeredPatientRepository.Add(patient);
        else throw new Exception("ID in use");
    }

    public void UpdatePatient(RegisteredPatient pat)
    {
        RegisteredPatient? patientInDatabase = this.GetById(pat.PersonalId);
        if (patientInDatabase == null)
            throw new Exception("Patient does not exist");

        patientInDatabase.Name = pat.Name;
        patientInDatabase.Lastname = pat.Lastname;
        patientInDatabase.Phone = pat.Phone;
        patientInDatabase.Password = pat.Password;
        patientInDatabase.ProfilePicture = pat.ProfilePicture;
        patientInDatabase.Occupation = pat.Occupation;
        patientInDatabase.Address.Street = pat.Address.Street;
        patientInDatabase.Address.Number = pat.Address.Number;
        patientInDatabase.Address.City = pat.Address.City;
        patientInDatabase.Address.Country = pat.Address.Country;
        patientInDatabase.BloodType = pat.BloodType;

        patientInDatabase.EmergencyContactName = pat.EmergencyContactName;
        patientInDatabase.EmergencyContactPhone = pat.EmergencyContactPhone;

        patientInDatabase.MedicalRecord.Allergies = pat.MedicalRecord.Allergies;
        patientInDatabase.MedicalRecord.Diagnoses = pat.MedicalRecord.Diagnoses;
        registeredPatientRepository.Update(patientInDatabase);
    }

    public void DeletePatient(String patientId)
    {
        registeredPatientRepository.Remove(this.GetById(patientId));
        return;
    }


    public bool IsAllergic(Medication medication, RegisteredPatient patient)
    {
        List<string> allergies = patient.MedicalRecord.Allergies;
        bool isAllergic = false;
        foreach (string allergy in allergies)
        {
            if (medication.BrandName.Equals(allergy))
            {
                isAllergic = true;
                break;
            }
            else
            {
                foreach (string allergen in medication.Allergens)
                {
                    if (allergen.Equals(allergy))
                    {
                        isAllergic = true;
                        break;
                    }
                }
                if (isAllergic) break;
            }
        }
        return isAllergic;
    }
    public bool IsBanned(RegisteredPatient patient)
    {
        return this.registeredPatientRepository.IsBanned(patient);
    }
    private bool IsPersonalIdFree(String personalId)
    {
        bool isIdFree = true;
        RegisteredPatient? pat = this.GetById(personalId);
        if (pat != null)
            isIdFree = false;
        return isIdFree;
    }
}