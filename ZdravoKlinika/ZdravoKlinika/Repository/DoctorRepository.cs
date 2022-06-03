
using System;
using System.Collections.Generic;

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

        public void Add(Doctor doctor)
        {
            DoctorList.Add(doctor);
            DoctorDataHandler.Write(DoctorList);
        }

        public void Delete(Doctor doctor)
        {
            var d = DoctorList.Find(x => x.PersonalId.Equals(doctor.PersonalId));
            DoctorList.Remove(d);
            DoctorDataHandler.Write(DoctorList);
        }

        public void Update(Doctor doctor)
        {
            Delete(doctor);
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

        public void Remove(Doctor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }
    }

}