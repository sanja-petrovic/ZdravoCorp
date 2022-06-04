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
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : UserControl
    {
        private int selectedAppointmentId;
        private HomePageViewModel viewModel;
        public DoctorHomePage()
        {
            
            viewModel = new HomePageViewModel();
            DataContext = viewModel;
            InitializeComponent();
            
        }

        private void RowSelectionChanged(object sender, RoutedEventArgs e)
        {

            var smth = (AppointmentViewModel)ScheduleDG.SelectedItem;
            this.selectedAppointmentId = smth.Id;
            viewModel.SelectionChanged(selectedAppointmentId);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            doctorMedicalRecord.Init(viewModel.PatientId);
            //this.NavigationService.Navigate(doctorMedicalRecord);
        }
    }
}
