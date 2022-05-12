using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AnamnesisTab : ViewModelBase, ITabViewModel
    {
        private string header;
        private int appointmentId;

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        

        private AppointmentController appointmentController;
        private Appointment appointment;
        private string patientName;
        private string patientId;
        private string doctorName;
        private string doctorSpecialty;
        private string dateTime;
        private string room;

        private string diagnoses;
        private string note;

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public string DoctorSpecialty { get => doctorSpecialty; set => SetProperty(ref doctorSpecialty, value); }
        public string DateTime { get => dateTime; set => SetProperty(ref dateTime, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string Note { get => note; set => SetProperty(ref note, value); }
        public AppointmentController AppointmentController { get => appointmentController; set => SetProperty(ref appointmentController, value); }
        public Appointment Appointment { get => appointment; set => SetProperty(ref appointment, value); }

        public AnamnesisTab()
        {
            this.AppointmentController = new AppointmentController();
        }

        public void Load()
        {
            this.appointment = appointmentController.GetAppointmentById(appointmentId);
            this.patientName = appointment.Patient.GetPatientFullName();
            this.patientId = appointment.Patient.GetPatientId();
            this.doctorName = "Dr " + appointment.Doctor.Name + " " + appointment.Doctor.Lastname;
            this.doctorSpecialty = appointment.Doctor.Specialty;
            this.dateTime = appointment.DateAndTime.ToString("dd.MM.yyyy HH:mm");
            this.room = "Soba " + appointment.Room.Name;
            this.diagnoses = appointment.Diagnoses;
            this.note = appointment.DoctorsNotes;
        }

        public void Save()
        {
            appointmentController.LogAppointment(appointment, Diagnoses, Note);
        }
    }
}
