using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service
{
    public class MedApprovalRequestService
    {
        private MedApprovalRequestRepository repository;

        public MedApprovalRequestService()
        {
            this.repository = new MedApprovalRequestRepository();
        }

        public List<MedApprovalRequest> GetAll()
        {
            return this.repository.GetAll();
        }

        public MedApprovalRequest GetById(int id)
        {
            return repository.GetById(id);
        }

        public MedApprovalRequest GetByMedication(Medication medication)
        {
            return this.repository.GetByMedication(medication);
        }

        public List<MedApprovalRequest> GetByReviewer(Doctor doctor)
        {
            return this.repository.GetByReviewer(doctor);
        }

        public List<MedApprovalRequest> GetPendingRequests()
        {
            return this.repository.GetPendingRequests();
        }

        public List<MedApprovalRequest> GetPendingRequestsByReviewer(Doctor doctor)
        {
            return this.repository.GetPendingRequestsByReviewer(doctor);
        }

        public void CreateRequest(MedApprovalRequest request)
        {
            int newId = this.repository.GetAll().Count > 0 ? this.repository.GetAll().Last().Id + 1 : 1;
            request.Id = newId;
            request.Pending = true;

            this.repository.CreateRequest(request);
        }

        public void DenyRequest(MedApprovalRequest request)
        {
            this.repository.UpdateRequest(request);
        }

        public void ApproveRequest(MedApprovalRequest request)
        {
            request.Pending = false;
            request.Medication.Validated = true;
            this.repository.UpdateRequest(request);
        }
    }
}
