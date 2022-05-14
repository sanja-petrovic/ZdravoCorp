﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class MedApprovalRequestRepository
    {

        private MedApprovalRequestDataHandler dataHandler;
        private List<MedApprovalRequest> requests;
        private DoctorRepository doctorRepository;
        private MedicationRepository medicationRepository;

        public MedApprovalRequestRepository()
        {
            this.dataHandler = new MedApprovalRequestDataHandler();
            this.doctorRepository = new DoctorRepository();
            this.medicationRepository = new MedicationRepository();
            ReadDataFromFile();
        }

        private void ReadDataFromFile()
        {
            this.requests = this.dataHandler.Read();
            if (this.requests == null)
            {
                this.requests = new List<MedApprovalRequest>();

            }
        }

        private void UpdateReferences(MedApprovalRequest request)
        {
            if (request.Reviewer != null)
            {
                request.Reviewer = this.doctorRepository.GetById(request.Reviewer.PersonalId);
            }
            if (request.Medication != null)
            {
                request.Medication = this.medicationRepository.GetById(request.Medication.MedicationId);
            }
        }

        public List<MedApprovalRequest> GetAll()
        {
            foreach(MedApprovalRequest request in this.requests)
            {
                UpdateReferences(request);
            }

            return this.requests;
        }

        public MedApprovalRequest GetById(int id)
        {
            MedApprovalRequest medApprovalRequest = null;
            foreach(MedApprovalRequest request in this.requests)
            {
                if(request.Id == id)
                {
                    UpdateReferences(request);
                    medApprovalRequest = request;
                }
            }

            return medApprovalRequest;
        }

        public MedApprovalRequest GetByMedication(Medication medication)
        {
            MedApprovalRequest r = null;
            foreach(MedApprovalRequest request in this.requests)
            {
                if(request.Medication.MedicationId.Equals(medication.MedicationId))
                {
                    r = request;
                }
            }

            return r;
        }

        public List<MedApprovalRequest> GetByReviewer(Doctor doctor)
        {
            List<MedApprovalRequest> requests = new List<MedApprovalRequest>();
            foreach (MedApprovalRequest request in this.requests)
            {
                if (request.Reviewer.PersonalId.Equals(doctor.PersonalId))
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public List<MedApprovalRequest> GetPendingRequests()
        {
            List<MedApprovalRequest> requests = new List<MedApprovalRequest>();
            foreach (MedApprovalRequest request in this.requests)
            {
                if (request.Pending)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public List<MedApprovalRequest> GetPendingRequestsByReviewer(Doctor doctor)
        {
            List<MedApprovalRequest> requests = new List<MedApprovalRequest>();
            foreach (MedApprovalRequest request in this.requests)
            {
                if (request.Reviewer.PersonalId.Equals(doctor.PersonalId) && request.Pending)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public void CreateRequest(MedApprovalRequest request)
        {
            this.requests.Add(request);
            this.dataHandler.Write(requests);
        }

        public void UpdateRequest(MedApprovalRequest request)
        {
            int index = this.GetIndex(request);
            this.requests[index] = request;

            this.dataHandler.Write(this.requests);
        }

        public int GetIndex(MedApprovalRequest request)
        {
            int index = -1;
            for (int i = 0; i < this.requests.Count; i++)
            {
                if (this.requests[i].Id == request.Id)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

    }

}
