using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class GuestPatient : Patient
    {
        private PatientType patientType;
        private UserType userType;
        private String personalId;
        private String name;
        private String lastname;

        public UserType UserType { get => userType; set => userType = value; }
        public string PersonalId { get => personalId; set => personalId = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public PatientType PatientType { get => patientType; set => patientType = value; }

        public static GuestPatient Parse(String id)
        {
            GuestPatient patient = new GuestPatient();
            patient.UserType = UserType.Patient;
            patient.patientType = PatientType.Guest;
            patient.PersonalId = id;
            return patient;
        }

        public PatientType GetPatientType()
        {
            return PatientType;
        }
        public bool IsPatientById(String id)
        {
            return PersonalId.Equals(id);
        }
    }
}
