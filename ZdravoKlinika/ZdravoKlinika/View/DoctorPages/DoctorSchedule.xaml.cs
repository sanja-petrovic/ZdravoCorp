using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class DoctorSchedule : Page
    {
        private DoctorController doctorController;
        private AppointmentController appointmentController;
        private String doctorsName = "Dr Nah";
        private String doctorsTitle = "Nah";
        private List<Appointment> appointments = new List<Appointment>();
        private List<String> times;
        public DoctorSchedule()
        {
            InitializeComponent();
            this.doctorController = new DoctorController();
            this.appointmentController = new AppointmentController();
            DataContext = this;
            Doctor doc = this.doctorController.GetByEmail("drhouse@zdravo.com");
            this.doctorsName = "Dr " + doc.Name + " " + doc.Lastname;
            this.doctorsTitle = doc.Specialty;

            this.appointments = this.appointmentController.GetAppointmentsByDoctorId(doc.PersonalId);
            ShowDayInfo();

            

        }

        public void ShowDayInfo()
        {
            this.times = new List<String>();
            var selectedDate = Cal.SelectedDate.ToString();
            if (String.IsNullOrEmpty(selectedDate))
            {
                selectedDate = DateTime.Now.ToShortDateString();
            }
            foreach (Appointment a in this.appointments)
            {
                if (selectedDate.Equals(a.DateAndTime.ToShortDateString()))
                {
                    times.Add(a.DateAndTime.ToShortTimeString());
                }
            }
        }

        public void CalendarSelectionChanged(object sender, RoutedEventArgs e)
        {
            ShowDayInfo();
            Tabs.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public List<String> Times
        {
            get => this.times;
            set
            {
                if(this.times != value)
                {
                    this.times = value;
                    OnPropertyChanged();
                }
            }
        }
        public String DoctorsName
        {
            get => this.doctorsName;
            set
            {
                if (this.doctorsName != value)
                {
                    this.doctorsName = value;
                    OnPropertyChanged();
                }
            }
        }
        public String DoctorsTitle
        {
            get => this.doctorsTitle;
            set
            {
                if (this.doctorsTitle != value)
                {
                    this.doctorsTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Appointment> Appointments
        {
            get => this.appointments;
            set
            {
                if(this.appointments != value)
                {
                    this.appointments = value;
                    OnPropertyChanged();
                }
            }
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        private void Cal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
