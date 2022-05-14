using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryUpdateDeletePatientPage.xaml
    /// </summary>
    public partial class SecretaryUpdateDeletePatientPage : Page
    {
        RegisteredPatientController registeredPatientController;
        public SecretaryUpdateDeletePatientPage(String pid)
        {
            InitializeComponent();
            registeredPatientController = new RegisteredPatientController();

            updateComponents(pid);
        }

        private void ChoosePictureFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                String[] splitData = openFileDialog.FileName.Split(System.IO.Path.DirectorySeparatorChar);
                String profilePic = System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 3] + System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 2] + System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 1];
                UpdateImage(profilePic);
            }
        }
        private void updateComponents(String pid)
        {
            RegisteredPatient pat = registeredPatientController.GetById(pid);
            TextBoxName.Text = pat.Name;
            TextBoxLastname.Text = pat.Lastname;
            TextBoxPID.Text = pat.PersonalId;
            DatePickerDateOfBirth.SelectedDate = pat.DateOfBirth;
            TextBoxPhone.Text = pat.Phone;
            TextBoxEmail.Text = pat.Email;
            TextBoxPassword.Text = pat.Password;
            ComboBoxGender.SelectedIndex = (int)pat.Gender;
            TextBoxOccupation.Text = pat.Occupation;
            TextBoxStreet.Text = pat.Address.Street;
            TextBoxStNumber.Text = pat.Address.Number;
            TextBoxCity.Text = pat.Address.City;
            TextBoxCountry.Text = pat.Address.Country;
            ComboBoxBloodType.SelectedIndex = (int)pat.BloodType;
            if (pat.BloodType == BloodType.Null)
                ComboBoxBloodType.IsEnabled = true;
            foreach (String aler in pat.MedicalRecord.Allergies)
                if (pat.MedicalRecord.Allergies.Last().Equals(aler))
                    TextBoxAllergies.Text += aler;
                else TextBoxAllergies.Text += aler + ", ";
            foreach (String diag in pat.MedicalRecord.Diagnoses)
                if (pat.MedicalRecord.Diagnoses.Last().Equals(diag))
                    TextBoxDiagnosis.Text += diag;
                else TextBoxDiagnosis.Text += diag + ", ";

            TextBoxECName.Text = pat.EmergencyContactName;
            TextBoxECPhone.Text = pat.EmergencyContactPhone;

            UpdateImage(pat.ProfilePicture);
        }
        private void UpdateImage(String path)
        {
            BitmapImage bitim = new BitmapImage();
            try
            {
                bitim.BeginInit();
                Uri uripath = new Uri(string.Concat(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, path));
                bitim.UriSource = new Uri(uripath.ToString());
                bitim.DecodePixelHeight = 140;
                bitim.DecodePixelWidth = 140;
                bitim.EndInit();
                ProfilePicImage.Source = bitim;
            }
            catch (FileNotFoundException) 
            {
            }    
        }
        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            String name = TextBoxName.Text;
            String lastname = TextBoxLastname.Text;
            String personalID = TextBoxPID.Text;
            String phone = TextBoxPhone.Text;
            String password = TextBoxPassword.Text;
            String occupation = TextBoxOccupation.Text;
            String street = TextBoxStreet.Text;
            String stnumber = TextBoxStNumber.Text;
            String city = TextBoxCity.Text;
            String country = TextBoxCountry.Text;
            BloodType bloodType;
            if (ComboBoxBloodType.SelectedIndex == -1)
                bloodType = BloodType.Null;
            else
                bloodType = (BloodType)ComboBoxBloodType.SelectedIndex;
            List<String> allergies = new List<String>();
            foreach (String allergy in TextBoxAllergies.Text.Split(","))
                allergies.Add(allergy.Trim());
            List<String> diagnosis = new List<String>();
            foreach (String diagnose in TextBoxDiagnosis.Text.Split(","))
                diagnosis.Add(diagnose.Trim());

            String ECname = TextBoxECName.Text;
            String ECphone = TextBoxECPhone.Text;

            String[] splitData = ProfilePicImage.Source.ToString().Split("/");
            String profilePic = System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 3] + System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 2] + System.IO.Path.DirectorySeparatorChar + splitData[splitData.Length - 1];

            registeredPatientController.UpdatePatient(personalID, name, lastname, phone, password, profilePic, street, stnumber, city, country, bloodType, occupation, ECname, ECphone, allergies, diagnosis);
            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new SecretaryChoosePatientUDPage());
        }
        private void DeletePatient(object sender, RoutedEventArgs e)
        { 
            String personalID = TextBoxPID.Text;
            registeredPatientController.DeletePatient(personalID);
            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new SecretaryChoosePatientUDPage());
        }
    }
}
