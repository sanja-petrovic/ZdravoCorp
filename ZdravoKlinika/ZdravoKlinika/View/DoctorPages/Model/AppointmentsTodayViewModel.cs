﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class AppointmentsTodayViewModel : ViewModelBase
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

        AppointmentController appointmentController;
        Appointment selectedAppointment;
        private int selectedAppointmentId;
        private string patientId;
        private string patientName;
        private string dateOfBirth;
        private string gender;
        private string diagnoses;
        private string therapy;

        public AppointmentsTodayViewModel()
        {
            this.Appointments = new ObservableCollection<AppointmentViewModel>();
            this.appointmentController = new AppointmentController();
            List<Appointment> appts = appointmentController.GetAppointmentsByDoctorDate("456", DateTime.Today);

            foreach(Appointment appointment in appts)
            {
                RegisteredPatient patient = (RegisteredPatient)appointment.Patient;
                DateTime time = appointment.DateAndTime;
                Room room = appointment.Room;
                

                if(!appointment.Over)
                    this.Appointments.Add(new AppointmentViewModel { Id = appointment.AppointmentId, Name = patient.Name + " " + patient.Lastname, Time = time.ToShortTimeString(), Type = appointment.getTranslatedType(), Room = room.Name });
            }
        }

        public void infoChange()
        {
            this.Appointments = new ObservableCollection<AppointmentViewModel>();
            this.appointmentController = new AppointmentController();
            List<Appointment> appts = appointmentController.GetAppointmentsByDoctorDate("456", DateTime.Today);

            foreach (Appointment appointment in appts)
            {
                RegisteredPatient patient = (RegisteredPatient)appointment.Patient;
                DateTime time = appointment.DateAndTime;
                Room room = appointment.Room;


                this.Appointments.Add(new AppointmentViewModel { Id = appointment.AppointmentId, Name = patient.Name + " " + patient.Lastname, Time = time.ToShortTimeString(), Type = appointment.getTranslatedType(), Room = room.Name });
            }
        }

        public void SelectionChanged(int selectedId)
        {
            SelectedAppointmentId = selectedId;
            SelectedAppointment = appointmentController.GetAppointmentById(selectedId);
            PatientName = SelectedAppointment.Patient.GetPatientFullName();
            PatientId = SelectedAppointment.Patient.GetPatientId();
            RegisteredPatient patient = ((RegisteredPatient)SelectedAppointment.Patient);
            DateOfBirth = patient.DateOfBirth.ToString("dd.MM.yyyy.");
            Gender = patient.GenderToString();
            Diagnoses = "";
            foreach (string diagnosis in patient.MedicalRecord.Diagnoses)
            {
                Diagnoses += diagnosis;
                if (patient.MedicalRecord.Diagnoses.Last() != diagnosis)
                {
                    Diagnoses += ", ";
                }
            }
            Therapy = "";
            foreach (Medication prescription in patient.MedicalRecord.CurrentMedication)
            {
                Therapy += prescription.BrandName + " " + prescription.Dosage + "\n";
            }
        }
    }
}