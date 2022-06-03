using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoKlinika.View.DialogHelper;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ScheduleTabItem : ViewModelBase
    {
        private string time;
        private int apptId;
        private string patientId;
        private string patientName;
        private string appointmentType;
        private string room;
        private string lastDate;
        private string diagnoses;
        private string prescriptions;
        private bool emergency;
        private string duration;
        private DateTime blackoutDate;
        private DialogHelper.DialogService dialogService;
        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms { get { return rooms; } set { SetProperty(ref rooms, value); } }
        private ObservableCollection<String> times;
        public ObservableCollection<String> Times { get { return times; } set { SetProperty(ref times, value); } }
        private ScheduleViewModel parent;
        private Visibility visible;

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
        public MyICommand CancelCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public DateTime BlackoutDate { get => blackoutDate; set => SetProperty(ref blackoutDate, value); }
        public string Duration { get => duration; set => SetProperty(ref duration, value); }
        public DialogService DialogService { get => dialogService; set => dialogService = value; }
        public int ApptId { get => apptId; set => SetProperty(ref apptId, value); }
        internal ScheduleViewModel Parent { get => parent; set => parent = value; }
        public Visibility Visible { get => visible; set => SetProperty(ref visible, value); }

        private AppointmentController appointmentController;

        private DoctorMedicalRecordViewModel recordViewModel;

        public ScheduleTabItem()
        {
            RecordCommand = new MyICommand(ExecuteGoToRecord);
            CancelCommand = new MyICommand(ExecuteCancel);
            this.appointmentController = new AppointmentController();
            this.dialogService = new DialogService();
        }

        public void ExecuteCancel()
        {
            DialogService.ShowPrompt("Otkazivanje pregleda", "Da li ste sigurni da želite da otkažete termin " + Time + "?", DeleteAppointment);
        }

        public void DeleteAppointment()
        {
            this.appointmentController.DeleteAppointment(ApptId);
            parent.InfoChange();
        }

        public void ExecuteGoToRecord()
        {
            Navigation.Navigator navigator = new Navigation.Navigator();
            navigator.ShowMedicalRecord(PatientId);
        }


    }
}
