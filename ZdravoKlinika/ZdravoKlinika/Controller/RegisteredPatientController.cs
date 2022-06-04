using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

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

    public RegisteredPatient? GetById(String id)
    {
        return patientService.GetById(id);
    }

    public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, List<String> allergies, List<String> diagnosis)
    {
        MedicalRecord record = new MedicalRecord(personalId, diagnosis, allergies);
        Address address = new Address(street, stnumber, city, country);
        RegisteredPatient patient = new RegisteredPatient(personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, address, bloodType, occupation, emergencyContactName, emergencyContactPhone, record);

        patientService.CreatePatient(patient);
    }

    public void UpdatePatient(String personalId, String name, String lastname, String phone, String password, String profilePicture, String street, String stnumber, String city, String country, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone,List<String> allergies, List<String> diagnosis)
    {
        RegisteredPatient pat = new RegisteredPatient();
        pat.PersonalId = personalId;
        pat.Name = name;
        pat.Lastname = lastname;
        pat.Phone = phone;
        pat.Password = password;
        pat.ProfilePicture = profilePicture;
        pat.Occupation = occupation;
        pat.BloodType = bloodType;

        pat.EmergencyContactName = emergencyContactName;
        pat.EmergencyContactPhone = emergencyContactPhone;

        MedicalRecord record = new MedicalRecord(personalId, diagnosis, allergies);
        Address address = new Address(street, stnumber, city, country);

        pat.MedicalRecord = record;
        pat.Address = address;

        patientService.UpdatePatient(pat);
    }

    public void DeletePatient(String patientId)
    {
        patientService.DeletePatient(patientId);
    }

    public bool IsAllergic(String medicationId, String patientId)
    {
        RegisteredPatientService registeredPatientService = new RegisteredPatientService();
        MedicationService medicationService = new MedicationService();
        return this.patientService.IsAllergic(medicationService.GetById(medicationId), registeredPatientService.GetById(patientId));
    }
    public bool IsBanned(RegisteredPatient patient)
    {
        return this.patientService.IsBanned(patient);
    }

    public bool IsBanned(String patientId)
    {
        return this.patientService.IsBanned(GetById(patientId));
    }

}