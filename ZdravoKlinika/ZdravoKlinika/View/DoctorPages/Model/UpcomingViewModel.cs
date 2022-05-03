using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class UpcomingViewModel : ViewModelBase
    {
        private int appointmentId;
        private string title;
        private string type;
        private string room;

        public UpcomingViewModel() { }

        public int AppointmentId { get => appointmentId; set => appointmentId = value; }
        public string Title { get => title; set => title = value; }
        public string Type { get => type; set => type = value; }
        public string Room { get => room; set => room = value; }

        public void init(Appointment appointment)
        {
            this.AppointmentId = appointment.AppointmentId;
            this.Title = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", " + appointment.Doctor.ToString();
            this.type = appointment.getTranslatedType();
            this.room = appointment.Room.Name;
        }
    }
}
