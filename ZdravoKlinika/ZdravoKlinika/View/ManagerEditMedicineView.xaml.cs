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
    /// <summary>
    /// Interaction logic for ManagerEditMedicineView.xaml
    /// </summary>
    public partial class ManagerEditMedicineView : Window
    {
        private MedicationController medicationController;
        private MedApprovalRequestController medApprovalRequestController;
        private Medication med;
        public ObservableCollection<Medication> Medications { get; set; }
        public ObservableCollection<MedApprovalRequest> DeniedApprovalRequests { get; set; }

        public ManagerEditMedicineView(Medication m)
        {
            InitializeComponent();
            this.DataContext = this;
            this.med = m;
            this.medicationController = new MedicationController();
            this.medApprovalRequestController = new MedApprovalRequestController();
            this.Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
            this.DeniedApprovalRequests = new ObservableCollection<MedApprovalRequest>(this.medApprovalRequestController.GetDeniedRequests());
            alternativesListBox.ItemsSource = this.Medications;
            InitializeValues();
        }

        private void InitializeActiveSubstancesListBox()
        {
            List<String> activeSubstances = new List<String>();
            String[] supstanceArray = { "antibiotik", "androgen", "anestetik", "antihistaminik", "antikoagulans", "antiperspirant", "antiparazitik", "antiseptik", "kaustik", "antivirotik", "citostatik", "hemostatik", "imunosupresiv", "humektans", "sebostatik", "vitamin" };
            for (int i = 0; i < supstanceArray.Length; i++)
            {
                activeSubstances.Add(supstanceArray[i]);
            }
            activeSubstancesListBox.ItemsSource = activeSubstances;
        }

        private void InitializeAllergensListBox()
        {
            List<String> allergens = new List<String>();
            String[] allergenArray = { "antiparazitik", "arachis ulje", "penicilin", "sulfonamid", "ibuprofen", "aspirin", "naproksen", "kukuruz", "psenica", "mleko", "krompir", "kokos", "zelatin" };
            for (int i = 0; i < allergenArray.Length; i++)
            {
                allergens.Add(allergenArray[i]);
            }
            allergensListBox.ItemsSource = allergens;
        }

        private void InitializeDoctorsComboBox()
        {
            DoctorController doctorController = new DoctorController();
            List<Doctor> doctors = doctorController.GetAll();

            //PRONADJI I IZBACI PROSLOG/PROSLE DOKTORE
            foreach(MedApprovalRequest mar in DeniedApprovalRequests)
            {
                if (mar.Medication.MedicationId.Equals(this.med.MedicationId))
                {

                    doctors.RemoveAll(d => d.PersonalId.Equals(mar.Reviewer.PersonalId));
                }
            }

            doctorsComboBox.ItemsSource = doctors;
        }

        private void InitializeFields()
        {
            foreach (MedApprovalRequest mar in DeniedApprovalRequests)
            {
                if (mar.Medication.MedicationId.Equals(this.med.MedicationId))
                {
                    commentLabel.Content = mar.Comment;
                    codeTextBox.Text = mar.Medication.MedicationCode;
                    nameTextBox.Text = mar.Medication.BrandName;
                    dosageTextBox.Text = mar.Medication.Dosage;
                    formTextBox.Text = mar.Medication.Form;
                    classificationTextBox.Text = mar.Medication.Classification;
                    amountTextBox.Text = mar.Medication.Amount.ToString();
                    indicationsTextBox.Text = mar.Medication.Indications;
                    sideEffectsTextBox.Text = mar.Medication.SideEffects;
                    instructionsTextBox.Text = mar.Medication.DosageInstructions;
                    noteTextBox.Text = mar.Medication.Note;
                }
            }           
        }

        private void InitializeValues()
        {
            InitializeActiveSubstancesListBox();
            InitializeAllergensListBox();
            InitializeDoctorsComboBox();
            InitializeFields();
        }

        private void editMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            /*this.medicationController.DeleteMedication(med.MedicationId);
            CreateMedication();
            CreateMedApprovalRequest();*/
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
