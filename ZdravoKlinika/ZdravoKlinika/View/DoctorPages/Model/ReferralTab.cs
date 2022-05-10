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

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public List<string> Specialties { get => specialties; set => SetProperty(ref specialties, value); }
        public List<Doctor> Doctors { get => doctors; set => SetProperty(ref doctors, value); }
        public ObservableCollection<string> DoctorsDisplay { get => doctorsDisplay; set => SetProperty(ref doctorsDisplay, value); }
        public String Type { get => type; set => SetProperty(ref type, value); }
        public bool Emergency { get => emergency; set => SetProperty(ref emergency, value); }
        public DateTime Date { get => date; set => SetProperty(ref date, value); }
        public List<string> Times { get => times; set => SetProperty(ref times, value); }
        public List<string> Rooms { get => rooms; set => SetProperty(ref rooms, value); }
        public DoctorController DoctorController { get => doctorController; set => doctorController = value; }

        private List<String> specialties;
        private List<Doctor> doctors;
        private ObservableCollection<String> doctorsDisplay;
        private String type;
        private bool emergency;
        private DateTime date;
        private List<String> times;
        private List<String> rooms;

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
            this.Times = new List<String>();
            this.Rooms = new List<String>();
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
    }
}
