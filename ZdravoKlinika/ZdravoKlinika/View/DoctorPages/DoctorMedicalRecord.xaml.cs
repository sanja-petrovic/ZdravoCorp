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
    /// Interaction logic for DoctorMedicalRecord.xaml
    /// </summary>
    public partial class DoctorMedicalRecord : UserControl
    {
        Model.DoctorMedicalRecordViewModel viewModel;
        string patientId;
        public DoctorMedicalRecord()
        {
            InitializeComponent();
            this.Focus();
        }

        public void Init(string patientId)
        {
            viewModel = new Model.DoctorMedicalRecordViewModel();
            DataContext = viewModel;
            viewModel.init(patientId);
            InitializeComponent();
            this.Focus();
        }

        public string PatientId { get => patientId; set => patientId = value; }

        public void MedicationAdded()
        {
            viewModel.MedicationAdded();
        }


    }
}
