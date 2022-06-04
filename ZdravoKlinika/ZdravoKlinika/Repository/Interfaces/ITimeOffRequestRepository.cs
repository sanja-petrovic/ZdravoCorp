using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface ITimeOffRequestRepository : IRepositoryBase<TimeOffRequest, int>
    {
        public List<TimeOffRequest> GetRequestsByDoctor(Doctor doctor);
        public List<TimeOffRequest> GetDoctorsRequestsByStatus(Doctor doctor, RequestState status);

    }
}
