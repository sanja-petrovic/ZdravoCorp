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

        public List<Prescription> GetByMedicalRecord(String medicalRecordId)
        {
            return this.prescriptionService.GetByMedicalRecord();
        }
    }
}
