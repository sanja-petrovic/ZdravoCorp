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
using System.Windows.Navigation;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private bool clicked = false;
        private bool show = false;
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            View.DoctorPages.DoctorSchedule doctorSchedule = new View.DoctorPages.DoctorSchedule();
            View.DoctorPages.DoctorBasePage doctorBasePage = new View.DoctorPages.DoctorBasePage();
            View.DoctorPages.DoctorMedicalRecord doctorMedicalRecord = new View.DoctorPages.DoctorMedicalRecord();
            View.DoctorPages.DoctorHomePage doctorHomePage = new View.DoctorPages.DoctorHomePage();
            MainFrame.Navigate(doctorSchedule);

        }

        public void TextBox_MouseDown(Object sender, RoutedEventArgs e)
        {
            if(!clicked)
            {
                UsernameTB.Clear();
                clicked = true;
                UsernameTB.Foreground = Brushes.Black;
            }
        }

        public void ShowPassword(Object sender, RoutedEventArgs e)
        {
            if(!show)
            {
                PasswordTB.FontFamily = new FontFamily("Open Sans");
                PasswordViewControl.Source = new BitmapImage(new Uri(@"/Resources/Images/hide.png", UriKind.RelativeOrAbsolute));
                show = true;
            } else
            {
                PasswordTB.FontFamily = new FontFamily("Password");
                PasswordViewControl.Source = new BitmapImage(new Uri(@"/Resources/Images/show.png", UriKind.RelativeOrAbsolute));
                show = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/40529684/how-to-store-login-info-of-a-wpf-application
            //https://stackoverflow.com/questions/33294471/proper-way-to-save-previous-user-login-info
        }
    }
}
