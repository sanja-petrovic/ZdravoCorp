using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AllRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<TimeOffRequestViewModel> Requests { get; set; }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public TimeOffRequestController Controller { get => controller; set => controller = value; }

        private Doctor doctor;
        private TimeOffRequestController controller;

        public AllRequestsViewModel(Doctor doctor)
        {
            Doctor = doctor;
            Requests = new ObservableCollection<TimeOffRequestViewModel>();
            Controller = new TimeOffRequestController();
            Load();

        }

        public void Load()
        {
            Requests.Clear();
            foreach (TimeOffRequest request in Controller.GetRequestsByDoctor(doctor.PersonalId))
            {
                Requests.Add(new TimeOffRequestViewModel() { Doctor = request.Doctor, Emergency = request.Emergency, Reason = request.Reason, End = request.EndDate, EndString = request.EndDate.ToString("dd.MM.yyyy."), Start = request.StartDate, StartString = request.StartDate.ToString("dd.MM.yyyy."), Status = request.StateToString(request.State) });
            }

            
        }
    }
}
