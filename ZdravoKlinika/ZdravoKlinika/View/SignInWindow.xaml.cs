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
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private bool clicked = false;
        private bool show = false;

        RegisteredUserController registeredUserController;

        public SignInWindow()
        {
            registeredUserController = new RegisteredUserController();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser? tryUser = registeredUserController.GetUserByEmailAndPassword(UsernameTB.Text, PasswordTB.Text);
            if (tryUser != null) 
            {
                switch (tryUser.UserType) 
                {
                    case UserType.Patient:
                        View.PatientViewBase pvB = new View.PatientViewBase(tryUser.PersonalId);
                        pvB.Show();
                        break;
                    case UserType.Secretary:
                        Secretary.SecretaryMainWindow secretaryMainWindow = new Secretary.SecretaryMainWindow();
                        secretaryMainWindow.Show();
                        break;
                    case UserType.Doctor:
                        DoctorPages.DoctorBasePage doctorBase = new DoctorPages.DoctorBasePage(tryUser);
                        doctorBase.Show();
                        break;
                    case UserType.Manager:
                        UpravnikWindow upravnikWindow = new UpravnikWindow();
                        upravnikWindow.Show();
                        break;
                    default:
                        break;

                }

                this.Close();
            }
            

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
