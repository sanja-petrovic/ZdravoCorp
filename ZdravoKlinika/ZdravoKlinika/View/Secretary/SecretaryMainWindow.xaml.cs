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
using ZdravoKlinika.ViewModel.SecretaryViewModel;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        PatientViewModel PatientViewModel { get; set; }
        public SecretaryMainWindow()
        {
            InitializeComponent();
            MainContentFrame.Navigate(new SecretaryHomePage());
            PatientViewModel = new PatientViewModel();
            Select(BorderHomePage);
            Application.Current.MainWindow = this;
        }

        private void HambuergerMenuIcon_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (HamburgerMenuFrame.Visibility == Visibility.Collapsed)
                HamburgerMenuFrame.Visibility = Visibility.Visible;

            if (HamburgerMenuFrame.IsChecked == false)
            {
                HamburgerMenuFrame.IsChecked = true;
                return;
            }
            HamburgerMenuFrame.IsChecked = false;
        }

        private void HomeChangePage(object sender, RoutedEventArgs e)
        {
            ChangeToHomePage();
        }

        private void ChangeToHomePage()
        {
            Select(BorderHomePage);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryHomePage());
            MenuContentLabel.Content = "Pocetna";
            HamburgerMenuFrame.IsChecked = false;
        }

        private void AddPatientChangePage(object sender, RoutedEventArgs e)
        {
            Select(BorderAddPatient);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryAddPatientPage());
            MenuContentLabel.Content = "Dodavanje pacijenta";
            HamburgerMenuFrame.IsChecked = false;
        }

        private void UpdatePatientPage(object sender, RoutedEventArgs e)
        {
            if (PatientViewModel.SelectedPatient == null)
            {
                ChoosePatient();
                return;
            }
            Select(BorderUDPatient);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryUpdateDeletePatientPage(PatientViewModel.SelectedPatient.GetPatientId()));
            MenuContentLabel.Content = "Azuriranje i brisanje pacijenta";
            HamburgerMenuFrame.IsChecked = false;
        }
        private void CreateAppointmentPage(object sender, RoutedEventArgs e)
        {
            if (PatientViewModel.SelectedPatient == null)
            {
                ChoosePatient();
                return;
            }
            Select(BorderCrAppointment);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryCreateAppointment(PatientViewModel));
            MenuContentLabel.Content = "Kreiranje termina";
            HamburgerMenuFrame.IsChecked = false;
        }
        private void OrderEquipmentPage(object sender, RoutedEventArgs e)
        {
            Select(BorderOrderEquipment);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryOrderEquipment());
            MenuContentLabel.Content = "Narucivanje opreme";
            HamburgerMenuFrame.IsChecked = false;
        }
        private void Select(Border b)
        {
            // unselect every border first
            ChangeColorUnSelected(BorderAddPatient);
            ChangeColorUnSelected(BorderUDPatient);
            ChangeColorUnSelected(BorderHomePage);
            ChangeColorUnSelected(BorderCrAppointment);
            ChangeColorUnSelected(BorderOrderEquipment);
            ChangeColorUnSelected(BorderChoosePatient);
            ChangeColorUnSelected(BorderOrderEquipment);
            ChangeColorUnSelected(BorderUpdateAppointment);

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

        private void CheckPatient(object sender, RoutedEventArgs e)
        {
            ChoosePatient();
        }

        private void ChoosePatient()
        {
            Select(BorderChoosePatient);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MenuContentLabel.Content = "Biranje pacijenta";
            HamburgerMenuFrame.IsChecked = false;
            MainContentFrame.Navigate(new ChoosePatientPage(PatientViewModel));
        }

        private void UpdateAppointmentPage(object sender, RoutedEventArgs e)
        {
            if (PatientViewModel.SelectedPatient == null)
            {
                ChoosePatient();
                return;
            }
            Select(BorderUpdateAppointment);
            if (MainContentFrame.CanGoBack)
                MainContentFrame.RemoveBackEntry();
            MainContentFrame.Navigate(new SecretaryUpdateAppointments(PatientViewModel));
            MenuContentLabel.Content = "Azuriranje termina";
            HamburgerMenuFrame.IsChecked = false;
        }

        public void ToHomePage() 
        {
            ChangeToHomePage();
        }
    }
}
