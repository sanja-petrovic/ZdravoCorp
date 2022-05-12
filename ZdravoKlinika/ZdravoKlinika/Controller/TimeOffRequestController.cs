using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;

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

        public TimeOffRequest GetById(int id)
        {
            return service.GetById(id);
        }
        /*        private int id;
        private Doctor doctor;
        private DateTime dateOfCreation;
        private DateTime startDate;
        private DateTime endDate;
        private string reason;
        private RequestState state;
        private bool emergency;*/
        public void CreateRequest(Doctor doctor, DateTime start, DateTime end, String reason, RequestState state, bool emergency)
        {
            TimeOffRequest request = new TimeOffRequest(-1, doctor, DateTime.Today, start, end, reason, state, emergency);

            this.service.CreateRequest(request);
        }
    }
}
