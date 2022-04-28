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


        public Prescription GetById(int id)
        {
            return this.prescriptionService.GetById(id);
        }

        public void Prescribe(Doctor doctor, RegisteredPatient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency)
        {
            
            this.prescriptionService.Prescribe(doctor, patient, medication, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency);

        }
    }
}
