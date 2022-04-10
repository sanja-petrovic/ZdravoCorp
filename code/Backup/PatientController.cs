// File:    PatientController.cs
// Author:  asd
// Created: Sunday, 10 April 2022 12:55:40 AM
// Purpose: Definition of Class PatientController

using System;

public class PatientController
{
   private PatientService patientService;
   
   public List<Patient> GetAll()
   {
      throw new NotImplementedException();
   }
   
   public Patient GetById(string id)
   {
      throw new NotImplementedException();
   }
   
   public void CreatePatient(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String parentName, BloodType bloodType, String occupation, String emergencyContanctName, String emergencyContactPhone, List<String> alergies, List<String> diagnosis)
   {
      throw new NotImplementedException();
   }
   
   public void UpdatePatient(String personalId, String name, String lastname, String phone, String email, String password, String profilePicture, String parentName, String occupation, String emergencyContanctName, String emergencyContactPhone)
   {
      throw new NotImplementedException();
   }
   
   public void DeletePatient(String patientId)
   {
      throw new NotImplementedException();
   }

}