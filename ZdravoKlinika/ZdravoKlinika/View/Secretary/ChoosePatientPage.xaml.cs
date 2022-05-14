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
            viewModel.ChoosePatient(TextBoxPID.Text, TextBoxName.Text, TextBoxLastname.Text);
        }
    }
}
