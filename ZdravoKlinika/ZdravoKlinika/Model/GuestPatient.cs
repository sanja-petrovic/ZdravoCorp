using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class GuestPatient
    {
        private UserType userType;
        private String personalId;
        private String name;
        private String lastname;

        public UserType UserType { get => userType; set => userType = value; }
        public string PersonalId { get => personalId; set => personalId = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }

        public static GuestPatient Parse(String id)
        {
            GuestPatient patient = new GuestPatient();
            patient.PersonalId = id;
            return patient;
        }
    }
}
