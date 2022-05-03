using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class MedicationController
    {

        private MedicationService medicationService;


        public MedicationController()
        {
            this.medicationService = new MedicationService();
        }

        public List<Medication> GetAll()
        {
            return this.medicationService.GetAll();
        }

        public Medication GetById(string medId)
        {
            return this.medicationService.GetById(medId);
        }


    }
}
