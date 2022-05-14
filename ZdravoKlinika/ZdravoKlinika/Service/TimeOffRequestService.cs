using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class TimeOffRequestService
    {
        private TimeOffRequestRepository repository;

        public TimeOffRequestService()
        {
            this.repository = new TimeOffRequestRepository();
        }

        public List<TimeOffRequest> GetAll()
        {
            return this.repository.GetAll();
        }

        public TimeOffRequest GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public void CreateRequest(TimeOffRequest request)
        {
            int newId = this.repository.GetAll().Count > 0 ? this.GetAll().Last().Id + 1 : 1;
            request.Id = newId;
            if(IsRequestAcceptable(request))
            {
                this.repository.CreateRequest(request);
            }
        }

        public bool IsRequestAcceptable(TimeOffRequest request)
        {
            AppointmentService appointmentService = new AppointmentService();
            bool result = true;
            if ((request.StartDate - request.DateOfCreation).Days < 2)
            {
                result = false;

            } else if (IsAnotherSpecialistOff(request) && request.Emergency == false) {
                result = false;
            } else if (appointmentService.DoctorHasAppointmentsInDateRange(request.Doctor, new Util.DateBlock(request.StartDate, request.EndDate)))
            {
                result = false;
            }

            return result;
        }

        public bool IsAnotherSpecialistOff(TimeOffRequest request)
        {
            bool result = false;
            foreach(TimeOffRequest r in this.GetAll())
            {
                if(r.EndDate.CompareTo(DateTime.Today) < 0 || request.State == RequestState.Denied)
                {
                    continue;
                } else if(r.Doctor.Specialty == request.Doctor.Specialty)
                {

                    if(r.StartDate <= request.EndDate && request.StartDate <= r.EndDate)
                    {
                        result = true;
                    }
                    
                }
            }

            return result;
        }

        public List<TimeOffRequest> GetRequestsByDoctor(Doctor doctor)
        {
            return this.repository.GetRequestsByDoctor(doctor);
        }

        public List<TimeOffRequest> GetDoctorsRequestsByStatus(Doctor doctor, RequestState state)
        {
            return this.repository.GetDoctorsRequestsByStatus(doctor, state);
        }
    }
}
