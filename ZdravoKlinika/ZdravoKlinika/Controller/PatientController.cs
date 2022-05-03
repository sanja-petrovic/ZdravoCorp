using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class PatientController
    {
        private PatientService patientService;

        public PatientService PatientService { get => patientService; set => patientService = value; }

        public PatientController()
        {
            patientService = new PatientService();
        }

        public Patient GetById(String id)
        {
            return PatientService.GetById(id);
        }

        public void CreateNewGuestPatient(String id, String name, String lastname)
        {
            PatientService.CreateNewGuestPatient(id, name, lastname);
            return;
        }
    }
}
