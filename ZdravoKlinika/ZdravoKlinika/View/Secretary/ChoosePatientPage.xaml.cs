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
using ZdravoKlinika.ViewModel.SecretaryViewModel;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for ChoosePatientPage.xaml
    /// </summary>
    public partial class ChoosePatientPage : Page
    {
        PatientViewModel viewModel;
        Object caller;
        public ChoosePatientPage(PatientViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            this.caller = caller;
            if (viewModel.SelectedPatient != null) 
            {
                TextBoxName.Text = viewModel.SelectedPatient.GetName();
                TextBoxLastname.Text = viewModel.SelectedPatient.GetLastname();
                TextBoxPID.Text = viewModel.SelectedPatient.GetPatientId();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int p = viewModel.ChoosePatient(TextBoxPID.Text, TextBoxName.Text, TextBoxLastname.Text);
            if (p == 2)
            { 
                InformationFocus.Visibility = Visibility.Visible;
                CreateGuest.Visibility = Visibility.Visible;
            }
            if (p == 1)
            {
                InformationText.Text = "Pogresno ime ili prezime za pacijenta sa izabranim JMBG!";
                InformationFocus.Visibility = Visibility.Visible;
                InformationWindow.Visibility = Visibility.Visible;
            }
            else
            {
                InformationText.Text = "Uspesno ste izabrali pacijenta!";
                InformationFocus.Visibility = Visibility.Visible;
                InformationWindow.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InformationFocus.Visibility = Visibility.Collapsed;
            InformationWindow.Visibility = Visibility.Collapsed;
            CreateGuest.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            viewModel.CreateGuest(TextBoxPID.Text, TextBoxName.Text, TextBoxLastname.Text);
            InformationText.Text = "Uspesno ste kreirali gost pacijenta!";
            InformationFocus.Visibility = Visibility.Visible;
            InformationWindow.Visibility = Visibility.Visible;
        }
    }
}
