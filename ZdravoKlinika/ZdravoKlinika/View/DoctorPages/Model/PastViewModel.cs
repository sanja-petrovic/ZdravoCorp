using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PastViewModel : ViewModelBase
    {
        private int appointmentId;
        private string title;
        private string diagnosis;
        private string precription;
        private string opinion;
        private Doctor doctor;

        public PastViewModel()
        {

        }

        public void init(Appointment appointment)
        {
            this.appointmentId = appointment.AppointmentId;
            this.Doctor = appointment.Doctor;
            this.title = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", " + appointment.Doctor.ToString();
            this.diagnosis = appointment.Diagnoses;
            foreach(Prescription p in appointment.Prescriptions)
            {
                this.precription += p.ToString() ;
                if(appointment.Prescriptions.Last().Id != p.Id)
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
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
    }
}
