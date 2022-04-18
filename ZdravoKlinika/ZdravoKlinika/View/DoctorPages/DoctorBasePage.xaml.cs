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

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorBasePage.xaml
    /// </summary>
    public partial class DoctorBasePage : UserControl
    {
        public DoctorBasePage()
        {
            Model.DoctorViewModel viewModel = new Model.DoctorViewModel();
            viewModel.Name = "Dr Gregory House";
            viewModel.Specialty = "Dijagnostičar";
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
