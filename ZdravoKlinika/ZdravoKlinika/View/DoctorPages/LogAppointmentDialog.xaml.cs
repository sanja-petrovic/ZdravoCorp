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
    /// Interaction logic for LogAppointmentDialog.xaml
    /// </summary>
    public partial class LogAppointmentDialog : Window
    {

        private AppointmentLoggingViewModel viewModel;
        private int selectedAppointmentId;

        public int SelectedAppointmentId { get => selectedAppointmentId; set => selectedAppointmentId = value; }

        public LogAppointmentDialog()
        {
            //https://stackoverflow.com/questions/9796174/load-usercontrol-in-tabitem
        }

        public void init()
        {
            viewModel = new AppointmentLoggingViewModel();
            DataContext = viewModel;
            viewModel.AppointmentId = selectedAppointmentId;
            viewModel.load();
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Save(object sender, RoutedEventArgs e)
        {
            viewModel.save(NoteTB.Text);
            this.Close();
        }


        private void MedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.AllergyCheck(MedCB.SelectedIndex))
            {
                MedCB.Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 87));
                AllergyTB.Visibility = Visibility.Hidden;
                ConfirmButton.IsEnabled = true;
                AddButton.IsEnabled = true;
            }
            else
            {
                MedCB.Foreground = new SolidColorBrush(Color.FromRgb(254, 93, 122));
                AllergyTB.Visibility = Visibility.Visible;
                ConfirmButton.IsEnabled = false;
                AddButton.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (String)RepeatCB.SelectedItem;
            var note = (String)MedNoteTB.Text;
            viewModel.PrescriptionAdded(MedCB.SelectedIndex, selected, note);
        }
    }
}
