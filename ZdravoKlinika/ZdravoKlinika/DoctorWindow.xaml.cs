using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace ZdravoKlinika
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
        public ObservableCollection<Appointment> Appointments { get; set; } 
        public DoctorWindow()
        { 
            InitializeComponent();
            this.DataContext = this;
            this.appointmentController = new AppointmentController();
            Appointments = new ObservableCollection<Appointment>(this.appointmentController.GetAll());

        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            DoctorCreateAppointment doctorCreateAppointment = new DoctorCreateAppointment();
            doctorCreateAppointment.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
           

            //this.appointmentController.EditAppointment(this.selectedIndex);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.appointmentController.DeleteAppointment(this.selectedIndex + 1);
            this.Appointments.RemoveAt(this.selectedIndex);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg == null) return;
            this.selectedIndex = dg.SelectedIndex;
        }
    }
}
