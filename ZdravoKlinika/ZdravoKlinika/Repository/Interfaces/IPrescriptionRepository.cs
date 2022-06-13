using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IPrescriptionRepository : IRepositoryBase<Prescription, int>
    {
        public void Prescribe(Prescription p);
    }
}
