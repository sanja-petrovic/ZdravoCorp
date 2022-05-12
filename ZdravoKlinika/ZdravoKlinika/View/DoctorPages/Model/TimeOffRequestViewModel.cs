using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class TimeOffRequestViewModel : ViewModelBase
    {
        private DateTime start;
        private DateTime end;
        private bool emergency;
        private Doctor doctor;
        private string reason;

        private TimeOffRequestController controller;

        public TimeOffRequestViewModel(Doctor doctor)
        {
            this.Doctor = doctor;
            this.controller = new TimeOffRequestController();
        }

        public DateTime Start { get => start; set => SetProperty(ref start, value); }
        public DateTime End { get => end; set => SetProperty(ref end, value);  }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public string Reason { get => reason; set => SetProperty(ref reason, value); }

        public void CreateRequest()
        {
            this.controller.CreateRequest(Doctor, Start, End, Reason, ZdravoKlinika.Model.RequestState.Pending, Emergency);
        }

    }
}
