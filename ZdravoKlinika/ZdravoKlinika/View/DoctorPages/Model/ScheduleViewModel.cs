using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ScheduleViewModel()
        {
            Tabs = new ObservableCollection<ScheduleTabItem>();
            selected = DateTime.Today;
            //infoChange();
        }

        public void infoChange()
        {
            Tabs.Clear();

            AppointmentController controller = new AppointmentController();
            List<Appointment> appointments = controller.GetAppointmentsByDoctorDate(doctor.PersonalId, Selected);
            RegisteredPatientController patientController = new RegisteredPatientController();

            foreach (Appointment appointment in appointments)
            {
                
                Patient patient = appointment.Patient;
                string diagnoses = "";
                string prescriptions = "";
                if(patient.GetPatientType() == PatientType.Registered)
                {
                    patient = patientController.GetById(patient.GetPatientId());
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

                string lastDate = "Nema";
                /*if (patient.MedicalRecord.)
                {
                    lastDate = patient.MedicalRecord.PastAppointments.Last().DateAndTime.ToShortDateString();
                }*/
                Tabs.Add(new ScheduleTabItem { Time = appointment.DateAndTime.ToShortTimeString(), AppointmentType = appointment.getTranslatedType(), PatientId = patient.GetPatientId(), PatientName = patient.GetPatientFullName(), Room = appointment.Room.RoomId, Diagnoses = diagnoses, LastDate = lastDate, Prescriptions = prescriptions });

            }

        }

    }
}
