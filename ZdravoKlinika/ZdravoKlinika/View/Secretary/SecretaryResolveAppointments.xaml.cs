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
using ZdravoKlinika.Util;
using ZdravoKlinika.ViewModel.SecretaryViewModel;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryResolveAppointments.xaml
    /// </summary>
    public partial class SecretaryResolveAppointments : Page
    {
        PatientController patientContoller;
        AppointmentController appointmentContoller;
        RoomController roomContoller;
        String specialitty;
        int duration;
        DoctorController doctorController;
        List<Appointment> apps;
        bool areRoomsFree;
        public SecretaryResolveAppointments(String specialitty, int duration, bool areRoomsFree)
        {
            InitializeComponent();
            this.specialitty = specialitty;
            this.duration = duration;
            this.areRoomsFree = areRoomsFree;
            patientContoller = new PatientController();
            doctorController = new DoctorController();
            AppointmentContoller = new AppointmentController();
            roomContoller = new RoomController();

            UpdateDataGrid();
        }

       

        public PatientController PatientContoller { get => patientContoller; set => patientContoller = value; }
        public AppointmentController AppointmentContoller { get => appointmentContoller; set => appointmentContoller = value; }

        private void LoadAppointmentsForRooms()
        {
            AppointmentDataGrid.ItemsSource = null;
            apps = new List<Appointment>();
            foreach (Room room in roomContoller.GetAll())
            {
                if (room.Type == RoomType.operating)
                {
                    foreach (Appointment app in appointmentContoller.GetAppointmentsByRoomIdInSpecificTimeFrame(room.RoomId, DateTime.Now, DateTime.Now.AddHours(1).AddMinutes(duration)))
                    {
                        apps.Add(app);
                    }
                }    
            }
            AppointmentDataGrid.ItemsSource = apps;
        }
        private void LoadAppointmentsForDoctors()
        {
            AppointmentDataGrid.ItemsSource = null;
            apps = new List<Appointment>();
            foreach (Doctor doc in doctorController.GetBySpecialty(specialitty))
            {
                foreach (Appointment app in appointmentContoller.GetAppointmentsByDoctorIdInSpecificTimeFrame(doc.PersonalId, DateTime.Now, DateTime.Now.AddHours(1).AddMinutes(duration)))
                    apps.Add(app);
            }
            AppointmentDataGrid.ItemsSource = apps;

        }

        private void UpdateDataGrid() 
        {
            if (areRoomsFree)
            {
                LoadAppointmentsForDoctors();
            }
            else
            {
                LoadAppointmentsForRooms();
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

                    foreach (DateBlock block in AppointmentContoller.getFreeTimeForPatient(((DateTime)SelectedDateUpdate.SelectedDate).Date, selected.Duration, selected.Patient, 8, 20))
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

                AppointmentContoller.EditAppointment(selected.AppointmentId, ab[2], selected.Patient.GetPatientId(), date, selected.Emergency, selected.Type, selected.Room.RoomId, selected.Duration);
                UpdateDataGrid();
            }

        }
    }
}