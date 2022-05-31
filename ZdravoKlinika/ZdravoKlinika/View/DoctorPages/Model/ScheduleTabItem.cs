using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ScheduleTabItem : ViewModelBase
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

        public DoctorMedicalRecordViewModel RecordViewModel { get => recordViewModel; set => SetProperty(ref recordViewModel, value); }
        public MyICommand RecordCommand { get; set; }
        private DoctorMedicalRecordViewModel recordViewModel;

        public ScheduleTabItem()
        {
            RecordCommand = new MyICommand(ExecuteGoToRecord);
        }

        public void ExecuteGoToRecord()
        {
            Navigation.Navigator navigator = new Navigation.Navigator();
            navigator.ShowMedicalRecord(PatientId);
        }


    }
}
