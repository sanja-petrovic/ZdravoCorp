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
        public DoctorSchedule()
        {
            DataContext = this;
            InitializeComponent();
            ApptTabPanel.Parent1 = this;
        }

        public DateTime Selected { get => selected; set => selected = value; }

        public void CalendarSelectionChanged(object sender, RoutedEventArgs e) {

            ApptTabPanel.Selected = (DateTime) Cal.SelectedDate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            doctorMedicalRecord.PatientId = "0105965123321";
            this.NavigationService.Navigate(doctorMedicalRecord);
        }

        public void goToMedicalRecord(string patientId)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();

            doctorMedicalRecord.init(patientId);
            this.NavigationService.Navigate(doctorMedicalRecord);
        }
    }
}
