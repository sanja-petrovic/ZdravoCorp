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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        public SecretaryMainWindow()
        {
            InitializeComponent();
            MainContentFrame.Navigate(new SecretaryHomePage());
        }

        private void HambuergerMenuIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (HamburgerMenuFrame.Visibility == Visibility.Visible) 
            {
                HamburgerMenuFrame.Visibility = Visibility.Collapsed;
                return;
            }
            HamburgerMenuFrame.Visibility = Visibility.Visible;
        }

        private void HomeChangePage(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new SecretaryHomePage());
            MenuContentLabel.Content = "Pocetna";
            HamburgerMenuFrame.Visibility = Visibility.Collapsed;
        }

        private void AddPatientChangePage(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new SecretaryAddPatientPage());
            MenuContentLabel.Content = "Dodavanje pacijenta";
            HamburgerMenuFrame.Visibility = Visibility.Collapsed;
        }
    }
}
