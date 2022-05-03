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

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindowCP1 : Window
    {
        private RegisteredPatientController PatientController;
        public SecretaryWindowCP1()
        {
            InitializeComponent();
            PatientController = new RegisteredPatientController();
            ReadAll();
        }

        private void ReadPatients(object sender, RoutedEventArgs e)
        {
            ReadAll();
        }

        private void CreatePatient(object sender, RoutedEventArgs e)
        {
            List<String> alergies = new List<String>();
            alergies.Add(TextBoxAlergies.Text);
            List<String> diag = new List<String>();
            diag.Add(TextBoxDiagnosis.Text);
            // PatientController.CreatePatient(TextBoxID.Text, TextBoxName.Text, TextBoxLastName.Text, (DateTime)picker.SelectedDate, (Gender)int.Parse(TextBoxGender.Text), TextBoxPhone.Text,
            //TextBoxEmail.Text, TextBoxPassword.Text, TextBoxProfilepic.Text, new Address(TextBoxAddress.Text), TextBoxParrentName.Text, (BloodType)int.Parse(TextBoxBlood.Text),
             //   TextBoxOccupation.Text, TextBoxECName.Text, TextBoxECPhone.Text, alergies, diag);
            ReadAll();

        }

        // Delete
        private void Delete(object sender, RoutedEventArgs e)
        {
            PatientController.DeletePatient(TextBoxID.Text);
            ReadAll();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            //PatientController.UpdatePatient(TextBoxID.Text, TextBoxName.Text, TextBoxLastName.Text, TextBoxPhone.Text, TextBoxEmail.Text, TextBoxPassword.Text, TextBoxProfilepic.Text, TextBoxParrentName.Text, TextBoxOccupation.Text, TextBoxECName.Text, TextBoxECPhone.Text);
            ReadAll();
        }

        private void ReadAll()
        {
            List<RegisteredPatient> patients = PatientController.GetAll();
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = patients;
        }

    }
}
