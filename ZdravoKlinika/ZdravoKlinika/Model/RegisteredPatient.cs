using System;
using System.Text.Json.Serialization;
using ZdravoKlinika.Model;

public class RegisteredPatient : RegisteredUser, Patient
{
    private PatientType patientType;
    private BloodType bloodType;
    private String occupation;
    private String emergencyContactName;
    private String emergencyContactPhone;
    private MedicalRecord medicalRecord;

    public RegisteredPatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, Address address, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, MedicalRecord medicalRecord)
    {
        this.UserType = UserType.Patient;
        this.PersonalId = personalId;
        this.Name = name;
        this.Lastname = lastname;
        this.DateOfBirth = dateOfBirth;
        this.Gender = gender;
        this.Phone = phone;
        this.Email = email;
        this.Password = password;
        this.ProfilePicture = profilePicture;
        this.Address = address;
        this.bloodType = bloodType;
        this.occupation = occupation;
        this.emergencyContactName = emergencyContactName;
        this.emergencyContactPhone = emergencyContactPhone;
        this.medicalRecord = medicalRecord;
    }

    public RegisteredPatient() { }
    public static RegisteredPatient Parse(String id)
    {
        RegisteredPatient patient = new RegisteredPatient();
        patient.PersonalId = id;
        return patient;
    }

    public BloodType BloodType { get => bloodType; set => bloodType = value; }
    public string Occupation { get => occupation; set => occupation = value; }
    public string EmergencyContactName { get => emergencyContactName; set => emergencyContactName = value; }
    public string EmergencyContactPhone { get => emergencyContactPhone; set => emergencyContactPhone = value; }
    public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }
    public PatientType PatientType { get => patientType; set => patientType = value; }

    public PatientType GetPatientType() 
    {
        return PatientType;
    }

    public bool IsPatientById(String id) 
    {
        return PersonalId.Equals(id);
    }

    public String GetPatientId()
    {
        return PersonalId;
    }
}