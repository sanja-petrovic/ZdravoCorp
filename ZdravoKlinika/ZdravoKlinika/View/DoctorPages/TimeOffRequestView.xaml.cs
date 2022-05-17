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

using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for TimeOffRequestView.xaml
    /// </summary>
    public partial class TimeOffRequestView : Window
    {
        private TimeOffRequestViewModel viewModel;

        public TimeOffRequestView(Doctor doctor)
        {
            this.viewModel = new TimeOffRequestViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(1)));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(1)));
            if (this.IsInitialized)
                Check();

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Save();
            this.Close();
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDatePicker != null && this.IsInitialized)
            {
                SpecialistsWarning.Visibility = Visibility.Collapsed;
                AppointmentsWarning.Visibility = Visibility.Collapsed;
                DuplicateWarning.Visibility = Visibility.Collapsed;
                WarningImg.Visibility = Visibility.Collapsed;
                ConfirmButton.IsEnabled = true;
                EndDatePicker.BlackoutDates.Clear();
                if (EndDatePicker.SelectedDate < StartDatePicker.SelectedDate)
                {
                    EndDatePicker.SelectedDate = StartDatePicker.SelectedDate;
                }
                DateTime date = (DateTime)StartDatePicker.SelectedDate;
                date = date.AddDays(-1);
                EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, date));
                Check();
            }
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsInitialized)
            {
                Check();
            }
        }

        private void Check()
        {
            if (this.viewModel.CheckAppointments())
            {
                AppointmentsWarning.Visibility = Visibility.Visible;
                WarningImg.Visibility = Visibility.Visible;
                ConfirmButton.IsEnabled = false;
            }
            else if (this.viewModel.CheckRequests())
            {
                SpecialistsWarning.Visibility = Visibility.Visible;
                WarningImg.Visibility = Visibility.Visible;
                ConfirmButton.IsEnabled = false;
            }
            else if (this.viewModel.CheckDuplicate())
            {
                DuplicateWarning.Visibility = Visibility.Visible;
                WarningImg.Visibility = Visibility.Visible;
                ConfirmButton.IsEnabled = false;
            }
            else
            {
                DuplicateWarning.Visibility = Visibility.Collapsed;
                SpecialistsWarning.Visibility = Visibility.Collapsed;
                AppointmentsWarning.Visibility = Visibility.Collapsed;
                WarningImg.Visibility = Visibility.Collapsed;
                ConfirmButton.IsEnabled = true;
            }
        }

        private void EmergencyCB_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void GiveUpButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmergencyCB_Unchecked(object sender, RoutedEventArgs e)
        {
            Check();
        }
    }
}
