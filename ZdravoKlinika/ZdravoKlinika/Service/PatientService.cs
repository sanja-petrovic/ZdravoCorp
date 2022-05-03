using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class PatientService
    {
        private PatientRepository patientRepository;

        public PatientRepository PatientRepository { get => patientRepository; set => patientRepository = value; }

        public PatientService()
        {
            patientRepository = new PatientRepository();
        }

        public Patient GetById(String id)
        {
            return PatientRepository.GetById(id);
        }

        public void CreateNewGuestPatient(String id, String name, String lastname)
        {
            PatientRepository.CreateNewGuestPatient(id, name, lastname);
            return;
        }

    }
}
