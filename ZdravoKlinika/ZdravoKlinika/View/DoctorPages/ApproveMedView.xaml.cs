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
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for ApproveMedView.xaml
    /// </summary>
    public partial class ApproveMedView : Window
    {
        private MedViewModel viewModel;
        public ApproveMedView(int requestId)
        {
            viewModel = new MedViewModel();
            //viewModel.Doctor = Controller.RegisteredUserController.UserToDoctor(App.User);
            viewModel.LoadRequest(requestId);
            DataContext = viewModel;
            InitializeComponent();
            this.Title = viewModel.BrandName + " " + viewModel.Dosage + ", " + viewModel.Form;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxGrid.IsEnabled = true;
            ConfirmButton.Visibility = Visibility.Collapsed;
            OkButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            GiveUpButton.Visibility = Visibility.Visible;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.ApproveRequest();
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DenyRequest();
            this.Close();
        }

        private void GiveUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
