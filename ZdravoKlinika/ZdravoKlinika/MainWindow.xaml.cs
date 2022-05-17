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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            View.SignInWindow signInWindow = new View.SignInWindow();
            signInWindow.Show();
        }

        private void secretaryButton_Click(object sender, RoutedEventArgs e)
        {
            //View.SecretaryWindowCP1 secretaryWindow = new View.SecretaryWindowCP1();
            View.Secretary.SecretaryMainWindow secretaryWindow = new View.Secretary.SecretaryMainWindow();
            secretaryWindow.Show();
        }

        private void patientButton_Click(object sender, RoutedEventArgs e)
        {
            /*View.PatientView patientView = new View.PatientView();
            patientView.Show();*/
            View.PatientPages.PatientViewBase pvB = new View.PatientPages.PatientViewBase("123");
            pvB.Show();
        }

        private void Doctor_Management_Click(object sender, RoutedEventArgs e)
        {
            View.DoctorManipView view = new View.DoctorManipView();
            view.Show();
        }

        private void Upravnik_Click(object sender, RoutedEventArgs e)
        {
            View.UpravnikWindow upravnikWindow = new View.UpravnikWindow();
            upravnikWindow.Show();
        }
    }
}