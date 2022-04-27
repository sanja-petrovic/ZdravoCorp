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
using ZdravoKlinika.ViewModel;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentView.xaml
    /// </summary>
    public partial class PatientAppointmentView : Page
    {
        private String patientId;
        private PatientApointmentsViewModel viewModel;
        
        public PatientAppointmentView(String id)
        {
            patientId = id;
            InitializeComponent();
            viewModel = new PatientApointmentsViewModel();
            this.DataContext = viewModel;
            listBox.ItemsSource = viewModel.SelectedDateAppointments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popUpFrame.Visibility = Visibility.Visible;
            popUpFrame.Navigate(viewModel.PatientAddView);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            popUpFrame.Visibility = Visibility.Visible;
            popUpFrame.Navigate(viewModel.PatientEditView);
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.GetSelectedDateAppointments(calendar.SelectedDate.Value);
            dateLabel.Content = calendar.SelectedDate.ToString();
            
            if((calendar.SelectedDate.Value - DateTime.Today).TotalDays <= 0)
            {
                //today or in past
                resetButtons();
                buttonAdd.IsEnabled = false;
                buttonEdit.IsEnabled = false;
                buttonRemove.IsEnabled = false;
            }
            else
            {
                buttonAdd.IsEnabled = true;
                buttonEdit.IsEnabled = true;
                buttonRemove.IsEnabled = true;
            }
            listBox.ItemsSource = viewModel.SelectedDateAppointments;
        }
        private void resetButtons()
        {
            buttonAdd.IsEnabled = true;
            buttonEdit.IsEnabled = true;
            buttonRemove.IsEnabled = true;
            buttonComment.IsEnabled = true;
            buttonDocuments.IsEnabled = true;
        }
    }
    class LookupConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
             var date = (DateTime)values[0];
             var dates = values[1] as List<DateTime>;
             return dates.Contains(date);

            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
