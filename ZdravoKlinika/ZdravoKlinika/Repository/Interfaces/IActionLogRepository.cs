using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IActionLogRepository : IRepositoryBase<Model.ActionLog, String>
    {
        public List<Model.ActionLog> GetByUserId(String id);

    }
}
