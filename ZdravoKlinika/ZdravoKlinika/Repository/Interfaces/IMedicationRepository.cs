using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IMedicationRepository : IRepositoryBase<Medication, string>
    {
        public Medication GetByCodeAndName(string medicationCode, string brandName);
        public List<Medication> GetByApprovedValue(bool approved);
        public List<Medication> GetAlternatives(Medication medication);
    }
}
