// File:    PatientRepository.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientRepository

using System;
using System.Collections.Generic;

public class DoctorRepository
{
    private DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
    private List<Doctor> doctors;

    public List<Doctor> Doctors { get => doctors; set => doctors = value; }

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
            if (doctor.PersonalId == id)
            {
                return doctor;
            }
        }
        return null;
    }

    public Doctor GetByEmail(String email)
    {
        List<Doctor> doctors = new List<Doctor>();
        doctors = doctorDataHandler.Read();
        foreach (Doctor doctor in doctors)
        {
            if (doctor.Email == email)
            {
                return doctor;
            }
        }
        return null;
    }

    public void CreateDoctor(Doctor doctor)
    {

        List<Doctor> doctors = doctorDataHandler.Read();
        if (doctors == null)
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


    public List<Doctor> GetBySpecialty(string specialty)
    {
        List<Doctor> doctors = new List<Doctor>();

        foreach (Doctor doctor in this.doctors)
        {
            if (doctor.Specialty.Equals(specialty))
            {
                doctors.Add(doctor);
            }
        }
        return doctors;
    }

    public List<String> GetAllSpecialties()
    {
        List<String> specialties = new List<String>();

        foreach(Doctor doctor in this.doctors)
        {
            if(!specialties.Contains(doctor.Specialty))
            {
                specialties.Add(doctor.Specialty);
            }
        }

        return specialties;
    }

}