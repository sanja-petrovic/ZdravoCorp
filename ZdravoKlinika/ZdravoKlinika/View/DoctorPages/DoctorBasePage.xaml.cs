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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Util;

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
        private DoctorMedicationsView doctorMedicationsView;
        private DoctorProfileView doctorProfileView;
        private DoctorAllPatientsView doctorAllPatientsView;

        public DoctorBasePage(RegisteredUser doctor)
        {
            this.viewModel = new Model.DoctorViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
            doctorHomePage = new DoctorHomePage(viewModel.Doctor);
            doctorSchedule = new DoctorSchedule(this.viewModel.Doctor);
            doctorMedicationsView = new DoctorMedicationsView(this.viewModel.Doctor);
            doctorProfileView = new DoctorProfileView(this.viewModel.Doctor);
            doctorAllPatientsView = new DoctorAllPatientsView(this.viewModel.Doctor);
            MainFrame.Navigate(doctorHomePage);
            AppointmentService service = new AppointmentService();
            DoctorService doctorService = new DoctorService();
            PatientController patientController = new PatientController();
            List<DateBlock> blocks = service.GetFreeTime(doctorService.GetById("123"), patientController.GetById("0105965123321"), new DateBlock(DateTime.Today, 30));

        }

        private void GoToSchedule(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorSchedule);
        }

        private void GoToHome(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorHomePage);
        }


        private void GoToMeds(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorMedicationsView);
        }

        private void GoToProfile(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorProfileView);
        }

        private void GoToPatients(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorAllPatientsView);
        }

        private void SignOut(object sender, MouseButtonEventArgs e)
        {
            RegisteredUserController registeredUserController = new RegisteredUserController();
            registeredUserController.ForgetUser();
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }
    }
}
