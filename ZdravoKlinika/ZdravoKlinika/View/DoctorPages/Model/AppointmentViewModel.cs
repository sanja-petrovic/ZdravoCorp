using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AppointmentViewModel : ViewModelBase
    {
        private Doctor _doctor;
        private IPatient _patient;
        private string doctorName;
        private int id;
        private string patientId;
        private string doctorId;
        private string name;
        private string type;
        private string time;
        private Room room;
        private string diagnosis;
        private string prescriptions;
        private string opinion;
        private bool emergency;
        private int duration;
        private DateTime date;
        private DateBlock _time;
        private ViewModelBase parent;

        public ObservableCollection<RegisteredPatient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<string> Types { get; set; }
        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                SetProperty(ref rooms, value);
            }
        }
        private ObservableCollection<DateBlock> times;
        public ObservableCollection<DateBlock> Times
        {
            get { return times; }
            set
            {
                SetProperty(ref times, value);
            }
        }

        public MyICommand CreateAppointment { get; set; }
        public MyICommand EditAppointment { get; set; }
        public MyICommand GiveUpCommand { get; set; }


        private RegisteredPatientController patientController;
        private DoctorController doctorController;
        private AppointmentController appointmentController;
        private RoomController roomController;

        public AppointmentViewModel()
        {
            CreateAppointment = new MyICommand(ExecuteCreate);
            EditAppointment = new MyICommand(ExecuteEdit);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
            _Doctor = RegisteredUserController.UserToDoctor(App.User);
            DoctorId = _Doctor.PersonalId;
            patientController = new RegisteredPatientController();
            Patients = new ObservableCollection<RegisteredPatient>(patientController.GetAll());
            doctorController = new DoctorController();
            Doctors = new ObservableCollection<Doctor>(doctorController.GetAll());
            Types = new ObservableCollection<string>();
            Types.Add("Pregled");
            Types.Add("Operacija");
            Times = new ObservableCollection<DateBlock>();
            Rooms = new ObservableCollection<Room>();
            Date = DateTime.Today.AddDays(1);

            this.appointmentController = new AppointmentController();
            this.roomController = new RoomController();
        }

        public void LoadForEdit(Appointment a)
        {
            Id = a.AppointmentId;
            _Doctor = a.Doctor;
            _Patient = a.Patient;
            Name = a.Patient.ToString();
            Type = a.Type == AppointmentType.Regular ? "Pregled" : "Operacija";
            Date = a.DateAndTime.Date;
            Duration = a.Duration;
            Emergency = a.Emergency;
            Room = a.Room;

        }

        public void ExecuteGiveUp()
        {
            DialogHelper.DialogService.CloseDialog(this);
        }

        public void ExecuteEdit()
        {
            DateTime datetime = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Start.Hour, Time1.Start.Minute, 0);
            appointmentController.EditAppointment(Id, _Doctor, _Patient, datetime, Emergency, Type.Equals("Pregled") ? AppointmentType.Regular : AppointmentType.Surgery, Room, Duration);
            DialogHelper.DialogService.CloseDialog(this);
        }

        public void ExecuteCreate()
        {
            DateTime datetime = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Start.Hour, Time1.Start.Minute, 0);
            this.appointmentController.CreateAppointment(DoctorId, _Patient.GetPatientId(), datetime, Emergency, Type.Equals("Pregled") ? AppointmentType.Regular : AppointmentType.Surgery, Room.RoomId, Duration);
            DialogHelper.DialogService.CloseDialog(this);

        }
        public bool CanExecuteCreate()
        {
            return DoctorId != null && PatientId != null  && Type != null && Room != null && Duration != null;
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }
        public string Time { get => time; set => SetProperty(ref time, value); }
        public Room Room { get => room; set => SetProperty(ref room, value); }
        public int Id { get => id; set => id = value; }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string Opinion { get => opinion; set => SetProperty(ref opinion, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public bool Emergency { get => emergency; set { SetProperty(ref emergency, value); CreateAppointment.RaiseCanExecuteChanged(); } }
        public int Duration { get => duration; set { SetProperty(ref duration, value); CreateAppointment.RaiseCanExecuteChanged(); SetTimes(); } }
        public DateTime Date { get => date; set  { SetProperty(ref date, value); CreateAppointment.RaiseCanExecuteChanged(); SetTimes(); }  }
        public string PatientId { get => patientId; set { SetProperty(ref patientId, value); _Patient = patientController.GetById(patientId); Name = _Patient.ToString(); } }
        public string DoctorId { get => doctorId; set => SetProperty(ref doctorId, value); }
        public Doctor _Doctor { get => _doctor; set => SetProperty(ref _doctor, value); }
        public IPatient _Patient { get => _patient; set => SetProperty(ref _patient, value); }
        public DateBlock Time1 { get => _time; set { SetProperty(ref _time, value); CreateAppointment.RaiseCanExecuteChanged(); SetRooms(); } }

        public ViewModelBase Parent { get => parent; set => SetProperty(ref parent, value); }

        public void SetRooms()
        {
            if(Date != null && Time1 != null)
            {
                Room = null;
                DateTime datetime = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Start.Hour, Time1.Start.Minute, 0);
                RoomType roomType = Type.Equals("Pregled") ? RoomType.checkup : RoomType.operating;
                Rooms = new ObservableCollection<Room>(this.roomController.GetOccupiedRooms(datetime, Duration, roomType));
                CreateAppointment.RaiseCanExecuteChanged();
            }
        }

        public void Set()
        {
            SetTimes();
            SetRooms();
        }

        public void SetTimes()
        {
            if (_Patient != null)
            {
                Time1 = null;
                Times = new ObservableCollection<DateBlock>(this.appointmentController.GetFreeTime(DoctorId, _Patient.GetPatientId(), new DateBlock(Date, Duration)));
                CreateAppointment.RaiseCanExecuteChanged();
            }
        }

    }
}
