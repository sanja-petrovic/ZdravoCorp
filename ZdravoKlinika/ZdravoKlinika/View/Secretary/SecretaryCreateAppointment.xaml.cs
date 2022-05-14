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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryCreateAppointment.xaml
    /// </summary>
    public partial class SecretaryCreateAppointment : Page
    {
        PatientController patientContoller;
        AppointmentController appointmentContoller;
        DoctorController doctorController;
        List<Appointment> apps;
        public SecretaryCreateAppointment()
        {
            InitializeComponent();
            doctorController = new DoctorController();
        }

        public PatientController PatientContoller { get => patientContoller; set => patientContoller = value; }
        public AppointmentController AppointmentContoller { get => appointmentContoller; set => appointmentContoller = value; }

        private void CheckPatient(object sender, RoutedEventArgs e)
        {
            SecretaryChoosePatientFunction pagueFunctuon = new SecretaryChoosePatientFunction(LabelPID.Content.ToString(), LabelName.Content.ToString(), LabelLastname.Content.ToString());
            pagueFunctuon.Return += PageFunctuinReturn;
            this.NavigationService.Navigate(pagueFunctuon);
        }

        private void PageFunctuinReturn(object sender, ReturnEventArgs<string> e)
        {
            PatientContoller = new PatientController();
            ChoosePatientPanel.Visibility = Visibility.Collapsed;
            Patient pat = PatientContoller.GetById(e.Result);
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

            LoadAppointments(pat.GetPatientId());

        }
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

                    foreach (DateBlock block in AppointmentContoller.getFreeTimeForPatient(((DateTime)SelectedDateUpdate.SelectedDate).Date, 30, selected.Patient, 8, 20))
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

        private void ToggleButtonUpdate_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void updateLists() 
        {

            ComboBoxAddTime.ItemsSource = null;
            ComboBoxAddDoctor.ItemsSource = null;
            List<String> a = new List<String>();
            List<String> b = new List<String>();


            foreach (Doctor doc in doctorController.GetAll())
            {
                a.Add(doc.NameAndLast);
            }

            foreach (DateBlock block in AppointmentContoller.getFreeTimeForPatient(((DateTime)DatePickerAdd.SelectedDate).Date, Int32.Parse(TextBoxDurationAdd.Text), patientContoller.GetById(LabelPID.Content.ToString()), 8, 20))
            {
                b.Add(block.Start.TimeOfDay.ToString());
            }

            b.Sort();

            //SelectedDateUpdate.SelectedDate = selected.DateAndTime;

            ComboBoxAddDoctor.ItemsSource = a;
            ComboBoxAddTime.ItemsSource = b;
            
            
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

                    foreach (DateBlock block in AppointmentContoller.getFreeTimeForPatient(((DateTime)SelectedDateUpdate.SelectedDate).Date, 30, selected.Patient, 8, 20))
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

        private void Dodavanje(object sender, RoutedEventArgs e)
        {

            DateTime date = (DateTime)DatePickerAdd.SelectedDate;
            String[] a = ComboBoxAddTime.SelectedItem.ToString().Split(":");
            date = date.AddMinutes(Int32.Parse(a[1]));
            date = date.AddHours(Int32.Parse(a[0]));

            String[] ab = ComboBoxAddDoctor.SelectedItem.ToString().Split(" ");
            DateTime date2 = (DateTime.Now).AddDays(2);
            if (date <= date2)
                return;

            int duration = Int32.Parse(TextBoxDurationAdd.Text);

            AppointmentType type = (AppointmentType)ComboBoxAddType.SelectedIndex;
            Doctor doc = doctorController.GetById(ab[2]);
            Patient pat = patientContoller.GetById(LabelPID.Content.ToString());

            List<DateBlock> t = DateBlock.getIntersection(AppointmentContoller.getFreeTimeForDoctor(date.Date, duration, doc, 8, 20), AppointmentContoller.getFreeTimeForPatient(date.Date, duration, pat, 8, 20));

            foreach (DateBlock block in t)
            {
                if (block.Start.TimeOfDay.Equals(date.TimeOfDay))
                {
                    AppointmentContoller.CreateAppointment(ab[2], LabelPID.Content.ToString(), date, false, type, "314", duration);
                    return;
                }
            }
            LoadAppointments(LabelPID.Content.ToString());
        }

        private void DatePickerAdd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            updateLists();
        }
    }
}
