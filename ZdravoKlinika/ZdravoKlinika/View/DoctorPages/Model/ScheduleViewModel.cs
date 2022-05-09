using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
                RegisteredPatient patient = (RegisteredPatient)appointment.Patient;
                string diagnoses = "";
                foreach (string diagnosis in patient.MedicalRecord.Diagnoses)
                {
                    diagnoses += diagnosis;
                    if (patient.MedicalRecord.Diagnoses.Last() != diagnosis)
                    {
                        diagnoses += ", ";
                    }
                }
                string prescriptions = "";
                foreach (Medication prescription in patient.MedicalRecord.CurrentMedication)
                {
                    prescriptions += prescription.BrandName + " " + prescription.Dosage;
                    if (patient.MedicalRecord.CurrentMedication.Last() != prescription)
                    {
                        prescriptions += ", ";
                    }
                }

                string lastDate = "Nema";
                /*if (patient.MedicalRecord.)
                {
                    lastDate = patient.MedicalRecord.PastAppointments.Last().DateAndTime.ToShortDateString();
                }*/
                Tabs.Add(new ScheduleTabItem { Time = appointment.DateAndTime.ToShortTimeString(), AppointmentType = appointment.getTranslatedType(), PatientId = patient.GetPatientId(), PatientName = patient.Name + " " + patient.Lastname, Room = appointment.Room.RoomId, Diagnoses = diagnoses, LastDate = lastDate, Prescriptions = prescriptions });

            }

        }

    }
}
