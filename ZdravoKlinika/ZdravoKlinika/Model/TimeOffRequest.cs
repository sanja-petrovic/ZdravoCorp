using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public enum RequestState
    {
        Pending,
        Approved,
        Denied
    }

    public class TimeOffRequest
    {
        private int id;
        private Doctor doctor;
        private DateTime dateOfCreation;
        private DateTime startDate;
        private DateTime endDate;
        private string reason;
        private RequestState state;
        private bool emergency;

        public TimeOffRequest(int id, Doctor doctor, DateTime dateOfCreation, DateTime startDate, DateTime endDate, string reason, RequestState state, bool emergency)
        {
            this.id = id;
            this.doctor = doctor;
            this.dateOfCreation = dateOfCreation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.reason = reason;
            this.state = state;
            this.emergency = emergency;
        }


        public int Id { get => id; set => id = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Reason { get => reason; set => reason = value; }
        public RequestState State { get => state; set => state = value; }
        public bool Emergency { get => emergency; set => emergency = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
    }
}
