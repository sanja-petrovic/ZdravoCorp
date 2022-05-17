using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AppointmentViewModel : ViewModelBase
    {
        private string doctorName;
        private int id;
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
        public ObservableCollection<string> Patients { get; set; }
        public ObservableCollection<string> Doctors { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<string> Times { get; set; }
        public ObservableCollection<string> Rooms { get; set; }

        private RegisteredPatientController patientController;
        private DoctorController doctorController;

        public AppointmentViewModel()
        {
            Patients = new ObservableCollection<string>();
            patientController = new RegisteredPatientController();
            foreach(RegisteredPatient patient in patientController.GetAll())
            {
                Patients.Add(patient.GetPatientFullName());
            }
            Doctors = new ObservableCollection<string>();
            doctorController = new DoctorController();
            foreach(Doctor doctor in doctorController.GetAll())
            {
                Doctors.Add(doctor.ToString());
            }
            Types = new ObservableCollection<string>();
            Types.Add("Pregled");
            Types.Add("Operacija");
            Times = new ObservableCollection<string>();
            Rooms = new ObservableCollection<string>();
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

        public void SetTimes()
        {

        }

        public void SetRooms()
        {

        }
    }
}
