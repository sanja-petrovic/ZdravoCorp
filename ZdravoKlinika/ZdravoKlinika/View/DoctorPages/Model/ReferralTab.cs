using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Util;
using ZdravoKlinika.Model;
using System.Windows;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ReferralTab : ViewModelBase, ITabViewModel
    {
        private string header;
        private int appointmentId;
        private AppointmentController appointmentController;
        private Visibility successVisibility;

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public List<string> Specialties { get => specialties; set => SetProperty(ref specialties, value); }
        public List<Doctor> Doctors { get => doctors; set => SetProperty(ref doctors, value); }
        public ObservableCollection<string> DoctorsDisplay { get => doctorsDisplay; set => SetProperty(ref doctorsDisplay, value); }
        public String Type { get => type; set { SetProperty(ref type, value); SetTimes();  } }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public DateTime Date { get => date; set => SetProperty(ref date, value); }
        public List<Room> Rooms { get => rooms; set => SetProperty(ref rooms, value); }
        public DoctorController DoctorController { get => doctorController; set => doctorController = value; }
        public ObservableCollection<string> Types { get => types; set => SetProperty(ref types, value); }

        private List<String> specialties;
        private List<Doctor> doctors;
        private ObservableCollection<String> doctorsDisplay;
        private String type;
        private bool emergency;
        private DateTime date;
        private int duration;
        public ObservableCollection<String> Times { get; set; }
        public ObservableCollection<String> RoomsDisplay { get; set; }
        public DateTime DateAndTime { get => dateAndTime; set => SetProperty(ref dateAndTime, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public Room Room { get => room; set => SetProperty(ref room, value); }

        private Doctor doctor;
        private Room room;

        private List<Room> rooms;
        public ObservableCollection<String> types;
        private DateTime dateAndTime;

        private DoctorController doctorController;
        public MyICommand ScheduleCommand { get; set; }
        public Visibility SuccessVisibility { get => successVisibility; set => SetProperty(ref successVisibility, value); }

        public ReferralTab()
        {
            this.appointmentController = new AppointmentController();
            ScheduleCommand = new MyICommand(Schedule, CanExecuteSchedule);
        }

        public void Load()
        {
            this.DoctorController = new DoctorController();
            this.Specialties = DoctorController.GetAllSpecialties();
            this.Emergency = false;
            this.Date = DateTime.Today.Date;
            this.Doctors = new List<Doctor>();
            this.DoctorsDisplay = new ObservableCollection<String>();
            this.RoomsDisplay = new ObservableCollection<string>();
            this.Times = new ObservableCollection<string>();
            this.Rooms = new List<Room>();
            this.types = new ObservableCollection<string>();
            this.types.Add("Pregled");
            this.types.Add("Operacija");
            this.appointmentController = new AppointmentController();
        }

        public bool CanExecuteSchedule()
        {
            return Doctor != null && DateAndTime != null && Duration > 0;
        }

        public void SetDoctorComboBox(int selected)
        {
            Doctors = this.doctorController.GetBySpecialty(Specialties[selected]);
            DoctorsDisplay.Clear();
            foreach(Doctor doctor in Doctors)
            {
                DoctorsDisplay.Add(doctor.ToString());
            }
            ScheduleCommand.RaiseCanExecuteChanged();
        }

        public void SetRooms()
        {
            RoomsDisplay.Clear();
            RoomController roomController = new RoomController();
            if(Type == "Pregled")
            {
                this.rooms = roomController.GetFreeRooms(DateAndTime, RoomType.checkup);
            } else
            {
                this.rooms = roomController.GetFreeRooms(DateAndTime, RoomType.operating);
            }
            foreach(Room room in this.rooms)
            {
                RoomsDisplay.Add(room.Name);
            }
            ScheduleCommand.RaiseCanExecuteChanged();

        }

        public void SetTypeComboBox(int selected)
        {
            Types.Clear();
            Types.Add("Pregled");
            if (!Specialties[selected].Equals("Opšta praksa"))
            {
                Types.Add("Operacija");
            }
            ScheduleCommand.RaiseCanExecuteChanged();
        }

        public void SetSelectedDoctor(int selected)
        {
            if(selected != -1)
            {
                this.doctor = this.Doctors[selected];
            }
            SetTimes();
            ScheduleCommand.RaiseCanExecuteChanged();
        }

        public void SetDateTime(int selected)
        {
            if(Times.Count > 0)
            {
                string[] time = Times[selected].Split(":");
                int hours;
                Int32.TryParse(time[0], out hours);
                int minutes;
                Int32.TryParse(time[1], out minutes);

                DateAndTime = new DateTime(this.date.Year, this.date.Month, this.date.Day, hours, minutes, 0);
            }
            ScheduleCommand.RaiseCanExecuteChanged();
        }
        

        public void SetTimes()
        {
            Times.Clear();
            if(Doctor != null)
            {
                List<DateBlock> list = this.appointmentController.GetFreeTimeForUser(new DateBlock(Date, Duration), Doctor, new int[] { 12, 20 });
                foreach(DateBlock block in list)
                {
                    Times.Add(block.Start.ToShortTimeString());
                }
            }
            ScheduleCommand.RaiseCanExecuteChanged();

        }

        public void SetRoom(int selected)
        {
            if(selected != -1)
            {
                Room = this.rooms[selected];
            }
            ScheduleCommand.RaiseCanExecuteChanged();
        }

        public void Schedule()
        {
            this.appointmentController.CreateAppointment(Doctor.PersonalId, this.appointmentController.GetAppointmentById(this.appointmentId).Patient.GetPatientId(), DateAndTime, Emergency, AppointmentType.Regular, Room.RoomId, Duration);
            SuccessVisibility = Visibility.Visible;
        }
    }
}
