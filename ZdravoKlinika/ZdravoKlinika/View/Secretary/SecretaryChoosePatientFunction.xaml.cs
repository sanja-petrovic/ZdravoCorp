#nullable disable
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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryChoosePatientFunction.xaml
    /// </summary>
    public partial class SecretaryChoosePatientFunction : PageFunction<String>
    {
        PatientController patientController;
        public SecretaryChoosePatientFunction(String id, String name, String lastname)
        {
            InitializeComponent();
            TextBoxPID.Text = id;
            TextBoxName.Text = name;
            TextBoxLastname.Text = lastname;
            PatientController = new PatientController();
        }

        public PatientController PatientController { get => patientController; set => patientController = value; }

        private void CheckForPatient(object sender, RoutedEventArgs e)
        {
            Patient pat = PatientController.GetById(TextBoxPID.Text);
            if (pat != null)
            {
                if (pat.GetName().Equals(TextBoxName.Text) && pat.GetLastname().Equals(TextBoxLastname.Text))
                {
                    OnReturn(new ReturnEventArgs<string>(TextBoxPID.Text));
                }
            }
            else 
            {
                PatientController.CreateNewGuestPatient(TextBoxPID.Text, TextBoxName.Text, TextBoxLastname.Text);
            }
        }
    }
}
