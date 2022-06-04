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
    private bool ban = false;

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
    public bool Ban { get => ban; set => ban = value; }

    public String GetPatientFullName()
    {
        return this.Name + " " + this.Lastname;
    }

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

    public String GetName() 
    {
        return Name;
    }
    public String GetLastname()
    {
        return Lastname;
    }

    public string BloodTypeToString()
    {
        string bloodType = "";
        switch(this.bloodType)
        {
            case BloodType.APositive:
                bloodType = "A+";
                break;
            case BloodType.ANegative:
                bloodType = "A-";
                break;
            case BloodType.BPositive:
                bloodType = "B+";
                break;
            case BloodType.BNegative:
                bloodType = "B-";
                break;
            case BloodType.ABPositive:
                bloodType = "AB+";
                break;
            case BloodType.ABNegative:
                bloodType = "AB-";
                break;
            case BloodType.OPositive:
                bloodType = "O+";
                break;
            case BloodType.ONegative:
                bloodType = "O-";
                break;
            default:
                break;

        }

        return bloodType;
    }
    
    public string ToString()
    {
        return this.GetPatientFullName() + ", " + this.GetPatientId();
    }
}