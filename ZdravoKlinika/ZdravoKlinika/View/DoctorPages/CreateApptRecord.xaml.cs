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
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateApptRecord.xaml
    /// </summary>
    public partial class CreateApptRecord : Window
    {
        private AppointmentViewModel viewModel;

        public CreateApptRecord(String id)
        {
            viewModel = new AppointmentViewModel() { _Doctor = RegisteredUserController.UserToDoctor(App.User), PatientId = id };
            DataContext = viewModel;
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SetTimes();
        }

        private void TimePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SetRooms();
        }
    }
}
