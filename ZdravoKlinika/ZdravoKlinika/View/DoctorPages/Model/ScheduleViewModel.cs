using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class ScheduleViewModel : ViewModelBase
    {
        private DateTime selected;
        private Doctor doctor;
        public ObservableCollection<ScheduleTabItem> Tabs { get; set; }
        public DateTime Selected { get => selected; set => SetProperty(ref selected, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        private DelegateCommand recordCommand;
        RegisteredPatient regPatient;
        private Visibility recordVisibility;
        private int selectedIndex;

        public RegisteredPatient RegPatient { get => regPatient; set => SetProperty(ref regPatient, value); }
        public Visibility RecordVisibility { get => recordVisibility; set => SetProperty(ref recordVisibility, value); }
        public DelegateCommand RecordCommand { get => recordCommand; set => recordCommand = value; }
        public int SelectedIndex { get => selectedIndex; set => SetProperty(ref selectedIndex, value); }

        public void ExecuteGoToRecord()
        {
        }

        public ScheduleViewModel()
        {
            RecordCommand = new DelegateCommand(ExecuteGoToRecord);
            RecordVisibility = Visibility.Collapsed;
            Tabs = new ObservableCollection<ScheduleTabItem>();
            selected = DateTime.Today;
            SelectedIndex = Tabs.Count > 0 ? 0 : -1;
        }

        public void InfoChange()
        {
            Tabs.Clear();

            AppointmentController controller = new AppointmentController();
            List<Appointment> appointments = controller.GetAppointmentsByDoctorDate(doctor.PersonalId, Selected);
            RegisteredPatientController patientController = new RegisteredPatientController();
            appointments = appointments.OrderBy(a => a.DateAndTime).ToList();

            foreach (Appointment appointment in appointments)
            {
                
                IPatient patient = appointment.Patient;
                string diagnoses = "";
                string prescriptions = "";
                if(patient.GetPatientType() == PatientType.Registered)
                {
                    RegPatient = patientController.GetById(patient.GetPatientId());
                    foreach (string diagnosis in ((RegisteredPatient) patient).MedicalRecord.Diagnoses)
                    {
                        diagnoses += diagnosis;
                        if (((RegisteredPatient)patient).MedicalRecord.Diagnoses.Last() != diagnosis)
                        {
                            diagnoses += ", ";
                        }
                    }
                    foreach (Medication prescription in ((RegisteredPatient)patient).MedicalRecord.CurrentMedication)
                    {
                        prescriptions += prescription.BrandName + " " + prescription.Dosage;
                        if (((RegisteredPatient)patient).MedicalRecord.CurrentMedication.Last() != prescription)
                        {
                            prescriptions += ", ";
                        }
                    }
                }

                var past = controller.GetPatientsLatestAppointment(patient.GetPatientId());
                string lastDate = past != null ? past.DateAndTime.ToString("dd.MM.yyyy.") : "/";
                Visibility v = !appointment.Over ? Visibility.Visible : Visibility.Collapsed;

                Tabs.Add(new ScheduleTabItem { ApptId = appointment.AppointmentId, Visible = v, Parent = this, Time = appointment.DateAndTime.ToString("HH:mm"), AppointmentType = appointment.getTranslatedType(), PatientId = patient.GetPatientId(), PatientName = patient.GetPatientFullName(), Room = appointment.Room.RoomId, Diagnoses = diagnoses, LastDate = lastDate, Prescriptions = prescriptions, Duration = appointment.Duration + " minuta", Emergency = appointment.Emergency });
                
            }

        }

    }
}
