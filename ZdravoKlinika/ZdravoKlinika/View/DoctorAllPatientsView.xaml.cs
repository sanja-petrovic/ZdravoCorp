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

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for DoctorAllPatientsView.xaml
    /// </summary>
    public partial class DoctorAllPatientsView : UserControl
    {
        private AllPatientsViewModel viewModel;
        public DoctorAllPatientsView(Doctor doctor)
        {
            viewModel = new AllPatientsViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
