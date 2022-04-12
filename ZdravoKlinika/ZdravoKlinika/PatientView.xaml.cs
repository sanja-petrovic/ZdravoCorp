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
using System.Windows.Shapes;

namespace ZdravoKlinika
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        private AppointmentController controller = new AppointmentController();
        public PatientView()
        {
            InitializeComponent();
            this.DataContext = this;
            datePicker.SelectedDate = DateTime.Now;
            appointmentTable.ItemsSource = controller.GetAll();
        }

        private void apointmentTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment app = (Appointment)appointmentTable.SelectedItem;
            if (app != null)
            {
                doctorIdTextBox.Text = app.DoctorId;
                datePicker.SelectedDate = app.DateAndTime;
                roomIdTextBox.Text = app.RoomId;
                appointmentTypeTextBox.Text = app.Type.ToString();
                durationTextBox.Text = app.Duration.ToString();
                appointmentIdTextBox.Text = app.AppointmentId.ToString();
            }
        }


        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            controller.CreateAppointment(
                doctorIdTextBox.Text,
                "5",
                (DateTime)datePicker.SelectedDate,
                false,
                AppointmentType.Regular,
                roomIdTextBox.Text,
                30
                );
            appointmentTable.ItemsSource = controller.GetAll();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            controller.EditAppointment(
                    Int32.Parse(appointmentIdTextBox.Text),
                    "5",
                    doctorIdTextBox.Text,
                    (DateTime)datePicker.SelectedDate,
                    false,
                    AppointmentType.Regular,
                    roomIdTextBox.Text,
                    30
                );
            appointmentTable.ItemsSource = controller.GetAll();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            controller.DeleteAppointment(Int32.Parse(appointmentIdTextBox.Text));
            appointmentTable.ItemsSource = controller.GetAll();
        }
    }
}
