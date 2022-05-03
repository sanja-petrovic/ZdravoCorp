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
    /// Interaction logic for DoctorManipView.xaml
    /// </summary>
    public partial class DoctorManipView : Window
    {
        private DoctorController controller = new DoctorController();
        //private List<Doctor> dataList;
        public DoctorManipView()
        {
            InitializeComponent();
            this.DataContext = this;
            genderComboBox.Items.Add(Gender.Male);
            genderComboBox.Items.Add(Gender.Female);
            genderComboBox1.Items.Add(Gender.Male);
            genderComboBox1.Items.Add(Gender.Female);
            genderComboBox.SelectedIndex = 0;
            genderComboBox1.SelectedIndex = 0;
            birthDatePicker.SelectedDate = DateTime.Now;
            birthDatePicker1.SelectedDate = DateTime.Now;
            doctorDataGrid.ItemsSource = controller.GetAll();

            
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            controller.CreateDoctor(
                    personalIdTextBox.Text,
                    firstNameTextBox.Text,
                    lastNameTextBox.Text,
                    (DateTime)birthDatePicker.SelectedDate,
                    (Gender)genderComboBox.SelectedItem,
                    phoneTextBox.Text,
                    emailTextBox.Text,
                    passwordTextBox.Text,
                    "null",
                    specialityTextBox.Text,
                    educationTextBox.Text
                );
            doctorDataGrid.ItemsSource = controller.GetAll();
        }

        private void doctorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor doc =  (Doctor)doctorDataGrid.SelectedItem;
            if (doc != null)
            {
                firstNameTextBox1.Text = doc.Name;
                lastNameTextBox1.Text = doc.Lastname;
                personalIdTextBox1.Text = doc.PersonalId;
                birthDatePicker1.SelectedDate = doc.DateOfBirth;
                genderComboBox1.SelectedItem = doc.Gender;
                phoneTextBox1.Text = doc.Phone;
                emailTextBox1.Text = doc.Email;
                passwordTextBox1.Text = doc.Password;
                specialityTextBox1.Text = doc.Specialty;
                educationTextBox1.Text = doc.EducationLevel;

            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            controller.UpdateDoctor(
                    personalIdTextBox1.Text,
                    firstNameTextBox1.Text,
                    lastNameTextBox1.Text,
                    (DateTime)birthDatePicker1.SelectedDate,
                    (Gender)genderComboBox1.SelectedItem,
                    phoneTextBox1.Text,
                    emailTextBox1.Text,
                    passwordTextBox1.Text,
                    "null",
                    specialityTextBox1.Text,
                    educationTextBox1.Text
                );
            doctorDataGrid.ItemsSource = controller.GetAll();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            controller.DeleteDoctor(personalIdTextBox1.Text);
            doctorDataGrid.ItemsSource = controller.GetAll();
        }
    }
}
