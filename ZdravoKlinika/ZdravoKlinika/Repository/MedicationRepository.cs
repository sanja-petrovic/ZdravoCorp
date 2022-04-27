using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;

namespace ZdravoKlinika.Repository
{
    internal class MedicationRepository
    {

        private MedicationDataHandler medicationDataHandler;
        private List<Medication> medications;


        public MedicationRepository()
        {
            medicationDataHandler = new MedicationDataHandler();
            this.medications = medicationDataHandler.Read();
        }

        public List<Medication> GetAll()
        {
            return medicationDataHandler.Read();
        }

        public Medication GetById(String id)
        {
            foreach(Medication medication in medications)
            {
                if(medication.MedicationId == id)
                {
                    return medication;
                }
            }

            return null;
        }
    }
}
