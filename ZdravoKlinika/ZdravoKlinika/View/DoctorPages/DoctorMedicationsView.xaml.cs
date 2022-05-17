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
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorMedicationsView.xaml
    /// </summary>
    public partial class DoctorMedicationsView : UserControl
    {
        DoctorMedicationsViewModel viewModel;
        public DoctorMedicationsView(Doctor doctor)
        {
            this.viewModel = new DoctorMedicationsViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if(MedsTab.SelectedIndex == 0)
                {
                    SupplyRequestButton.Visibility = Visibility.Visible;
                    SupplyShadow.Visibility = Visibility.Visible;
                    //AuthorizeButton.Visibility = Visibility.Hidden;
                    //AuthorizeShadow.Visibility = Visibility.Hidden;
                } else
                {

                    //AuthorizeButton.Visibility = Visibility.Visible;
                    //AuthorizeShadow.Visibility = Visibility.Visible;
                    SupplyRequestButton.Visibility = Visibility.Hidden;
                    SupplyShadow.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ApprovedMeds_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var m = ApprovedMeds.SelectedItem as MedViewModel;
            
            if(m != null)
            {
                MedView med = new MedView(this.viewModel.Doctor, m.Id);
                med.Show();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach(MedViewModel vm in viewModel.ApprovedMeds)
            {
                vm.IsChecked = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (MedViewModel vm in viewModel.ApprovedMeds)
            {
                vm.IsChecked = false; ;
            }
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var s = UnapprovedMeds.SelectedItem as MedViewModel;
            if(s != null)
            {

                ApproveMedView approveMedView = new ApproveMedView(this.viewModel.Doctor, s.RequestId);
                approveMedView.ShowDialog();
                this.viewModel.LoadApproved();
                this.viewModel.LoadPending();
                DataContext = null;
                DataContext = this.viewModel;
            }
        }
    }
}
