using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PastViewModel : ViewModelBase
    {
        private int appointmentId;
        private string title;
        private string diagnosis;
        private string precription;
        private string opinion;

        public PastViewModel()
        {

        }

        public void init(Appointment appointment)
        {
            this.appointmentId = appointment.AppointmentId;
            this.title = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", " + appointment.Doctor.ToString();
            this.diagnosis = appointment.Diagnoses;
            foreach(string medication in appointment.Prescriptions)
            {
                this.precription += medication;
                if(appointment.Prescriptions.Last() != medication)
                {
                    this.precription += ", ";
                }
            }
            this.opinion = appointment.DoctorsNotes;
        }

        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string Precription { get => precription; set => SetProperty(ref precription, value); }
        public string Opinion { get => opinion; set => SetProperty(ref opinion, value); }
    }
}
