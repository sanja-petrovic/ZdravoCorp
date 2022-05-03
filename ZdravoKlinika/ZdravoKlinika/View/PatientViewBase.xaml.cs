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

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for PatientViewBase.xaml
    /// </summary>
    public partial class PatientViewBase : Window
    {
        private ViewModel.PatientViewModelBase viewModel = new ViewModel.PatientViewModelBase(); 
        public PatientViewBase()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            mainFrame.Navigate(viewModel.ProfilePage);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(viewModel.ProfilePage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(viewModel.PatientApointmentView);
        }
        public void refreshAppointmentView()
        {
            //TODO remove hardcoded patientID value
            viewModel.PatientApointmentView = new PatientAppointmentView("12345");
            mainFrame.Navigate(viewModel.PatientApointmentView);
        }
    }
}
