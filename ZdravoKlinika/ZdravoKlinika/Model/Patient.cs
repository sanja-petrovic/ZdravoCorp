using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Patient
    {
        private PatientType patientType;
        private RegisteredPatient registeredPatient;
        private GuestPatient guestPatient;

        public PatientType PatientType { get => patientType; set => patientType = value; }
        public RegisteredPatient RegisteredPatient { get => registeredPatient; set => registeredPatient = value; }
        public GuestPatient GuestPatient { get => guestPatient; set => guestPatient = value; }
        
    }
}
