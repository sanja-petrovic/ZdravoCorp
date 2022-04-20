using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Model
{
    internal class ScheduleTabItem : ViewModelBase
    {
        private string time;
        private string patientId;
        private string patientName;
        private string appointmentType;
        private string room;
        private string lastDate;
        private string diagnoses;
        private string prescriptions;

        public string Time { get => time; set => SetProperty(ref time, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public string LastDate { get => lastDate; set => SetProperty(ref lastDate, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string AppointmentType { get => appointmentType; set => SetProperty(ref appointmentType, value); }

        public ScheduleTabItem()
        {

        }
    }
}
