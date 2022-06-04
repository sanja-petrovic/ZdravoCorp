using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.Controller
{
    public class TimeOffRequestController
    {
        private TimeOffRequestService service;

        public TimeOffRequestController()
        {
            this.service = new TimeOffRequestService();
        }

        public List<TimeOffRequest> GetAll()
        {
            return this.service.GetAll();
        }

        public List<TimeOffRequest> GetAllUnprocessed()
        {
            return this.service.GetAllUnprocessed();
        }

        public TimeOffRequest GetById(int id)
        {
            return service.GetById(id);
        }

        public List<TimeOffRequest> GetAllSorted()
        {
            return this.service.GetAllSorted();
        }

        public void CreateRequest(Doctor doctor, DateTime start, DateTime end, String reason, RequestState state, bool emergency)
        {
            TimeOffRequest request = new TimeOffRequest(-1, doctor, DateTime.Today, start, end, reason, state, emergency);

            this.service.CreateRequest(request);
        }

        public List<TimeOffRequest> GetRequestsByDoctor(String doctorId)
        {
            DoctorService doctorService = new DoctorService();
            Doctor doctor = doctorService.GetById(doctorId);
            return this.service.GetRequestsByDoctor(doctor);
        }

        public List<TimeOffRequest> GetDoctorsRequestsByStatus(String doctorId, RequestState status)
        {
            DoctorService doctorService = new DoctorService();
            Doctor doctor = doctorService.GetById(doctorId);
            return this.service.GetDoctorsRequestsByStatus(doctor, status);
        }

        public bool IsAnotherSpecialistOff(DateBlock period, String specialty)
        {
            return this.service.IsAnotherSpecialistOff(period, specialty);
        }

        public bool HasAlreadyMadeRequest(DateBlock period, String doctorId)
        {
            return this.service.HasAlreadyMadeRequest(period, doctorId);
        }

        public bool HasScheduledAppointments(String doctorId, DateBlock period)
        {
            AppointmentService appointmentService = new AppointmentService();
            return appointmentService.HasScheduledAppointments(doctorId, period);
        }

        public void ProcessRequest(int requestId, String comment, RequestState state)
        {
            TimeOffRequest req = new TimeOffRequest();
            req.Comment = comment;
            req.Id = requestId;
            req.State = state;
            service.ProcessRequest(req);
            return;
        }

    }
}
