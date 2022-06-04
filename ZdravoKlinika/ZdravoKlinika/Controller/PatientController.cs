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

        public IPatient GetById(String id)
        {
            return PatientService.GetById(id);
        }

        public void CreateNewGuestPatient(String id, String name, String lastname)
        {
            GuestPatient guestPatient = new GuestPatient();
            guestPatient.Name = name;
            guestPatient.Lastname = lastname;
            guestPatient.PersonalId = id;
            PatientService.CreateNewGuestPatient(guestPatient);
            return;
        }
    }
}
