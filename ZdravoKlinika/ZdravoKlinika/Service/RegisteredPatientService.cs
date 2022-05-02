
using System;
using System.Collections.Generic;

public class RegisteredPatientService
{
    private RegisteredPatientRepository patientRepository;

    public RegisteredPatientRepository PatientRepository { get => patientRepository; set => patientRepository = value; }


    public RegisteredPatientService() { 
        // init
        this.patientRepository = new RegisteredPatientRepository();
    }

    public List<RegisteredPatient> GetAll()
    {
        return patientRepository.GetAll();
    }

    public RegisteredPatient GetById(String id)
    {
        return patientRepository.GetById(id);
    }

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, Address address, String parentName, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> alergies, List<String> diagnosis)
    {
        Console.WriteLine("service");
        MedicalRecord record = new MedicalRecord(personalId, alergies, diagnosis);
        RegisteredPatient patient = new RegisteredPatient( personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, address, bloodType, occupation, emergencyContactName, emergencyContactPhone, record);
        
        patientRepository.CreatePatient(patient);
        //TODO medicalRecord repo write it too!
    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String email, String password, String profilePicture, String parentName, String occupation, String emergencyContactName, String emergencyContactPhone)
    {
        RegisteredPatient pat = this.GetById(personalId);
        pat.Name = name;
        pat.Lastname = lastname;
        pat.Phone = phone;
        pat.Email = email;
        pat.Password = password;
        pat.ProfilePicture = profilePicture;
        pat.Occupation = occupation;
        pat.EmergencyContactName = emergencyContactName;
        pat.EmergencyContactPhone = emergencyContactPhone;

        patientRepository.UpdatePatient(pat);
    }

    public void DeletePatient(String patientId)
    {
        patientRepository.DeletePatient(this.GetById(patientId));
        return;
    }

    public bool IsAllergic(Medication medication, RegisteredPatient patient)
    {
        return this.patientRepository.IsAllergic(medication, patient);
    }

}