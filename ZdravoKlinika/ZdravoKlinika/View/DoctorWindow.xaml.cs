using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;
namespace ZdravoKlinika.View
{
    public partial class DoctorWindow : Window
    {
        private int selectedIndex = -1;
        private DataGridRow selectedRow;
        private int colNum = 0;
        private AppointmentController appointmentController;
        private DateTime dateAndTime;
        private String patientName;
        private String appointmentType;
        private String emergency;
        public DoctorWindow()
        { 
            InitializeComponent();
            this.DataContext = this;
            this.appointmentController = new AppointmentController();
            refresh();

        }


        public void refresh()
        {
            List<Appointment> appointments = this.appointmentController.GetAll();
            dataGridDoctorsAppointments.ItemsSource = null;
            dataGridDoctorsAppointments.ItemsSource = appointments;
        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            DoctorCreateAppointment doctorCreateAppointment = new DoctorCreateAppointment();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            Appointment selected = dataGridDoctorsAppointments.SelectedItem as Appointment;
            EditWindow editWindow = new EditWindow(selected);
            editWindow.Show();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Appointment selected = dataGridDoctorsAppointments.SelectedItem as Appointment;
            this.appointmentController.DeleteAppointment(selected.AppointmentId);
            refresh();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg == null) return;
            this.selectedIndex = dg.SelectedIndex;
        }
    }
}
