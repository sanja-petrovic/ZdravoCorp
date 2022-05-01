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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryChoosePatientUDPage.xaml
    /// </summary>
    public partial class SecretaryChoosePatientUDPage : Page
    {
        RegisteredPatientController registeredPatientController;
        public SecretaryChoosePatientUDPage()
        {
            InitializeComponent();
            registeredPatientController = new RegisteredPatientController();
        }

        private void CheckForPatient(object sender, RoutedEventArgs e)
        {
            RegisteredPatient pat = registeredPatientController.GetById(TextBoxPID.Text);
            if (pat != null)
                if (pat.Name.Equals(TextBoxName.Text) && pat.Lastname.Equals(TextBoxLastname.Text))
                    NavigationService.RemoveBackEntry();
                    NavigationService.Navigate(new SecretaryUpdateDeletePatientPage(TextBoxPID.Text));
        }
    }
}
