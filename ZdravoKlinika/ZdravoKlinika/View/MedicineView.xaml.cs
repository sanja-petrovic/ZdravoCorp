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
        private MedApprovalRequestController medApprovalRequestController;
        public ObservableCollection<Medication> Medications { get; set; }

        ObservableCollection<Medication> approvedMedication;
        ObservableCollection<MedApprovalRequest> deniedRequests;
        ObservableCollection<MedApprovalRequest> pendingRequests;
        ObservableCollection<Medication> unapprovedMedication;
        ObservableCollection<Medication> pendingMedication;


        public MedicineView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicationController = new MedicationController();
            this.medApprovalRequestController = new MedApprovalRequestController();
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
            deniedRequests = new ObservableCollection<MedApprovalRequest>(this.medApprovalRequestController.GetDeniedRequests());
            pendingRequests = new ObservableCollection<MedApprovalRequest>(this.medApprovalRequestController.GetPendingRequests());
            unapprovedMedication = new ObservableCollection<Medication>();
            pendingMedication = new ObservableCollection<Medication>();

            switch (filteriComboBox.SelectedIndex)
            {
                case 0: //odobreni
                    dataGridMedicine.ItemsSource = approvedMedication;
                    break;
                case 1: //neodobreni
                    foreach (MedApprovalRequest mar in this.deniedRequests)
                    {
                        unapprovedMedication.Add(mar.Medication);
                    }
                    dataGridMedicine.ItemsSource = unapprovedMedication;
                    break;
                case 2: //cekaju odobrenje
                    foreach (MedApprovalRequest mar in pendingRequests)
                    {
                        pendingMedication.Add(mar.Medication);
                    }
                    dataGridMedicine.ItemsSource = pendingMedication;
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
