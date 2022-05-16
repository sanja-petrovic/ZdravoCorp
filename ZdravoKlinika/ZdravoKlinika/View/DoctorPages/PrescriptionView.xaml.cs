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
using ZdravoKlinika.Controller;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for PrescriptionView.xaml
    /// </summary>
    public partial class PrescriptionView : Window
    {

        private string patientId;
        private string doctorId;

        private PrescriptionController prescriptionController;
        private TherapyTab viewModel;

        public PrescriptionView()
        {

            prescriptionController = new PrescriptionController();
            
        }

        public void init(string doctorId, string patientId)
        {
            ViewModel = new TherapyTab();
            ViewModel.LoadFromRecord(doctorId, patientId);
            DataContext = this;
            InitializeComponent();
        }

        public string PatientId { get => patientId; set => patientId = value; }
        public string DoctorId { get => doctorId; set => doctorId = value; }
        public TherapyTab ViewModel { get => viewModel; set => viewModel = value; }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            this.Close();
        }

    }
}
