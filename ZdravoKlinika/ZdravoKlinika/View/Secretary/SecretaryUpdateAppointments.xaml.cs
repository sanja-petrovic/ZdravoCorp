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
    /// Interaction logic for SecretaryUpdateAppointments.xaml
    /// </summary>
    public partial class SecretaryUpdateAppointments : Page
    {
        PatientController patientContoller;
        AppointmentController appointmentContoller;
        PatientViewModel patientViewModel;
        DoctorController doctorController;
        List<Appointment> apps;
        public SecretaryUpdateAppointments(PatientViewModel viewModel)
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

        private void AppointmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentDataGrid.SelectedIndex != -1)
            {
                Appointment selected = (Appointment)AppointmentDataGrid.SelectedItem;
                if (selected != null)
                {
                    SelectedDateUpdate.SelectedDate = selected.DateAndTime.Date;

                    ComboBoxDoctorUpdate.ItemsSource = null;
                    ComboBoxTimeUpdate.ItemsSource = null;
                    List<String> a = new List<String>();
                    List<String> b = new List<String>();


                    foreach (Doctor doc in doctorController.GetAll())
                    {
                        a.Add(doc.NameAndLast);
                    }

                    foreach (DateBlock block in AppointmentContoller.GetFreeTimeForPatient(((DateTime)SelectedDateUpdate.SelectedDate).Date, 30, selected.Patient, 8, 20))
                    {
                        b.Add(block.Start.TimeOfDay.ToString());
                    }

                    b.Add(selected.DateAndTime.TimeOfDay.ToString());

                    b.Sort();

                    //SelectedDateUpdate.SelectedDate = selected.DateAndTime;

                    ComboBoxDoctorUpdate.ItemsSource = a;
                    ComboBoxTimeUpdate.ItemsSource = b;

                    ComboBoxDoctorUpdate.SelectedItem = selected.Doctor.NameAndLast;
                    ComboBoxTimeUpdate.SelectedItem = selected.DateAndTime.TimeOfDay.ToString();
                }
            }

        }
        private void SelectedDateUpdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentDataGrid.SelectedIndex != -1)
            {
                Appointment selected = (Appointment)AppointmentDataGrid.SelectedItem;
                if (selected != null)
                {

                    ComboBoxDoctorUpdate.ItemsSource = null;
                    ComboBoxTimeUpdate.ItemsSource = null;
                    List<String> a = new List<String>();
                    List<String> b = new List<String>();

                    foreach (Doctor doc in doctorController.GetAll())
                    {
                        a.Add(doc.NameAndLast);
                    }

                    foreach (DateBlock block in AppointmentContoller.GetFreeTimeForPatient(((DateTime)SelectedDateUpdate.SelectedDate).Date, selected.Duration, selected.Patient, 8, 20))
                    {
                        b.Add(block.Start.TimeOfDay.ToString());
                    }

                    b.Add(selected.DateAndTime.TimeOfDay.ToString());

                    b.Sort();

                    ComboBoxDoctorUpdate.ItemsSource = a;
                    ComboBoxTimeUpdate.ItemsSource = b;

                    ComboBoxDoctorUpdate.SelectedItem = selected.Doctor.NameAndLast;
                    ComboBoxTimeUpdate.SelectedItem = selected.DateAndTime.TimeOfDay.ToString();
                }
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            Appointment selected = (Appointment)AppointmentDataGrid.SelectedItem;
            if (selected != null)
            {
                AppointmentContoller.DeleteAppointment(selected.AppointmentId);
                LoadAppointments(selected.Patient.GetPatientId());
            }

        }

        private void Izmena(object sender, RoutedEventArgs e)
        {
            Appointment selected = (Appointment)AppointmentDataGrid.SelectedItem;

            if (SelectedDateUpdate.SelectedDate != null && ComboBoxDoctorUpdate.SelectedItem != null && ComboBoxTimeUpdate.SelectedItem != null && selected != null)
            {
                DateTime date = (DateTime)SelectedDateUpdate.SelectedDate;
                String[] a = ComboBoxTimeUpdate.SelectedItem.ToString().Split(":");
                date = date.AddMinutes(Int32.Parse(a[1]));
                date = date.AddHours(Int32.Parse(a[0]));

                String[] ab = ComboBoxDoctorUpdate.SelectedItem.ToString().Split(" ");
                DateTime date2 = (DateTime.Now).AddDays(2);
                if (date <= date2)
                    return;

                AppointmentContoller.EditAppointment(selected.AppointmentId, ab[2], selected.Patient.GetPatientId(), date, selected.Emergency, selected.Type, selected.Room.RoomId, selected.Duration);
                LoadAppointments(LabelPID.Content.ToString());
            }

        }
    }
}
