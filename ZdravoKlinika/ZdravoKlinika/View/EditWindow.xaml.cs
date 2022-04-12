using System;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Appointment appointment;
        public EditWindow(Appointment appointment)
        {
            InitializeComponent();
            DataContext = this;
            this.appointment = appointment;
            this.durationTB.Text = appointment.Duration.ToString();
            this.datePicker.Text = appointment.DateAndTime.ToShortDateString();
            this.timeCB.Text = appointment.DateAndTime.ToShortTimeString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)patientCB.SelectedItem;
            var name = ComboItem.Content.ToString();
            string[] patientInfo = name.Split('(');
            string patientName = patientInfo[0];
            string patientId = patientInfo[1].Remove(patientInfo[1].Length - 1);
            ComboBoxItem ComboItem2 = (ComboBoxItem)timeCB.SelectedItem;
            string time = ComboItem2.Content.ToString();
            DateTime dateTime = new DateTime();
            string[] timeSplitted = time.Split(':');
            int hours;
            int minutes;
            Int32.TryParse(timeSplitted[0], out hours);
            Int32.TryParse(timeSplitted[1], out minutes);

            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                string formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dateTime = DateTime.Parse(formatted);
                dateTime = new DateTime(
                    selectedDate.Value.Year,
                    selectedDate.Value.Month,
                    selectedDate.Value.Day,
                    hours,
                    minutes,
                    0);
            }

            ComboBoxItem ComboItem3 = (ComboBoxItem)typeCB.SelectedItem;
            var type = ComboItem3.Content.ToString();
            AppointmentType actualType;
            if (type.Equals("Pregled"))
            {
                actualType = AppointmentType.Regular;
            }
            else
            {
                actualType = AppointmentType.Surgery;
            }

            ComboBoxItem ComboItem4 = (ComboBoxItem)roomCB.SelectedItem;
            var room = ComboItem4.Content.ToString();

            int duration;
            Int32.TryParse(durationTB.Text, out duration);

            AppointmentController appointmentController = new AppointmentController();
            appointmentController.EditAppointment(this.appointment.AppointmentId, this.appointment.DoctorId, patientId, dateTime, false, actualType, room, duration);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
