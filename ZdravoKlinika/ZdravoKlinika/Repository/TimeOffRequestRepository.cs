using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class TimeOffRequestRepository
    {
        private TimeOffRequestDataHandler dataHandler;
        private List<TimeOffRequest> requests;

        public TimeOffRequestRepository()
        {
            this.dataHandler = new TimeOffRequestDataHandler();
            this.requests = this.dataHandler.Read();
        }


        public List<TimeOffRequest> GetAllSorted()
        {
            return this.GetAll().OrderBy(request => request.StartDate).ToList();
        }

        public List<TimeOffRequest> GetAll()
        {
            foreach(TimeOffRequest request in this.requests)
            {
                UpdateReferences(request);
            }
            
            return this.requests;
        }

        public TimeOffRequest GetById(int id)
        {
            TimeOffRequest request = null;
            foreach(TimeOffRequest r in this.requests)
            {
                if(r.Id == id)
                {
                    request = r;
                    UpdateReferences(request);
                }
            }

            return request;
        }

        public void CreateRequest(TimeOffRequest request)
        {
            this.requests.Add(request);
            this.dataHandler.Write(this.requests);
        }

        public void UpdateReferences(TimeOffRequest request)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            request.Doctor = doctorRepository.GetById(request.Doctor.PersonalId);
        }

        public List<TimeOffRequest> GetRequestsByDoctor(Doctor doctor)
        {
            List<TimeOffRequest> requests = new List<TimeOffRequest>();

            foreach (TimeOffRequest request in this.dataHandler.Read().OrderBy(request => request.StartDate))
            {
                if(request.Doctor.PersonalId.Equals(doctor.PersonalId) && request.EndDate.CompareTo(DateTime.Today) >= 0)
                {
                    UpdateReferences(request);
                    requests.Add(request);
                } 
            }

            return requests;
        }

        public List<TimeOffRequest> GetDoctorsRequestsByStatus(Doctor doctor, RequestState status)
        {
            List<TimeOffRequest> requests = new List<TimeOffRequest>();

            foreach (TimeOffRequest request in this.GetRequestsByDoctor(doctor))
            {
                if(request.State == status)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public void EditRequest(TimeOffRequest requestInDatabase)
        {
            int index = -1;
            foreach (TimeOffRequest request in requests)
            {
                if (request.Id == requestInDatabase.Id)
                {
                    index = requests.IndexOf(request);
                }
            }
            if (index == -1)
            {
                throw new Exception("Request does not exist");
            }
            requests[index] = requestInDatabase;
            dataHandler.Write(requests);
        }
    }
}
