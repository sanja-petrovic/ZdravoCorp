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

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for PatientAddView.xaml
    /// </summary>
    
    public partial class PatientAddView : Page
    {
        private AppointmentController appointmentController = new AppointmentController();
        private DoctorController doctorController = new DoctorController();
        private RegisteredPatientController registeredPatientController = new RegisteredPatientController();
        private int appointmentDuration = 30;
        public PatientAddView()
        {
            InitializeComponent();
            priorityComboBox.Items.Add("Vreme");
            priorityComboBox.Items.Add("Doktor");
            priorityComboBox.SelectedIndex = -1;
        }
        /*private List<DateTime> getWorkingHours()
        {
            if (datePicker.SelectedDate != null)
            {
                List<DateTime> workingHours = new List<DateTime>();
                DateTime startTime = datePicker.SelectedDate.Value.Date.AddHours(8);
                DateTime endTime = datePicker.SelectedDate.Value.Date.AddHours(20);
                for (int i = 0; i < (endTime.Hour - startTime.Hour); i++)
                {
                    workingHours.Add(startTime.AddHours(i));
                }
                return workingHours;
            }
            return null;
        }*/

        private void priorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearCombos();
            if (priorityComboBox.SelectedIndex == 0)
            {
                //time prio
                
                //timeComboBox.ItemsSource = getWorkingHours();
                timeComboBox.SelectedIndex = 0;
                doctorComboBox.ItemsSource = null;

            }
            else if(priorityComboBox.SelectedIndex == 1)
            {
                
                doctorComboBox.ItemsSource = doctorController.GetAll();
                doctorComboBox.SelectedIndex = -1;
                timeComboBox.ItemsSource = null;
            }

        }

        private void timeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(priorityComboBox.SelectedIndex == 0)
            {
                if (timeComboBox.SelectedItem != null)
                {
                    doctorComboBox.ItemsSource = null;
                    List<Doctor> doctors = new List<Doctor>();
                    doctors = (appointmentController.getFreeDoctorsForTime((DateTime)timeComboBox.SelectedItem, appointmentDuration));
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
            if(priorityComboBox.SelectedIndex == 1)
            {
                if (doctorComboBox.SelectedItem != null)
                {
                    timeComboBox.ItemsSource = null;
                    List<DateTime> doctorTimes = appointmentController.getFreeTimeForDoctor(datePicker.SelectedDate.Value, 30, (Doctor)doctorComboBox.SelectedItem, 8, 20);
                    timeComboBox.ItemsSource = doctorTimes;
                    if (timeComboBox.ItemsSource == null)
                    {
                        errorLabel.Content = "no available appointment times";
                        errorLabel.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
