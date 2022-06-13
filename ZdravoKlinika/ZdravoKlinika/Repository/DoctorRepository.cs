
using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class DoctorRepository : Interfaces.IDoctorRepository
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
            Doctor retVal = null;
            foreach (Doctor doctor in DoctorList)
            {
                if (doctor.PersonalId == id)
                {
                    retVal = doctor;
                    break;
                }
            }
            return retVal;
        }

        public Doctor GetByEmail(String email)
        {
            Doctor retVal = null;
            foreach (Doctor doctor in DoctorList)
            {
                if (doctor.Email == email)
                {
                    retVal = doctor;
                    break;
                }
            }
            return retVal;
        }

        public void Add(Doctor doctor)
        {
            DoctorList.Add(doctor);
            DoctorDataHandler.Write(DoctorList);
        }

        public void Remove(Doctor doctor)
        {
            var d = DoctorList.Find(x => x.PersonalId.Equals(doctor.PersonalId));
            DoctorList.Remove(d);
            DoctorDataHandler.Write(DoctorList);
        }

        public void Update(Doctor doctor)
        {
            Remove(doctor);
            Add(doctor);
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

            foreach (Doctor doctor in this.DoctorList)
            {
                if (!specialties.Contains(doctor.Specialty))
                {
                    specialties.Add(doctor.Specialty);
                }
            }

            return specialties;
        }


        public void RemoveAll()
        {
            this.doctorList = new List<Doctor>();
            this.doctorDataHandler.Write(this.doctorList);
        }
    }

}