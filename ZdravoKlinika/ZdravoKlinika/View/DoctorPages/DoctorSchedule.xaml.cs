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
        private DateTime selected;
        private Doctor doctor;
        public DoctorSchedule(Doctor doctor)
        {
            DataContext = this;
            this.doctor = doctor;
            InitializeComponent();
            ApptTabPanel.Parent1 = this;
            ApptTabPanel.Doctor = doctor;
            ApptTabPanel.Selected = DateTime.Today;
        }

        public DateTime Selected { get => selected; set => selected = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public void CalendarSelectionChanged(object sender, RoutedEventArgs e) {

            ApptTabPanel.Selected = (DateTime) Cal.SelectedDate;
        }

        public void goToMedicalRecord(string patientId)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord(Doctor);

            doctorMedicalRecord.init(patientId);
            this.NavigationService.Navigate(doctorMedicalRecord);
        }

        private void ViewDaysOff(object sender, RoutedEventArgs e)
        {
            TimeOffView timeOffView = new TimeOffView(Doctor);
            timeOffView.ShowDialog();
        }
    }
}
