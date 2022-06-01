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
using ZdravoKlinika.View.Navigation;

namespace ZdravoKlinika.View
{
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
            if(Navigator.MainWindow == null)
                Navigator.MainWindow = this;
            viewModel = new SignInViewModel();
            DataContext = viewModel;
            InitializeComponent();
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
        
    }
}
