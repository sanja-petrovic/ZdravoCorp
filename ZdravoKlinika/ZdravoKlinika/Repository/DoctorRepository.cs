// File:    PatientRepository.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientRepository

using System;
using System.Collections.Generic;

public class DoctorRepository
{
   private DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
   
   public List<Doctor> GetAll()
   {
        return doctorDataHandler.Read();
   }
   
   public Doctor GetById(String id)
   {
        List<Doctor> doctors = new List<Doctor>();
        doctors = doctorDataHandler.Read();
        foreach (Doctor doctor in doctors)
        {
            if(doctor.PersonalId == id)
            {
                return doctor;
            }
        }
        return null;
   }
   
   public void CreateDoctor(Doctor doctor)
   {

        List<Doctor> doctors = doctorDataHandler.Read(); 
        if(doctors == null)
        {
            doctors = new List<Doctor>();
        }
        doctors.Add(doctor);
        doctorDataHandler.Write(doctors);
    }
   
   public void DeleteDoctor(Doctor doctor)
   {
        List<Doctor> doctors = doctorDataHandler.Read();
        if (doctors == null)
        {
            throw new Exception();
        }
        var d = doctors.Find(x => x.PersonalId.Equals(doctor.PersonalId));
        doctors.Remove(d);
        doctorDataHandler.Write(doctors);
    }
   
   public void UpdateDoctor(Doctor doctor)
   {
      DeleteDoctor(doctor);
      CreateDoctor(doctor);
   }

}