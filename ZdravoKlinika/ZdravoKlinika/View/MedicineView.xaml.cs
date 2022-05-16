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
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View
{

    public partial class MedicineView : Window
    {
        private MedicationController medicationController;
        public ObservableCollection<Medication> Medications { get; set; }

        ObservableCollection<Medication> approvedMedication;
        ObservableCollection<Medication> unapprovedMedication;

        public MedicineView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicationController = new MedicationController();
            this.Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
            dataGridMedicine.ItemsSource = this.Medications;
            EditMedicineButton.IsEnabled = false;        
        }

        private void dataGridMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridMedicine.SelectedItem != null && filteriComboBox.SelectedIndex == 1)
            {
                EditMedicineButton.IsEnabled = true;
            } else
            {
                EditMedicineButton.IsEnabled = false;
            }
        }

        private void AddMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerAddMedicineView m = new ManagerAddMedicineView();
            m.Show();
        }

        private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerEditMedicineView m = new ManagerEditMedicineView((Medication)dataGridMedicine.SelectedItem);
            m.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            approvedMedication = new ObservableCollection<Medication>(this.medicationController.GetByApprovedValue(true));
            unapprovedMedication = new ObservableCollection<Medication>(this.medicationController.GetByApprovedValue(false));

            switch (filteriComboBox.SelectedIndex)
            {
                case 0: //odobreni
                    dataGridMedicine.ItemsSource = approvedMedication;
                    break;
                case 1: //neodobreni
                    dataGridMedicine.ItemsSource = unapprovedMedication;
                    break;
                default:
                    break;
            }
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            dataGridMedicine.ItemsSource = this.Medications;
            filteriComboBox.SelectedIndex = -1;
        }

    }
}
