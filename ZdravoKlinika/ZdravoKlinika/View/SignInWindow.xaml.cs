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
using ZdravoKlinika.ViewModel;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private bool clicked = false;
        private bool show = false;
        RegisteredUser user;

        RegisteredUserController registeredUserController;
        

        public RegisteredUser User { get => user; set => user = value; }

        public SignInViewModel viewModel;

        public SignInWindow()
        {
            viewModel = new SignInViewModel();
            DataContext = viewModel;
            InitializeComponent();
            if (viewModel.SavedLogin())
            {
                this.LogIn();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsLoginSuccessful()) 
            {
                this.LogIn();
                if(StayLoggedIn.IsChecked == true)
                {
                    viewModel.RememberUser();
                }

                this.Close();
            }
        }

        public void LogIn()
        {
            switch (viewModel.User.UserType)
            {
                case UserType.Patient:
                    PatientViewSpawn(viewModel.User.PersonalId);
                    break;
                case UserType.Secretary:
                    Secretary.SecretaryMainWindow secretaryMainWindow = new Secretary.SecretaryMainWindow(viewModel.User);
                    secretaryMainWindow.Show();
                    break;
                case UserType.Doctor:
                    DoctorPages.DoctorBasePage doctorBase = new DoctorPages.DoctorBasePage(viewModel.User);
                    doctorBase.Show();
                    break;
                case UserType.Manager:
                    UpravnikWindow upravnikWindow = new UpravnikWindow();
                    upravnikWindow.Show();
                    break;
                default:
                    break;
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
        
        private void PatientViewSpawn(String patientId)
        {
            if (!viewModel.RegisteredPatientController.IsBanned(viewModel.RegisteredPatientController.GetById(patientId)))
            {
                View.PatientPages.PatientViewBase pvB = new View.PatientPages.PatientViewBase(viewModel.User.PersonalId);
                pvB.Show();
            }
            else
            {
                MessageBox.Show("Previse puta ste izmenili pregled, rad ce privremeno biti onemogucen obratite se sekretaru", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
