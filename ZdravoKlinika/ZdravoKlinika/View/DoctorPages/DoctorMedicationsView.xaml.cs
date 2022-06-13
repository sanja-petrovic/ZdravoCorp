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
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class DoctorMedicationsView : UserControl
    {
        private static DoctorMedicationsViewModel viewModel;

        public DoctorMedicationsViewModel ViewModel { get => viewModel; set => viewModel = value; }

        public DoctorMedicationsView()
        {
            ViewModel = new DoctorMedicationsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            this.Focus();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl && MedsTab.IsInitialized)
            {
                if(MedsTab.SelectedIndex == 0)
                {
                    SupplyRequestButton.Visibility = Visibility.Visible;
                    SupplyShadow.Visibility = Visibility.Visible;
                } else
                {
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
                MedView med = new MedView(m.Id);
                med.Show();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach(MedViewModel vm in ViewModel.ApprovedMeds)
            {
                vm.IsChecked = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (MedViewModel vm in ViewModel.ApprovedMeds)
            {
                vm.IsChecked = false; ;
            }
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var s = UnapprovedMeds.SelectedItem as MedViewModel;
            if(s != null)
            {

                ApproveMedView approveMedView = new ApproveMedView(s.RequestId);
                approveMedView.ShowDialog();
                this.ViewModel.LoadApproved();
                this.ViewModel.LoadPending();
                DataContext = null;
                DataContext = this.ViewModel;
            }
        }
    }
}
