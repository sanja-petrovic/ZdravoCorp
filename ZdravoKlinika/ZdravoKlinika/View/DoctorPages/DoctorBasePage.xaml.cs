using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorBasePage.xaml
    /// </summary>
    public partial class DoctorBasePage : Window
    {
        Model.DoctorViewModel viewModel;

        private DoctorHomePage doctorHomePage;
        private DoctorSchedule doctorSchedule;

        public DoctorBasePage(RegisteredUser doctor)
        {
            this.viewModel = new Model.DoctorViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
            doctorHomePage = new DoctorHomePage(viewModel.Doctor);
            doctorSchedule = new DoctorSchedule(this.viewModel.Doctor);
            MainFrame.Navigate(doctorHomePage);
        }

        private void GoToSchedule(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorSchedule);
        }

        private void GoToHome(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorHomePage);
        }
    }
}
