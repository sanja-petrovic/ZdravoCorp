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
            MainWindow = doctorBasePage;
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
