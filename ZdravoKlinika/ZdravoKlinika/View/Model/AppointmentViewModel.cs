using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Model
{
    internal class AppointmentViewModel : ViewModelBase
    {
        private string patientName;
        private string patientId;
        private string appointmentType;
        private string dateOfLatestAppt;
        private string room;
        private string diagnoses;
        private string prescriptions;
        private AppointmentController appointmentController;
        private string doctorId;
        private DateTime dateTime;

        public AppointmentViewModel()
        {   
            this.appointmentController = new AppointmentController();
            this.appointmentController.GetAppointmentByDoctorDateTime(DoctorId, DateTime);
        }

        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string AppointmentType { get => appointmentType; set => SetProperty(ref appointmentType, value); }
        public string DateOfLatestAppt { get => dateOfLatestAppt; set => SetProperty(ref dateOfLatestAppt, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string DoctorId { get => doctorId; set => doctorId = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
