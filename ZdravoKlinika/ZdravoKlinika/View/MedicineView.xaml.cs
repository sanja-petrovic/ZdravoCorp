using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZdravoKlinika.View
{

    public partial class MedicineView : Window
    {
        private MedicationController medicationController;
        public ObservableCollection<Medication> Medications { get; set; }

        public MedicineView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicationController = new MedicationController();
            this.Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
            dataGridMedicine.ItemsSource = this.Medications;
        }

        private void dataGridMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerAddMedicineView m = new ManagerAddMedicineView();
            m.Show();
        }
    }
}
