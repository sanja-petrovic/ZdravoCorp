using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class AppointmentLoggingViewModel : ViewModelBase
    {
        private AppointmentController appointmentController;
        private Appointment appointment;
        private string patientName;
        private string patientId;
        private string doctorName;
        private string doctorSpecialty;
        private string dateTime;
        private string room;

        private string diagnoses;
        private string doctorsNote;

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public string DateTime { get => dateTime; set => SetProperty(ref dateTime, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string DoctorsNote { get => doctorsNote; set => SetProperty(ref doctorsNote, value); }

        public AppointmentLoggingViewModel()
        {
            
        }

        public void load()
        {

        }
        
        public void save()
        {
            
        }
    }
}
