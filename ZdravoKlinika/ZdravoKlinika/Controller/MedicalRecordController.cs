using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
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
    }
}
