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
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : UserControl
    {
        private int selectedAppointmentId;
        private AppointmentsTodayViewModel viewModel;
        public DoctorHomePage()
        {
            viewModel = new AppointmentsTodayViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void RowSelectionChanged(object sender, RoutedEventArgs e)
        {
            var smth = (AppointmentViewModel)ScheduleDG.CurrentItem;
            this.selectedAppointmentId = smth.Id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogAppointmentDialog logAppointmentDialog = new LogAppointmentDialog { SelectedAppointmentId = this.selectedAppointmentId };
            logAppointmentDialog.init();
            logAppointmentDialog.ShowDialog();
            viewModel.infoChange();
            
        }
    }
}
