using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class MedicationService
    {
        private MedicationRepository medicationRepository;


        public MedicationService()
        {
            this.medicationRepository = new MedicationRepository();
        }

        public List<Medication> GetAll()
        {
            return this.medicationRepository.GetAll();
        }

        public Medication GetById(string medId)
        {
            return this.medicationRepository.GetById(medId);
        }
    }
}
