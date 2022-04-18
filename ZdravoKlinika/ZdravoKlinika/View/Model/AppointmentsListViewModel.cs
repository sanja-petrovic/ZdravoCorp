using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Model
{
    internal class AppointmentsListViewModel : ViewModelBase
    {
        private DateTime dateTime;
        ObservableCollection<Appointment> appointments;
        AppointmentController appointmentController;
        private List<String> times;

        public AppointmentsListViewModel()
        {
            this.Appointments = new ObservableCollection<Appointment>();
            this.appointmentController = new AppointmentController(); 
            this.Times = new List<String>();
        }

        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                SetProperty(ref dateTime, value);
                List<Appointment> appointments2 = this.appointmentController.GetAppointmentsByDoctorDate("456", this.dateTime);
                this.Appointments = new ObservableCollection<Appointment>(appointments2);
                List<String> newTimes = new List<String>();
                foreach(Appointment appointment in appointments2)
                {
                    newTimes.Add(appointment.DateAndTime.ToShortTimeString());
                }

                SetProperty(ref times, newTimes);
            }
        }

        public ObservableCollection<Appointment> Appointments { get => appointments; set => SetProperty(ref appointments, value); }
        public List<string> Times { get => times; set => SetProperty(ref times, value); }
    }
}
