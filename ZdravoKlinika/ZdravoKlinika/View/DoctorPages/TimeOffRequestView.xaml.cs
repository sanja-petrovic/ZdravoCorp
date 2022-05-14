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
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Save();
            this.Close();
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EndDatePicker != null)
            {
                EndDatePicker.BlackoutDates.Clear();
                EndDatePicker.SelectedDate = StartDatePicker.SelectedDate;
                DateTime date = (DateTime)StartDatePicker.SelectedDate;
                date = date.AddDays(-1);
                EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, date));
            }
        }
    }
}
