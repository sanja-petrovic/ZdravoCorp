using System.Collections.Generic;
using System;
namespace ZdravoKlinika.Model
{
    public class Doctor : ZdravoKlinika.Model.Employee
    {
        private String specialty;
        //private String education;

        public Doctor()
        {

        }

        public string Specialty { get => specialty; set => specialty = value; }
        public string NameAndLast { get => Name + " " + Lastname + " " + PersonalId; }
        //public string Education { get => education; set => education = value; }

        public bool IsSpecialist()
        {
            throw new NotImplementedException();
        }

        public static Doctor Parse(string id)
        {
            Doctor doc = new Doctor();
            doc.PersonalId = id;
            return doc;
        }

        public override string ToString()
        {
            return "Dr " + this.Name + " " + this.Lastname;
        }
    }
}
