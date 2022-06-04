using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IEquipmentRepository : IRepositoryBase<Equipment, String>
    {
        public List<Equipment> GetByExpendability(bool expendable);
    }
}
