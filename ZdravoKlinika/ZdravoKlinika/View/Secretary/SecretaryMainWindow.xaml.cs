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
            Select(BorderHomePage);
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
            Select(BorderHomePage);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryHomePage());
            MenuContentLabel.Content = "Pocetna";
            HamburgerMenuFrame.Visibility = Visibility.Collapsed;
        }

        private void AddPatientChangePage(object sender, RoutedEventArgs e)
        {
            Select(BorderAddPatient);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryAddPatientPage());
            MenuContentLabel.Content = "Dodavanje pacijenta";
            HamburgerMenuFrame.Visibility = Visibility.Collapsed;
        }

        private void UpdatePatientPage(object sender, RoutedEventArgs e)
        {
            Select(BorderUDPatient);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryChoosePatientUDPage());
            MenuContentLabel.Content = "Azuriranje i brisanje pacijenta";
            HamburgerMenuFrame.Visibility = Visibility.Collapsed;
        }

        private void Select(Border b)
        {
            // unselect every border first
            ChangeColorUnSelected(BorderAddPatient);
            ChangeColorUnSelected(BorderUDPatient);
            ChangeColorUnSelected(BorderHomePage);

            // selected border
            ChangeColorSelected(b);
        }

        private void ChangeColorSelected(Border b)
        {
            Color selectedColor = new Color();
            selectedColor.A = 255;
            selectedColor.R = 197;
            selectedColor.G = 217;
            selectedColor.B = 105;
            b.BorderBrush = new SolidColorBrush(selectedColor);
            b.BorderThickness = new Thickness(5, 0, 0, 0);
        }
        private void ChangeColorUnSelected(Border b)
        {
            b.BorderThickness = new Thickness(0, 0, 0, 0);
        }
    }
}
