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

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, Address address,String parentName, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> alergies, List<String> diagnosis)
    {
        Console.WriteLine("cont");
        patientService.CreatePatient(personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, address, parentName, bloodType, occupation, emergencyContactName, emergencyContactPhone, alergies, diagnosis);

    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String email, String password, String profilePicture, String parentName, String occupation, String emergencyContactName, String emergencyContactPhone)
    {
        patientService.UpdatePatient(personalId, name, lastname, phone, email, password, profilePicture, parentName, occupation, emergencyContactName, emergencyContactPhone);
    }

    public void DeletePatient(String patientId)
    {
        patientService.DeletePatient(patientId);
    }

}