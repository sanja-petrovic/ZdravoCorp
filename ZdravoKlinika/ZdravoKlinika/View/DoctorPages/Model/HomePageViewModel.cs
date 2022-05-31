using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class HomePageViewModel : ViewModelBase
    {
        
        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }
        public int SelectedAppointmentId { get => selectedAppointmentId; set => SetProperty(ref selectedAppointmentId, value); }
        public string PatientId { get => patientId; set => SetProperty(ref patientId, value); }
        public string PatientName { get => patientName; set => SetProperty(ref patientName, value); }
        public string DateOfBirth { get => dateOfBirth; set => SetProperty(ref dateOfBirth, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string Diagnoses { get => diagnoses; set => SetProperty(ref diagnoses, value); }
        public string Therapy { get => therapy; set => SetProperty(ref therapy, value); }
        public Appointment SelectedAppointment { get => selectedAppointment; set => SetProperty(ref selectedAppointment, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public Visibility AboutVisibility { get => aboutVisibility; set => SetProperty(ref aboutVisibility, value); }

        private Visibility aboutVisibility;

        public MyICommand LogAppointment { get; set; }

        private Doctor doctor;
        AppointmentController appointmentController;
        Appointment selectedAppointment;
        private int selectedAppointmentId;
        private string patientId;
        private string patientName;
        private string dateOfBirth;
        private string gender;
        private string diagnoses;
        private string therapy;

        public HomePageViewModel()
        {
            LogAppointment = new MyICommand(ExecuteLog);
            this.doctor = RegisteredUserController.UserToDoctor(App.User);
            this.Appointments = new ObservableCollection<AppointmentViewModel>();
            this.appointmentController = new AppointmentController();
            List<Appointment> appts = appointmentController.GetAppointmentsByDoctorDate(doctor.PersonalId, DateTime.Today);
            foreach (Appointment appointment in appts)
            {
                {

                    Patient patient = appointment.Patient;
                    DateTime time = appointment.DateAndTime;
                    Room room = appointment.Room;


                    if (!appointment.Over)
                        this.Appointments.Add(new AppointmentViewModel { Id = appointment.AppointmentId, Name = patient.GetPatientFullName(), Time = time.ToShortTimeString(), Type = appointment.getTranslatedType(), Room = room });
                }

            }
            AboutVisibility = Appointments.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public void ExecuteLog()
        {
            DialogHelper.DialogService dialogService = new DialogHelper.DialogService();
            dialogService.ShowLogApptDialog(SelectedAppointmentId);
        }

        public void infoChange()
        {
            this.Appointments = new ObservableCollection<AppointmentViewModel>();
            this.appointmentController = new AppointmentController();
            List<Appointment> appts = appointmentController.GetAppointmentsByDoctorDate(doctor.PersonalId, DateTime.Today);

            foreach (Appointment appointment in appts)
            {
                RegisteredPatient patient = (RegisteredPatient)appointment.Patient;
                DateTime time = appointment.DateAndTime;
                Room room = appointment.Room;


                this.Appointments.Add(new AppointmentViewModel { Id = appointment.AppointmentId, Name = patient.Name + " " + patient.Lastname, Time = time.ToShortTimeString(), Type = appointment.getTranslatedType(), Room = room });
            }
        }

        public void SelectionChanged(int selectedId)
        {
            SelectedAppointmentId = selectedId;
            SelectedAppointment = appointmentController.GetAppointmentById(selectedId);
            PatientName = SelectedAppointment.Patient.GetPatientFullName();
            PatientId = SelectedAppointment.Patient.GetPatientId();
            Patient patient = SelectedAppointment.Patient;
            if(patient.GetPatientType() == PatientType.Registered)
            {
                DateOfBirth = ((RegisteredPatient) patient).DateOfBirth.ToString("dd.MM.yyyy.");
                Gender = ((RegisteredPatient)patient).GenderToString();
                Diagnoses = "";
                foreach (string diagnosis in ((RegisteredPatient)patient).MedicalRecord.Diagnoses)
                {
                    Diagnoses += diagnosis;
                    if (((RegisteredPatient) patient).MedicalRecord.Diagnoses.Last() != diagnosis)
                    {
                        Diagnoses += ", ";
                    }
                }
                Therapy = "";
                foreach (Medication prescription in ((RegisteredPatient)patient).MedicalRecord.CurrentMedication)
                {
                    Therapy += prescription.BrandName + " " + prescription.Dosage + "\n";
                }
            }
        }
    }
}
