using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IMedApprovalRequestRepository : IRepositoryBase<Model.MedApprovalRequest, int>
    {
        public Model.MedApprovalRequest GetByMedication(Medication medication);
        public List<Model.MedApprovalRequest> GetByReviewer(Model.Doctor doctor);
        public List<Model.MedApprovalRequest> GetPendingRequests();

        public List<Model.MedApprovalRequest> GetDeniedRequests();
        public List<Model.MedApprovalRequest> GetPendingRequestsByReviewer(Model.Doctor doctor);

    }
}
