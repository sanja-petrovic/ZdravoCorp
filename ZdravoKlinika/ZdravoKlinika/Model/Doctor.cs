using System.Collections.Generic;
using System;
namespace ZdravoKlinika.Model
{
    public class Doctor : ZdravoKlinika.Model.Employee
    {
        private String specialty;
        //private String education;

        public Doctor()
        { }

    public string Specialty { get => specialty; set => specialty = value; }
    public string NameAndLast { get => Name + " " + Lastname + " " + PersonalId; }

    public bool IsSpecialist()
    {
        return !Specialty.Equals("Opšta praksa");
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
