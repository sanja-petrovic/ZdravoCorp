// File:    PatientService.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientService

using System;
using System.Collections.Generic;

public class DoctorService
{
    private DoctorRepository doctorRepository = new DoctorRepository();

    public List<Doctor> GetAll()
    {
        return doctorRepository.GetAll();
    }

    public Doctor GetById(String id)
    {
        return (Doctor)doctorRepository.GetById(id);
    }

    public Doctor GetByEmail(String email)
    {
        return doctorRepository.GetByEmail(email);
    }
    public void CreateDoctor(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String speciality, String education)
    {
        Doctor doctor = new Doctor();
        doctor.PersonalId = personalId;
        doctor.Name = name;
        doctor.Lastname = lastname;
        doctor.DateOfBirth = dateOfBirth;
        doctor.ProfilePicture = profilePicture;
        doctor.Email = email;
        doctor.Password = password;
        doctor.Gender = gender;
        doctor.Phone = phone;
        doctor.Specialty = speciality;
        doctor.EducationLevel = education;

        doctorRepository.Add(doctor);

    }

    public void UpdateDoctor(String personalId, String name, String lastname, DateTime dateOfBirth, Gender gender, String phone, String email, String password, String profilePicture, String speciality, String education)
    {
        Doctor doctor = new Doctor();
        doctor.PersonalId = personalId;
        doctor.Name = name;
        doctor.Lastname = lastname;
        doctor.ProfilePicture = profilePicture;
        doctor.Email = email;
        doctor.Password = password;
        doctor.Phone = phone;
        doctor.Specialty = speciality;
        doctor.EducationLevel = education;
        doctor.Gender = gender;
        doctor.DateOfBirth = dateOfBirth;

        doctorRepository.Update(doctor);

    }

    public void DeleteDoctor(String personalId)
    {
        doctorRepository.Delete(GetById(personalId));
    }

    public List<Doctor> GetBySpecialty(string specialty)
    {
        return this.doctorRepository.GetBySpecialty(specialty);
    }

    public List<String> GetAllSpecialties()
    {
        return this.doctorRepository.GetAllSpecialties();
    }
}