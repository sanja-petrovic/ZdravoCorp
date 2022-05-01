using System;
using System.Collections.Generic;

public class RegisteredPatientController
{
    private RegisteredPatientService patientService;

    public RegisteredPatientService PatientService { get => patientService; set => patientService = value; }
  
    public RegisteredPatientController()
    {
        this.patientService = new RegisteredPatientService();
    }

    public List<RegisteredPatient> GetAll()
    {
        return  patientService.GetAll();
    }

    public RegisteredPatient GetById(String id)
    {
        return patientService.GetById(id);
    }

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> allergies, List<String> diagnosis)
    {
        patientService.CreatePatient(personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, street, stnumber, city, country, bloodType, occupation, emergencyContactName, emergencyContactPhone, allergies, diagnosis);

    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone,List<String> allergies, List<String> diagnosis)
    {
        patientService.UpdatePatient(personalId, name, lastname, phone, password, profilePicture, street, stnumber, city, country, bloodType, occupation, emergencyContactName, emergencyContactPhone, allergies, diagnosis);
    }

    public void DeletePatient(String patientId)
    {
        patientService.DeletePatient(patientId);
    }

}