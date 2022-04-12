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
            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();
        }

        private void secretaryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void patientButton_Click(object sender, RoutedEventArgs e)
        {
            PatientView patientView = new PatientView();
            patientView.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorManipView view = new DoctorManipView();
            view.Show();
        }

        private void patientText_TextChanged(object sender, TextChangedEventArgs e)
        {

        private void Upravnik_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow upravnikWindow = new UpravnikWindow();
            upravnikWindow.Show();
        }
    }
}