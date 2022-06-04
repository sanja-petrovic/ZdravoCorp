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

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for AddDiagnosisView.xaml
    /// </summary>
    public partial class AddDiagnosisView : Window
    {
        private Model.DiagnosisViewModel viewModel;
        public AddDiagnosisView(String medicalRecordId)
        {
            viewModel = new Model.DiagnosisViewModel(medicalRecordId);
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
