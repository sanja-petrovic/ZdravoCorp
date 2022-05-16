using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class MedApprovalRequestController
    {
        private MedApprovalRequestService service;

        public MedApprovalRequestController()
        {
            this.service = new MedApprovalRequestService();
        }

        public List<MedApprovalRequest> GetAll()
        {
            return this.service.GetAll();
        }

        public MedApprovalRequest GetById(int id)
        {
            return service.GetById(id);
        }

        public MedApprovalRequest GetByMedication(String medicationId)
        {
            MedicationService medicationService = new MedicationService();
            return this.service.GetByMedication(medicationService.GetById(medicationId));
        }

        public List<MedApprovalRequest> GetByReviewer(String doctorId)
        {
            DoctorService doctorService = new DoctorService();
            return this.service.GetByReviewer(doctorService.GetById(doctorId));
        }

        public List<MedApprovalRequest> GetPendingRequests()
        {
            return this.service.GetPendingRequests();
        }

        public List<MedApprovalRequest> GetDeniedRequests()
        {
            return this.service.GetDeniedRequests();
        }

        public List<MedApprovalRequest> GetPendingRequestsByReviewer(String doctorId)
        {
            DoctorService doctorService = new DoctorService();
            return this.service.GetPendingRequestsByReviewer(doctorService.GetById(doctorId));
        }

        public void CreateRequest(String doctorId, String medicationId)
        {
            DoctorService doctorService = new DoctorService();
            MedicationService medicationService = new MedicationService();
            Doctor doctor = doctorService.GetById(doctorId);
            Medication medication = medicationService.GetById(medicationId);
            this.service.CreateRequest(new MedApprovalRequest { Reviewer = doctor, Medication = medication});
        }

        public void DenyRequest(int requestId, string comment)
        {
            MedApprovalRequest request = this.service.GetById(requestId);
            request.Comment = comment;
            //request.Reviewer = null;
            this.service.DenyRequest(request);
        }

        public void ApproveRequest(int requestId)
        {
            MedApprovalRequest request = this.service.GetById(requestId);
            this.service.ApproveRequest(request);
        }
    }
}
