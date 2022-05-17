using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AppointmentViewModel : ViewModelBase
    {
        private Doctor _doctor;
        private Patient _patient;
        private string doctorName;
        private int id;
        private string patientId;
        private string doctorId;
        private string name;
        private string type;
        private string time;
        private string room;
        private string diagnosis;
        private string prescriptions;
        private string opinion;
        private bool emergency;
        private int duration;
        private DateTime date;

        public ObservableCollection<RegisteredPatient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<string> Times { get; set; }
        public ObservableCollection<string> Rooms { get; set; }

        private MyICommand CreateCommand;


        private RegisteredPatientController patientController;
        private DoctorController doctorController;
        private AppointmentController appointmentController;

        public AppointmentViewModel()
        {
            patientController = new RegisteredPatientController();
            Patients = new ObservableCollection<RegisteredPatient>(patientController.GetAll());
            doctorController = new DoctorController();
            Doctors = new ObservableCollection<Doctor>(doctorController.GetAll());
            Types = new ObservableCollection<string>();
            Types.Add("Pregled");
            Types.Add("Operacija");
            Times = new ObservableCollection<string>();
            Rooms = new ObservableCollection<string>();

            CreateCommand = new MyICommand(OnCreate);
            this.appointmentController = new AppointmentController();
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }
        public string Time { get => time; set => SetProperty(ref time, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public int Id { get => id; set => id = value; }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string Opinion { get => opinion; set => SetProperty(ref opinion, value); }
        public string DoctorName { get => doctorName; set => SetProperty(ref doctorName, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public DateTime Date { get => date; set => SetProperty(ref date, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string DoctorId { get => doctorId; set => SetProperty(ref doctorId, value); }
        public Doctor _Doctor { get => _doctor; set => SetProperty(ref _doctor, value); }
        public Patient _Patient { get => _patient; set => SetProperty(ref _patient, value); }

        public void SetTimes()
        {

        }

        public void SetRooms()
        {

        }

       private void OnCreate()
        {
            this.appointmentController.CreateAppointment(DoctorId, PatientId, Date, Emergency, AppointmentType.Surgery, Room, Duration);
        }
    }
}
