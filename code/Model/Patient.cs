using System;
using System.Collections.Generic;

public class Patient : RegisteredUser
{
    private String parentName;
    private BloodType bloodType;
    private String occupation;
    private String emergencyContactName;
    private String emergencyContactPhone;
    private MedicalRecord medicalRecord;

    public string ParentName { get => parentName; set => parentName = value; }
    public BloodType BloodType { get => bloodType; set => bloodType = value; }
    public string Occupation { get => occupation; set => occupation = value; }
    public string EmergencyContactName { get => emergencyContactName; set => emergencyContactName = value; }
    public string EmergencyContactPhone { get => emergencyContactPhone; set => emergencyContactPhone = value; }
    public MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }
}