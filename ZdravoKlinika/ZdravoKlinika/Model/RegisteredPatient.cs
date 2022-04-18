using System;
using System.Text.Json.Serialization;

public class RegisteredPatient : RegisteredUser
{
    private String parentName;
    private BloodType bloodType;
    private String occupation;
    private String emergencyContactName;
    private String emergencyContactPhone;
    private MedicalRecord medicalRecord;

    public RegisteredPatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, Address address, String parentName, BloodType bloodType, String occupation, String emergencyContactName, String emergencyContactPhone, MedicalRecord medicalRecord)
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
        this.parentName = parentName;
        this.bloodType = bloodType;
        this.occupation = occupation;
        this.emergencyContactName = emergencyContactName;
        this.emergencyContactPhone = emergencyContactPhone;
        this.medicalRecord = medicalRecord;
    }

    public RegisteredPatient() { }

    public string ParentName { get => parentName; set => parentName = value; }
    public BloodType BloodType { get => bloodType; set => bloodType = value; }
    public string Occupation { get => occupation; set => occupation = value; }
    public string EmergencyContactName { get => emergencyContactName; set => emergencyContactName = value; }
    public string EmergencyContactPhone { get => emergencyContactPhone; set => emergencyContactPhone = value; }
    public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }
}