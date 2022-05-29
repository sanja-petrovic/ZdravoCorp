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
    /// <summary>
    /// Interaction logic for PrescriptionUserControl.xaml
    /// </summary>
    public partial class PrescriptionUserControl : UserControl
    {
        private PrescriptionViewModel viewModel;

        public PrescriptionUserControl()
        {
            this.viewModel = new PrescriptionViewModel();
            DataContext = this.viewModel;
            InitializeComponent();
        }

        public PrescriptionUserControl(PrescriptionViewModel model)
        {
            this.viewModel = model;
            DataContext = this.viewModel;
            InitializeComponent();
        }
    }
}
