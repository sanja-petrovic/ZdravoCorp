// File:    Patient.cs
// Author:  sanya
// Created: Tuesday, 5 April 2022 3:38:20 PM
// Purpose: Definition of Class Patient

using System;

public class Patient : RegisteredUser
{
   private String parentName;
   private BloodType bloodType;
   private String occupation;
   private String emergencyContactName;
   private String emergencyContactPhone;
   
   private MedicalRecord medicalRecord;

}