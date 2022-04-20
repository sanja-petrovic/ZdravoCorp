using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Model
{
    internal class ScheduleViewModel : ViewModelBase
    {
        private DateTime selected;
        private ScheduleTabItem selectedTab;
        public ObservableCollection<ScheduleTabItem> Tabs { get; set; }
        public DateTime Selected { get => selected; set => SetProperty(ref selected, value); }
        public ScheduleTabItem SelectedTab { get => selectedTab; set => SetProperty(ref selectedTab, value); }

        public ScheduleViewModel()
        {
            Tabs = new ObservableCollection<ScheduleTabItem>();
            selected = DateTime.Today;
            infoChange();
        }

        public void infoChange()
        {
            Tabs.Clear();
            
            AppointmentController controller = new AppointmentController();
            List<Appointment> appointments = controller.GetAppointmentsByDoctorDate("456", Selected);
            PatientController patientController = new PatientController();

            foreach (Appointment appointment in appointments)
            {
                Patient patient = patientController.GetById(appointment.PatientId);
                string diagnoses = "";
                foreach (String diagnosis in patient.MedicalRecord.Diagnoses)
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
                if (patient.MedicalRecord.PastAppointments.Count() > 0)
                {
                    lastDate = patient.MedicalRecord.PastAppointments.Last().DateAndTime.ToShortDateString();
                }
                Tabs.Add(new ScheduleTabItem { Time = appointment.DateAndTime.ToShortTimeString(), AppointmentType = appointment.getTranslatedType(), PatientId = appointment.PatientId, PatientName = patient.Name + " " + patient.Lastname, Room = appointment.RoomId, Diagnoses = diagnoses, LastDate = lastDate, Prescriptions = prescriptions });
                
            }

            if(Tabs.Count > 0)
            {
                selectedTab = Tabs[0];
            }
        }

    }
}
