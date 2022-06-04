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
using ZdravoKlinika.Util;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientAddView.xaml
    /// </summary>

    public partial class PatientAddView : Page
    {
        private string patientId;
        private AppointmentController appointmentController = new AppointmentController();
        private DoctorController doctorController = new DoctorController();
        private RegisteredPatientController registeredPatientController = new RegisteredPatientController();
        private RoomController roomController = new RoomController();
        private int appointmentDuration = 30;

        public PatientAddView(String id)
        {
            patientId = id;
            InitializeComponent();
            priorityComboBox.Items.Add("Vreme");
            priorityComboBox.Items.Add("Doktor");
            priorityComboBox.SelectedIndex = -1;
        }

        private void priorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetError();
            clearCombos();
            if (priorityComboBox.SelectedIndex == 0)
            {
                //time prio

                timeComboBox.ItemsSource = DateBlock.getStartTimes(appointmentController.getFreeTimeForPatient(datePicker.SelectedDate.Value, 15, registeredPatientController.GetById(patientId), 8, 20));
                timeComboBox.SelectedIndex = -1;
                doctorComboBox.ItemsSource = null;

            }
            else if (priorityComboBox.SelectedIndex == 1)
            {

                doctorComboBox.ItemsSource = doctorController.GetAll();
                doctorComboBox.SelectedIndex = -1;
                timeComboBox.ItemsSource = null;
            }

        }

        private void timeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetError();
            if (priorityComboBox.SelectedIndex == 0)
            {
                if (timeComboBox.SelectedItem != null)
                {
                    doctorComboBox.ItemsSource = null;
                    List<Doctor> doctors = new List<Doctor>();
                    doctors = appointmentController.getFreeDoctorsForTime(new DateBlock((DateTime)timeComboBox.SelectedItem, 15), 8, 20);
                    if (doctors.Any())
                    {
                        doctorComboBox.ItemsSource = doctors;
                        doctorComboBox.SelectedIndex = -1;

                    }
                    else
                    {
                        errorLabel.Content = "No doctors available at given time";
                    }
                }
            }
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            resetError();
            priorityComboBox.SelectedIndex = -1;
            clearCombos();
        }
        private void clearCombos()
        {
            doctorComboBox.ItemsSource = null;
            timeComboBox.ItemsSource = null;
        }

        private void doctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetError();
            if (priorityComboBox.SelectedIndex == 1)
            {
                if (doctorComboBox.SelectedItem != null)
                {
                    timeComboBox.ItemsSource = null;
                    List<DateBlock> doctorTimes = appointmentController.getFreeTimeForDoctor(datePicker.SelectedDate.Value, 30, (Doctor)doctorComboBox.SelectedItem, 8, 20);
                    List<DateBlock> patientTimes = appointmentController.getFreeTimeForPatient(datePicker.SelectedDate.Value, 30, registeredPatientController.GetById(patientId), 8, 20);
                    timeComboBox.ItemsSource = DateBlock.getStartTimes(DateBlock.getIntersection(doctorTimes, patientTimes));

                    if (timeComboBox.ItemsSource == null)
                    {
                        errorLabel.Content = "no available appointment times";
                        errorLabel.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            resetError();
            
            if (doctorComboBox.SelectedIndex != -1 && timeComboBox.SelectedIndex != -1)
            {
                // TODO finish this
                List<Room> rooms = roomController.GetFreeRooms((DateTime)timeComboBox.SelectedItem, RoomType.checkup);
                if (!rooms.Any())
                {
                    errorLabel.Content = "no available rooms times";
                    errorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    Doctor doctor = (Doctor)doctorComboBox.SelectedItem;

                    appointmentController.CreateAppointment(doctor.PersonalId, patientId, (DateTime)timeComboBox.SelectedItem, false, AppointmentType.Regular, rooms[0].RoomId, appointmentDuration);
                    resetBaseView();
                }
            }
            else
            {
                errorLabel.Content = "check data entry";
                errorLabel.Visibility = Visibility.Visible;
            }

        }

        private void resetError()
        {
            errorLabel.Content = "";
            errorLabel.Visibility = Visibility.Hidden;
        }
        private void resetBaseView()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "patientBase")
                {
                    PatientViewBase baseWindow = (PatientViewBase)window;
                    baseWindow.refreshAppointmentView();
                }
            }
        }
    }
}
