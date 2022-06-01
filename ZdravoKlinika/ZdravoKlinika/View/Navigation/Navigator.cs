using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.View.DoctorPages;

namespace ZdravoKlinika.View.Navigation
{
    public class Navigator
    {
        private static Window mainWindow; 
        public static Window MainWindow { get => mainWindow; set => mainWindow = value; }

        public Navigator()
        {
        }
        
        public static void CloseMainAndOpenSignIn()
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            MainWindow.Close();
            MainWindow = signInWindow;
        }

        public static void ShowDoctorWindow()
        {
            DoctorBasePage doctorBasePage = new DoctorBasePage();
            doctorBasePage.Show();
            MainWindow.Close();
            MainWindow = doctorBasePage;
        }

        public static void ShowManagerWindow()
        {
            UpravnikWindow upravnikWindow = new UpravnikWindow();
            upravnikWindow.Show();
            MainWindow.Close();
            MainWindow = upravnikWindow;
        }

        public static void ShowSecretaryWindow()
        {
            Secretary.SecretaryMainWindow secretaryMainWindow = new Secretary.SecretaryMainWindow();
            secretaryMainWindow.Show();
            MainWindow.Close();
            MainWindow = secretaryMainWindow;
        }

        public static void ShowPatientWindow(RegisteredPatientController controller)
        {
            if (!controller.IsBanned(App.User.PersonalId))
            {
                View.PatientPages.PatientViewBase pvB = new View.PatientPages.PatientViewBase(App.User.PersonalId);
                MainWindow.Close();
                pvB.Show();
                MainWindow = pvB;
            }
            else
            {
                MessageBox.Show("Previse puta ste izmenili pregled, rad ce privremeno biti onemogucen obratite se sekretaru", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ShowMedicalRecord(String patientId)
        {
            View.DoctorPages.Model.DoctorMedicalRecordViewModel viewModel = new DoctorPages.Model.DoctorMedicalRecordViewModel();
            viewModel.init(patientId);
            if (MainWindow.GetType().Name.Equals("DoctorBasePage"))
            {
                ((DoctorBasePage)MainWindow).ViewModel.SelectedVm = viewModel;
            }
        }
    }
}
