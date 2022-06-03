
using System;
using System.Collections.Generic;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

public class RegisteredPatientService
{
    private RegisteredPatientRepository registeredPatientRepository;

    public RegisteredPatientRepository PatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }


    public RegisteredPatientService() { 
        // init
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

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> allergies, List<String> diagnosis)
    {
        Console.WriteLine("service");
        MedicalRecord record = new MedicalRecord(personalId, diagnosis, allergies);
        Address address = new Address(street,stnumber,city,country);
        RegisteredPatient patient = new RegisteredPatient( personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, address, bloodType, occupation, emergencyContactName, emergencyContactPhone, record);
        
        registeredPatientRepository.Add(patient);
    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> allergies, List<String> diagnosis)
    {
        RegisteredPatient pat = this.GetById(personalId);
        pat.Name = name;
        pat.Lastname = lastname;
        pat.Phone = phone;
        pat.Password = password;
        pat.ProfilePicture = profilePicture;
        pat.Occupation = occupation;
        pat.Address.Street = street;
        pat.Address.Number = stnumber;
        pat.Address.City = city;
        pat.Address.Country = country;
        pat.BloodType = bloodType;

        pat.EmergencyContactName = emergencyContactName;
        pat.EmergencyContactPhone = emergencyContactPhone;

        pat.MedicalRecord.Allergies = allergies;
        pat.MedicalRecord.Diagnoses = diagnosis;

        registeredPatientRepository.Update(pat);
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
}