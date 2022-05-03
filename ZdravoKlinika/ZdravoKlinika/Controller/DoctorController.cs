// File:    PatientService.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientService

using System;
using System.Collections.Generic;

public class DoctorController
{
   private DoctorService doctorService = new DoctorService();
   
   public List<Doctor> GetAll()
   {
        return doctorService.GetAll();
   }
   
   public Doctor GetById(String id)
   {
      return (Doctor)doctorService.GetById(id);
   }

    public Doctor GetByEmail(String email)
    {
        return doctorService.GetByEmail(email);
    }
   
   public void CreateDoctor(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String speciality, String education)
   {
       doctorService.CreateDoctor(personalId, name, lastname, dateOfBirth, gender, phone, email, password, profilePicture, speciality, education);
   }
   
   public void UpdateDoctor(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String speciality, String education)
    {
      doctorService.UpdateDoctor(personalId, name, lastname, dateOfBirth, gender,phone, email, password, profilePicture, speciality, education);    
   }
   
   public void DeleteDoctor(String personalId)
   {
        doctorService.DeleteDoctor(personalId); 
   }

}