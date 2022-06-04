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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Util;
using ZdravoKlinika.ViewModel.SecretaryViewModel;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryCreateEmergencyAppointment.xaml
    /// </summary>
    public partial class SecretaryCreateEmergencyAppointment : Page
    {
        PatientController patientContoller;
        AppointmentController appointmentContoller;
        PatientViewModel patientViewModel;
        DoctorController doctorController;
        List<Appointment> apps;
        DateTime selectedDate = DateTime.Now.Date;

        public SecretaryCreateEmergencyAppointment(PatientViewModel viewModel)
        {
            InitializeComponent();
            patientViewModel = viewModel;
            patientContoller = new PatientController();
            doctorController = new DoctorController();
            LoadAppointments(patientViewModel.SelectedPatient.GetPatientId());

            IPatient pat = patientViewModel.SelectedPatient;
            if (pat.GetPatientType() == PatientType.Registered)
            {
                RegisteredPatient rpat;
                rpat = (RegisteredPatient)pat;
                LabelLastname.Content = rpat.Lastname;
                LabelName.Content = rpat.Name;
                LabelPID.Content = rpat.PersonalId;
                UpdateImage(rpat.ProfilePicture);
            }
            else if (pat.GetPatientType() == PatientType.Guest)
            {
                GuestPatient gpat;
                gpat = (GuestPatient)pat;
                LabelLastname.Content = gpat.Lastname;
                LabelName.Content = gpat.Name;
                LabelPID.Content = gpat.PersonalId;
                UpdateImage("/Resources/Images/IconFlagRs.png");
            }

            ComboBoxAddType.ItemsSource = null;
            ComboBoxAddType.ItemsSource = doctorController.GetAllSpecialties();

        }

        public PatientController PatientContoller { get => patientContoller; set => patientContoller = value; }
        public AppointmentController AppointmentContoller { get => appointmentContoller; set => appointmentContoller = value; }


        private void LoadAppointments(String id)
        {
            AppointmentContoller = new AppointmentController();
            AppointmentDataGrid.ItemsSource = null;
            apps = AppointmentContoller.GetAppointmentsByPatientId(id);
            AppointmentDataGrid.ItemsSource = apps;

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

        private void ComboBoxAddType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDoctorList();
        }

        private void TextBoxDurationAdd_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateDoctorList();
        }

        private void UpdateDoctorList() 
        {
            int holder;
            if (Int32.TryParse(TextBoxDurationAdd.Text, out holder) && ComboBoxAddType.SelectedIndex != -1)
            {
                try
                {
                    ComboBoxAddDoctor.ItemsSource = appointmentContoller.GetFreeDoctorsBySpecialityForNextHour(holder, ComboBoxAddType.SelectedValue.ToString());
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("1"))
                        this.NavigationService.Navigate(new SecretaryResolveAppointments(ComboBoxAddType.SelectedValue.ToString(),holder,true));
                    else
                        this.NavigationService.Navigate(new SecretaryResolveAppointments(ComboBoxAddType.SelectedValue.ToString(), holder, false));
                }
            }
            
        }

        private void ComboBoxAddDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int holder;
            if (Int32.TryParse(TextBoxDurationAdd.Text, out holder) && ComboBoxAddType.SelectedIndex != -1 && ComboBoxAddDoctor.SelectedIndex != -1)
            {
                string a = ((Doctor)ComboBoxAddDoctor.SelectedItem).PersonalId;
                Doctor doc = doctorController.GetById(a);

                List<String> b = new List<String>();

                DateTime currentDate;
                if (DateTime.Now.Minute < 15 && DateTime.Now.Minute > 0)
                    currentDate = DateTime.Now.ToLocalTime().AddMinutes(14);
                else if (DateTime.Now.Minute < 30)
                    currentDate = DateTime.Now.ToLocalTime().AddMinutes(29);
                else if (DateTime.Now.Minute < 45)
                    currentDate = DateTime.Now.ToLocalTime().AddMinutes(44);
                else
                    currentDate = DateTime.Now.ToLocalTime().AddHours(1);


                foreach (DateBlock block in appointmentContoller.GetDateBlocksForDoctorInNextHour(holder,doc))
                {
                    b.Add(block.Start.TimeOfDay.ToString());
                }

                b.Sort();

                ComboBoxAddTime.ItemsSource = b;
            }     
        }

        private void CreateAppointment(object sender, RoutedEventArgs e)
        {
            String[] a = ComboBoxAddTime.SelectedItem.ToString().Split(":");
            selectedDate = selectedDate.AddMinutes(Int32.Parse(a[1]));
            selectedDate = selectedDate.AddHours(Int32.Parse(a[0]));

            appointmentContoller.CreateAppointment( ((Doctor)ComboBoxAddDoctor.SelectedItem).PersonalId,this.patientViewModel.SelectedPatient.GetPatientId(),selectedDate,true,AppointmentType.Surgery,"314",Int32.Parse(TextBoxDurationAdd.Text));
            NavigationService.Navigate(new SecretaryCreateEmergencyAppointment(patientViewModel));
        }
    }
}
