using Microsoft.Win32;
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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryAddPatientPage.xaml
    /// </summary>
    public partial class SecretaryAddPatientPage : Page
    {
        RegisteredPatientController registeredPatientController;
        public SecretaryAddPatientPage()
        {
            InitializeComponent();
            registeredPatientController = new RegisteredPatientController();
        }

        private void CreateNewPatient_ButtonClick(object sender, RoutedEventArgs e)
        {
            String name = TextBoxName.Text;
            String lastname = TextBoxLastname.Text;
            String personalID = TextBoxPID.Text;
            DateTime dateOfBirth = (DateTime)DatePickerDateOfBirth.SelectedDate;
            String phone = TextBoxPhone.Text;
            String email = TextBoxEmail.Text;
            String password = TextBoxPassword.Text;
            Gender gender = (Gender)ComboBoxGender.SelectedIndex;
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
            allergies.Add(TextBoxAllergies.Text);
            List<String> diagnosis = new List<String>();
            diagnosis.Add(TextBoxDiagnosis.Text);

            String ECname = TextBoxECName.Text;
            String ECphone = TextBoxECPhone.Text;

            String profilePic = ProfilePicImage.Source.ToString();

            registeredPatientController.CreatePatient(personalID, name, lastname, dateOfBirth, gender, phone, email, password, profilePic, street, stnumber, city, country, bloodType, occupation, ECname, ECphone, allergies, diagnosis);
            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new SecretaryAddPatientPage());
        }

        private void ChoosePictureFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitim = new BitmapImage();
                bitim.BeginInit();
                Uri uripath = new Uri(openFileDialog.FileName);
                bitim.UriSource = new Uri(uripath.AbsoluteUri);
                bitim.DecodePixelHeight = 140;
                bitim.DecodePixelWidth = 140;
                bitim.EndInit();
                ProfilePicImage.Source = bitim;
            }
        }
    }
}
