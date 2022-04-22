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
            return PatientType.none;
        }

        public bool IsPatientById(String id) 
        {
            return false;
        }

    }
}
