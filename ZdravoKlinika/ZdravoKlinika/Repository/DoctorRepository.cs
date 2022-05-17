
using System;
using System.Collections.Generic;

public class DoctorRepository
{
    private DoctorDataHandler doctorDataHandler;
    private List<Doctor> doctorList;

    public List<Doctor> DoctorList { get => doctorList; set => doctorList = value; }
    public DoctorDataHandler DoctorDataHandler { get => doctorDataHandler; set => doctorDataHandler = value; }

    public DoctorRepository()
    { 
        DoctorDataHandler = new DoctorDataHandler();
        DoctorList = DoctorDataHandler.Read();
        if (DoctorList == null) DoctorList = new List<Doctor>();
    }

   
   public List<Doctor> GetAll()
   {
        return DoctorList;
   }
   
   public Doctor GetById(String id)
   {

        foreach (Doctor doctor in DoctorList)
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
        foreach (Doctor doctor in DoctorList)
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
        DoctorList.Add(doctor);
        DoctorDataHandler.Write(DoctorList);
    }
   
   public void DeleteDoctor(Doctor doctor)
   {
        var d = DoctorList.Find(x => x.PersonalId.Equals(doctor.PersonalId));
        DoctorList.Remove(d);
        DoctorDataHandler.Write(DoctorList);
    }

    public void UpdateDoctor(Doctor doctor)
    {
        DeleteDoctor(doctor);
        CreateDoctor(doctor);
    }


    public List<Doctor> GetBySpecialty(string specialty)
    {
        List<Doctor> doctors = new List<Doctor>();

        foreach (Doctor doctor in this.doctorList)
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

        foreach(Doctor doctor in this.DoctorList)
        {
            if(!specialties.Contains(doctor.Specialty))
            {
                specialties.Add(doctor.Specialty);
            }
        }

        return specialties;
    }

}