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
    /// Interaction logic for SecretaryCreateAppointment.xaml
    /// </summary>
    public partial class SecretaryCreateAppointment : Page
    {
        PatientController patientContoller;
        AppointmentController appointmentContoller;
        PatientViewModel patientViewModel;
        DoctorController doctorController;
        List<Appointment> apps;
        public SecretaryCreateAppointment(PatientViewModel viewModel)
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

      

        private void updateLists() 
        {

            ComboBoxAddTime.ItemsSource = null;
            ComboBoxAddDoctor.ItemsSource = null;
            List<Doctor> a = new List<Doctor>();
            List<String> b = new List<String>();


            foreach (Doctor doc in doctorController.GetAll())
            {
                a.Add(doc);
            }

            foreach (DateBlock block in AppointmentContoller.GetFreeTimeForPatient(((DateTime)DatePickerAdd.SelectedDate).Date, Int32.Parse(TextBoxDurationAdd.Text), patientContoller.GetById(LabelPID.Content.ToString()), 8, 20))
            {
                b.Add(block.Start.TimeOfDay.ToString());
            }

            b.Sort();


            ComboBoxAddDoctor.ItemsSource = a;
            ComboBoxAddTime.ItemsSource = b;
            
            
        }

        private void Dodavanje(object sender, RoutedEventArgs e)
        {

            DateTime date = (DateTime)DatePickerAdd.SelectedDate;
            String[] a = ComboBoxAddTime.SelectedItem.ToString().Split(":");
            date = date.AddMinutes(Int32.Parse(a[1]));
            date = date.AddHours(Int32.Parse(a[0]));

            DateTime date2 = (DateTime.Now).AddDays(2);
            if (date <= date2)
                return;

            int duration = Int32.Parse(TextBoxDurationAdd.Text);

            AppointmentType type = (AppointmentType)ComboBoxAddType.SelectedIndex -1;
            Doctor doc = doctorController.GetById(((Doctor)ComboBoxAddDoctor.SelectedItem).PersonalId);
            IPatient pat = patientContoller.GetById(LabelPID.Content.ToString());

            List<DateBlock> t = DateBlock.getIntersection(AppointmentContoller.GetFreeTimeForUser(new DateBlock(date.Date, duration), doc, new int[] { 8, 20 }), AppointmentContoller.GetFreeTimeForPatient(date.Date, duration, pat, 8, 20));
            

            foreach (DateBlock block in t)
            {
                if (block.Start.TimeOfDay.Equals(date.TimeOfDay))
                {
                    RoomType roomType = RoomType.checkup;
                    if (type == AppointmentType.Surgery) roomType = RoomType.operating;
                    List<Room> rooms = new RoomController().GetFreeRooms(block.Start,roomType);
                    if (rooms.Count > 0)
                    {
                        AppointmentContoller.CreateAppointment(((Doctor)ComboBoxAddDoctor.SelectedItem).PersonalId, LabelPID.Content.ToString(), date, false, type, rooms.First().RoomId, duration);
                    }
                    break;
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
