using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IRegisteredPatientRepository : IRepositoryBase<RegisteredPatient, String>
    {
        public void RecordUpdated(RegisteredPatient p);
        public bool IsBanned(RegisteredPatient patient);
        public void Ban(RegisteredPatient patient);
    }
}
