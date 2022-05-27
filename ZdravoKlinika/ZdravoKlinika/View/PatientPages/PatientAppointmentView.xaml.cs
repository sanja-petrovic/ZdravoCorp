using System;
using System.Collections.Generic;
using System.Globalization;
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
using ZdravoKlinika.PatientPages.ViewModel;
using ZdravoKlinika.View.PatientPages.ViewModel;


namespace ZdravoKlinika.View.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientAppointmentView.xaml
    /// </summary>
    public partial class PatientAppointmentView : Page
    {
        private String patientId;
        private PatientApointmentsViewModel viewModel;
        private Appointment selectedInList;
        private ZdravoKlinika.Util.DatePickerRestrictors restrictor = new Util.DatePickerRestrictors();
        private AppointmentController appointmentController = new AppointmentController();
        public PatientAppointmentView(String id)
        {
            patientId = id;
            InitializeComponent();
            viewModel = new PatientApointmentsViewModel(patientId);
            this.DataContext = viewModel;
            listBox.ItemsSource = viewModel.SelectedDateAppointments;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            popUpFrame.Visibility = Visibility.Visible;
            viewModel.PatientAddView = new PatientAddView(patientId);
            popUpFrame.Navigate(viewModel.PatientAddView);
            
           
            restrictor.setDatePickerBlackoutForward(DateTime.Now.AddDays(2), viewModel.PatientAddView.datePicker);

            if (calendar.SelectedDate != null)
            {
                
                try
                {
                    viewModel.PatientAddView.datePicker.SelectedDate = calendar.SelectedDate.Value;
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            popUpFrame.Visibility = Visibility.Visible;
            viewModel.PatientEditView = new PatientEditView(selectedInList.AppointmentId, patientId);
            popUpFrame.Navigate(viewModel.PatientEditView);
            if(listBox.SelectedItem != null)
            {
                
                restrictor.setDatePickerBlackoutRange(restrictor.getValidDateRange((DateTime)calendar.SelectedDate.Value,2), viewModel.PatientEditView.datePicker);
                try
                {
                    viewModel.PatientEditView.datePicker.SelectedDate = calendar.SelectedDate.Value;
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);

                }
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                try
                {
                    appointmentController.PatientDeleteAppointment(selectedInList.AppointmentId, patientId);
                    resetBaseView();
                }
                catch (Exception ex)
                {
                    //TODO localise this later and call logout!
                    MessageBox.Show("Previse puta ste izmenili pregled, rad ce privremeno biti onemogucen obratite se sekretaru", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
                    forcedLogout();
                }
            }
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
        private void forcedLogout()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "patientBase")
                {
                    PatientViewBase baseWindow = (PatientViewBase)window;
                    SignInWindow signInWindow = new SignInWindow();
                    signInWindow.Show();
                    baseWindow.Close();
                }
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.GetSelectedDateAppointments(calendar.SelectedDate.Value);
            dateLabel.Content = (DateTime)calendar.SelectedDate.Value.Date;
            listBox.SelectedIndex = -1;

            if ((calendar.SelectedDate.Value - DateTime.Today.AddDays(2)).TotalDays <= 0)
            {
                //today or in past
                buttonAdd.IsEnabled = false;
            }
            else
            {
                buttonAdd.IsEnabled = true;
            }
            
            listBox.ItemsSource = viewModel.SelectedDateAppointments;
            popUpFrame.Navigate(null);
            popUpFrame.Visibility = Visibility.Hidden;
            popUpFrame2.Navigate(null);
            popUpFrame2.Visibility = Visibility.Hidden;
        }
        private void resetButtons()
        {
            buttonAdd.IsEnabled = false;
            buttonEdit.IsEnabled = false;
            buttonRemove.IsEnabled = false;
            buttonComment.IsEnabled = false;
            buttonDocuments.IsEnabled = false;
        }
        
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                selectedInList = (Appointment)listBox.SelectedItem;
                timeLabel.Content = selectedInList.DateAndTime.TimeOfDay.ToString();
                doctorLabel.Content = selectedInList.Doctor.Name.ToString() + " " + selectedInList.Doctor.Lastname.ToString();
                roomLabel.Content = selectedInList.Room.Name.ToString();
                typeLabel.Content = selectedInList.Type.ToString();

                if ((calendar.SelectedDate.Value - DateTime.Now).TotalSeconds > 0) //Future appointment
                {
                    buttonAdd.IsEnabled = false;
                    buttonEdit.IsEnabled = true;
                    buttonRemove.IsEnabled = true;
                    buttonComment.IsEnabled = false;
                    buttonDocuments.IsEnabled = false;
                }
                else //past appointment
                {
                    PatientDiagnosisView diagnosisView = new PatientDiagnosisView(selectedInList);
                    popUpFrame.Navigate(diagnosisView);
                    popUpFrame.Visibility = Visibility.Visible;

                    buttonAdd.IsEnabled = false;
                    buttonEdit.IsEnabled = false; 
                    buttonRemove.IsEnabled = false;
                    if (selectedInList.Grading == null)
                    {
                        buttonComment.IsEnabled = true;
                    }
                    else
                    {
                        buttonComment.IsEnabled = false;
                    }
                    buttonDocuments.IsEnabled = true;
                }
            }
            else
            {
                resetButtons();
            }

        }

        private void buttonComment_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentReviewView reviewView = new PatientAppointmentReviewView(selectedInList.AppointmentId);
            popUpFrame2.Navigate(reviewView);
            popUpFrame2.Visibility = Visibility.Visible;
        }
    }
}
