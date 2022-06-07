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

        private ApptLogViewModel viewModel;
        private int selectedAppointmentId;

        public int SelectedAppointmentId { get => selectedAppointmentId; set => selectedAppointmentId = value; }

        public LogAppointmentDialog()
        {
        }

        public void Init()
        {
            viewModel = new ApptLogViewModel();
            viewModel.AppointmentId = SelectedAppointmentId;
            viewModel.Init();
            DataContext = viewModel;
            InitializeComponent();
            Tabby.ItemsSource = viewModel.TabViewModels;
        }

        private void Tabby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ConfirmButton != null)
            {
                if (Tabby.SelectedIndex != 0)
                {
                    ConfirmButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    ConfirmButton.Visibility = Visibility.Visible;
                }
            }

        }

    }
}
