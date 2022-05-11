using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ReferralTab : ViewModelBase, ITabViewModel
    {
        private string header;
        private int appointmentId;
        private AppointmentController appointmentController;

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public List<string> Specialties { get => specialties; set => SetProperty(ref specialties, value); }
        public List<Doctor> Doctors { get => doctors; set => SetProperty(ref doctors, value); }
        public ObservableCollection<string> DoctorsDisplay { get => doctorsDisplay; set => SetProperty(ref doctorsDisplay, value); }
        public String Type { get => type; set => SetProperty(ref type, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public DateTime Date { get => date; set => SetProperty(ref date, value); }
        public List<string> Times { get => times; set => SetProperty(ref times, value); }
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
        private List<String> times;
        public ObservableCollection<String> RoomsDisplay { get; set; }
        public DateTime DateAndTime { get => dateAndTime; set => SetProperty(ref dateAndTime, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }

        private Doctor doctor;

        private List<Room> rooms;
        private ObservableCollection<String> types;
        private DateTime dateAndTime;

        private DoctorController doctorController;


        public ReferralTab()
        {
            
        }

        public void Load()
        {
            this.DoctorController = new DoctorController();
            this.Specialties = DoctorController.GetAllSpecialties();
            this.Emergency = false;
            this.Date = DateTime.Today;
            this.Doctors = new List<Doctor>();
            this.DoctorsDisplay = new ObservableCollection<String>();
            this.RoomsDisplay = new ObservableCollection<string>();
            this.Times = new List<String>();
            this.Rooms = new List<Room>();
            Room room = new Room();
            room.Name = "Hi";
            Rooms.Add(new Room());
            this.types = new ObservableCollection<string>();
            this.appointmentController = new AppointmentController();
        }

        public void SetDoctorComboBox(int selected)
        {
            Doctors = this.doctorController.GetBySpecialty(Specialties[selected]);
            DoctorsDisplay.Clear();
            foreach(Doctor doctor in Doctors)
            {
                DoctorsDisplay.Add(doctor.ToString());
            }
        }

        public void SetRooms()
        {
            RoomController roomController = new RoomController();
            this.rooms = roomController.GetFreeRooms(DateAndTime);
            foreach(Room room in this.rooms)
            {
                RoomsDisplay.Add(room.Name);
            }

        }

        public void SetTypeComboBox(int selected)
        {
            Types.Clear();
            Types.Add("Pregled");
            if (!Specialties[selected].Equals("Opšta praksa"))
            {
                Types.Add("Operacija");
            }
        }

        public Doctor SetSelectedDoctor(int selected)
        {
            return this.Doctors[selected];
        }

        public void SetDateTime(int selected)
        {
            string[] time = Times[selected].Split(":");
            int hours;
            Int32.TryParse(time[0], out hours);
            int minutes;
            Int32.TryParse(time[1], out minutes);

            DateAndTime = new DateTime(this.date.Year, this.date.Month, this.date.Day, hours, minutes, 0);
        }

        public void SetTimes()
        {
            this.appointmentController.getFreeTimeForDoctor(Date, Duration, Doctor, 8, 20);
        }

        public void Schedule(int selectedDoc, int selectedTime)
        {
            this.appointmentController.CreateAppointment(Doctor.PersonalId, this.appointmentController.GetAppointmentById(this.appointmentId).Patient.GetPatientId(), DateAndTime, Emergency, AppointmentType.Regular, "1", 15);
        }
    }
}
