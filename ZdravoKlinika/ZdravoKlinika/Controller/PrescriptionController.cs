using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class PrescriptionController
    {
        private PrescriptionService prescriptionService;

        public PrescriptionController()
        {
            this.prescriptionService = new PrescriptionService();
        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionService.GetAll();
        }

        public List<Prescription> GetByPatient(string patientId)
        {
            RegisteredPatientController registeredPatientController = new RegisteredPatientController();

            return this.prescriptionService.GetByPatient(registeredPatientController.GetById(patientId));
        }

        public Prescription GetById(int id)
        {
            return this.prescriptionService.GetById(id);
        }


        public void Prescribe(Prescription prescription)
        {
            prescription.DateOfCreation = DateTime.Now;
            this.prescriptionService.Prescribe(prescription);
        }

    }
}
