using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class MedicalRecordController
    {
        private MedicalRecordService medicalRecordService;

        public MedicalRecordController()
        {
            medicalRecordService = new MedicalRecordService();
        }

        public MedicalRecord GetById(String id)
        {
            return this.medicalRecordService.GetById(id);
        }

        public List<string> GetDiagnosesAndAllergies(String id)
        {
            return this.medicalRecordService.GetDiagnosesAndAllergies(this.medicalRecordService.GetById(id));
        }

        public void AddDiagnosis(String diagnosis, String medicalRecordId)
        {
            this.medicalRecordService.AddDiagnosis(diagnosis, this.medicalRecordService.GetById(medicalRecordId));
        }
    }
}
