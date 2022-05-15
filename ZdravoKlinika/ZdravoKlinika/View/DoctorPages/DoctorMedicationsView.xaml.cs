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
            viewModel = new DoctorMedicationsViewModel(doctor);
            DataContext = viewModel;
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
                    AuthorizeButton.Visibility = Visibility.Hidden;
                    AuthorizeShadow.Visibility = Visibility.Hidden;
                } else
                {

                    AuthorizeButton.Visibility = Visibility.Visible;
                    AuthorizeShadow.Visibility = Visibility.Visible;
                    SupplyRequestButton.Visibility = Visibility.Hidden;
                    SupplyShadow.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
