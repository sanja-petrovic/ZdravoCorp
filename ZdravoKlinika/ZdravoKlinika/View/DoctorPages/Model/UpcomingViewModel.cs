using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.View.DialogHelper;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class UpcomingViewModel : ViewModelBase
    {
        private DoctorMedicalRecordViewModel parent; 
        private Visibility visibility;
        private int appointmentId;
        private string title;
        private string type;
        private string room;
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        private DialogHelper.DialogService dialogService; 
        private AppointmentController controller;
        private string patient;
        private string dateTime;
        private bool cancelled;


        public UpcomingViewModel()
        {
            DialogService = new DialogService();
            Controller = new AppointmentController();
            DeleteCommand = new MyICommand(ExecuteDelete);
            EditCommand = new MyICommand(ExecuteEdit);
            Visibility = cancelled ? Visibility.Collapsed : Visibility.Visible;
        }

        public void ExecuteEdit()
        {
            AppointmentController appointmentController = new AppointmentController();
            Appointment a = appointmentController.GetAppointmentById(appointmentId);
            AppointmentViewModel avm = new AppointmentViewModel();
            avm.LoadForEdit(a);
            DialogService.ShowEditAppt(avm);
            parent.EditedUpcoming();
        }

        public void ExecuteDelete()
        {
            DialogService.ShowPrompt("Otkazivanje pregleda", "Da li ste sigurni da želite da otkažete termin " + DateTime + "?", DeleteAppointment);
        }

        public void DeleteAppointment()
        {
            controller.DeleteAppointment(AppointmentId);
            parent.RemoveFromUpcoming(AppointmentId);
        }

        public int AppointmentId { get => appointmentId; set => appointmentId = value; }
        public string Title { get => title; set => title = value; }
        public string Type { get => type; set => type = value; }
        public string Room { get => room; set => room = value; }
        public DialogService DialogService { get => dialogService; set => dialogService = value; }
        public AppointmentController Controller { get => controller; set => controller = value; }
        public string Patient { get => patient; set => patient = value; }
        public string DateTime { get => dateTime; set => dateTime = value; }
        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }
        public DoctorMedicalRecordViewModel Parent { get => parent; set => parent = value; }

        public void init(Appointment appointment)
        {
            this.AppointmentId = appointment.AppointmentId;
            this.Title = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", " + appointment.Doctor.ToString();
            this.type = appointment.getTranslatedType();
            this.room = appointment.Room.Name;
            this.Patient = appointment.Patient.GetPatientFullName();
            this.DateTime = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm");
        }
    }
}
