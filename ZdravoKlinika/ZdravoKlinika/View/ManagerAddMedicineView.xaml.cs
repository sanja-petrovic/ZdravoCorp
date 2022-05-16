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
    /// <summary>
    /// Interaction logic for ManagerAddMedicineView.xaml
    /// </summary>
    public partial class ManagerAddMedicineView : Window
    {
        private MedicationController medicationController;
        public ObservableCollection<Medication> Medications { get; set; }

        public ManagerAddMedicineView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicationController = new MedicationController();
            this.Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
            alternativesListBox.ItemsSource = this.Medications;
            InitializeValues();
        }

        private void addMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            CreateMedication();
            CreateMedApprovalRequest();
        }

        private void InitializeActiveSubstancesListBox()
        {
            List<String> activeSubstances = new List<String>();
            String[] supstanceArray = {"antibiotik", "androgen", "anestetik", "antihistaminik", "antikoagulans", "antiperspirant", "antiparazitik", "antiseptik", "kaustik", "antivirotik", "citostatik", "hemostatik", "imunosupresiv", "humektans", "sebostatik", "vitamin"};
            for(int i = 0; i < supstanceArray.Length; i++)
            {
                activeSubstances.Add(supstanceArray[i]);
            }
            activeSubstancesListBox.ItemsSource = activeSubstances;
        }

        private void InitializeAllergensListBox()
        {
            List<String> allergens = new List<String>();
            String[] allergenArray = {"antiparazitik", "arachis ulje", "penicilin", "sulfonamid", "ibuprofen", "aspirin", "naproksen", "kukuruz", "psenica", "mleko", "krompir", "kokos", "zelatin"};
            for (int i = 0; i < allergenArray.Length; i++)
            {
                allergens.Add(allergenArray[i]);
            }
            allergensListBox.ItemsSource = allergens;
        }

        private void InitializeDoctorsComboBox()
        {           
            DoctorRepository doctorRepository = new DoctorRepository();
            List<Doctor> doctors = doctorRepository.GetAll();
            doctorsComboBox.ItemsSource = doctors;
        }

        private void InitializeValues()
        {
            InitializeActiveSubstancesListBox();
            InitializeAllergensListBox();
            InitializeDoctorsComboBox();
        }

        private void CreateMedication()
        {
            bool validated = false;
            string medicationCode = codeTextBox.Text;
            string brandName = nameTextBox.Text;
            string dosage = dosageTextBox.Text;
            List<string> activeSubstances = activeSubstancesListBox.SelectedItems.Cast<string>().ToList();
            string form = formTextBox.Text;
            string note = noteTextBox.Text;
            List<string> allergens = allergensListBox.SelectedItems.Cast<string>().ToList();
            List<Medication> alternatives = alternativesListBox.SelectedItems.Cast<Medication>().ToList();
            string classification = classificationTextBox.Text;
            string indications = indicationsTextBox.Text;
            string sideEffects = sideEffectsTextBox.Text;
            string dosageInstructions = instructionsTextBox.Text;
            int amount = Int32.Parse(amountTextBox.Text);
            this.medicationController.CreateMedication(medicationCode, brandName, dosage, activeSubstances, form, note, allergens, validated, alternatives, classification, indications, sideEffects, dosageInstructions, amount);
        }

        private void CreateMedApprovalRequest()
        {
            MedApprovalRequestController medApprovalRequestcontroller = new MedApprovalRequestController();
            Doctor d = (Doctor)doctorsComboBox.SelectedItem;
            Medication m = this.medicationController.GetByCodeAndName(codeTextBox.Text, nameTextBox.Text);
            medApprovalRequestcontroller.CreateRequest(d.PersonalId, m.MedicationId);
        }
    }
}
