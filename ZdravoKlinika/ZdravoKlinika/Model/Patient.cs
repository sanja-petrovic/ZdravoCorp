using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public interface Patient
    {
        public PatientType GetPatientType() 
        {
            return PatientType.Null;
        }

        public bool IsPatientById(String id) 
        {
            return false;
        }

        public String GetPatientId()
        {
            return null;
        }

        public String GetName()
        {
            return null;
        }
        public String GetLastname()
        {
            return null;
        }

        public static Patient Parse(String data)
        {
            String[] splitData = data.Split(',');
            if (splitData[1].Equals(PatientType.Registered.ToString()))
            {
                return RegisteredPatient.Parse(splitData[0]);
            }
            else if (splitData[1].Equals(PatientType.Guest.ToString()))
            { 
                return GuestPatient.Parse(splitData[0]);
            }
            return null;
        }

    }
}
