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
using ZdravoKlinika.View.DoctorPages.Model;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for MedView.xaml
    /// </summary>
    public partial class MedView : Window
    {

        private MedViewModel viewModel;
        public MedView(Doctor doctor, String id)
        {
            viewModel = new MedViewModel();
            viewModel.Doctor = doctor;
            viewModel.LoadMed(id);
            DataContext = viewModel;
            InitializeComponent();
            this.Title = viewModel.BrandName + " " + viewModel.Dosage + ", " + viewModel.Form;
        }
    }
}
