using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class PatientMedicationNotification : Notification
    {
        private Prescription prescription;

        internal Prescription Prescription { get => prescription; set => prescription = value; }
    }
}
