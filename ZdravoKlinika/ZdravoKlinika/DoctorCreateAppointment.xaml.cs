using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoKlinika
{
    public partial class DoctorCreateAppointment : Window
    {

        private String type;
        private String time;
        private DateTime date;
        private String patient;
        private bool opened;

        Appointment createdAppointment;
        public DoctorCreateAppointment()
        {
            this.Show();
            InitializeComponent();
            DataContext = this;
            Opened = true;
        }

        public String Type
        {
            get => type;
            set
            {
                if(type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Patient
        {
            get => patient;
            set
            {
                if(patient != value)
                {
                    patient = value;
                    OnPropertyChanged();
                }
            }
        }

        public Appointment CreatedAppointment { get => createdAppointment; set => createdAppointment = value; }
        public bool Opened { get => opened; set => opened = value; }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)patientCB.SelectedItem;
            var name = ComboItem.Content.ToString();
            string[] patientInfo = name.Split('(');
            string patientName = patientInfo[0];
            string patientId = patientInfo[1].Remove(patientInfo[1].Length - 1);
            ComboBoxItem ComboItem2 = (ComboBoxItem)timeCB.SelectedItem;
            this.time = ComboItem2.Content.ToString();
            string[] timeSplitted = time.Split(':');
            int hours;
            int minutes;
            Int32.TryParse(timeSplitted[0], out hours);
            Int32.TryParse(timeSplitted[1], out minutes);

            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                string formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                this.date = DateTime.Parse(formatted);
                this.date = new DateTime(
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
            if(type.Equals("Pregled"))
            {
                actualType = AppointmentType.Regular;
            } else
            {
                actualType = AppointmentType.Surgery;
            }

            ComboBoxItem ComboItem4 = (ComboBoxItem)roomCB.SelectedItem;
            var room = ComboItem4.Content.ToString();

            int duration;
            Int32.TryParse(durationTB.Text, out duration);

            AppointmentController appointmentController = new AppointmentController();
            this.CreatedAppointment = appointmentController.CreateAppointment("D1", patientId, date, false, actualType, room, duration);
            Close();
            

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
