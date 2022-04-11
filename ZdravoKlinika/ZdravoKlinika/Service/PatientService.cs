
using System;
using System.Collections.Generic;

public class PatientService
{
    private PatientRepository patientRepository;

    public PatientRepository PatientRepository { get => patientRepository; set => patientRepository = value; }

    public List<Patient> GetAll()
    {
        return patientRepository.GetAll();
    }

    public Patient GetById(String id)
    {
        return patientRepository.GetById(id);
    }

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String parentName, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> alergies, List<String> diagnosis)
    {
        MedicalRecord record = new MedicalRecord(personalId, alergies, diagnosis);
        Patient patient = new Patient( personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, parentName, bloodType, occupation, emergencyContactName, emergencyContactPhone, record);
        
        patientRepository.CreatePatient(patient);
        //TODO medicalRecord repo write it too!
    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String email, String password, String profilePicture, String parentName, String occupation, String emergencyContactName, String emergencyContactPhone)
    {
        Patient pat = this.GetById(personalId);
        pat.Name = name;
        pat.Lastname = lastname;
        pat.Phone = phone;
        pat.Email = email;
        pat.Password = password;
        pat.ProfilePicture = profilePicture;
        pat.ParentName = parentName;
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

}