using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientDiagnosisViewModel : INotifyPropertyChanged
    {
        public PatientDiagnosisViewModel(Appointment appointment)
        {
            SelectedAppointment = appointment;
            if (SelectedAppointment.Diagnoses == null || !SelectedAppointment.Diagnoses.Equals(""))
            {
                diagnosis = "Lekar nije upisao dijagnozu.";

            }
            else
            {
                diagnosis = SelectedAppointment.Diagnoses;
            }
            if (SelectedAppointment.DoctorsNotes == null || SelectedAppointment.Diagnoses.Equals(""))
            {
                doctorNote = "Lekar nije upisao belesku.";

            }
            else
            {
                doctorNote = SelectedAppointment.DoctorsNotes;
            }
            if (SelectedAppointment.PatientNotes == null || SelectedAppointment.PatientNotes.Equals(""))
            {
                patientNote = "";
            }
            else
            {
                patientNote = SelectedAppointment.PatientNotes;
            }
            appointmentController = new AppointmentController();
            SaveCommand = new MyICommand(SavePatientNote,CanExecuteSavePatientNote);
        }

        public Appointment SelectedAppointment { get; set; }
        public string Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string DoctorNote { get => doctorNote; set => doctorNote = value; }
        public string PatientNote { get => patientNote; 
            set 
            {
                patientNote = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public MyICommand SaveCommand { get; private set; }
        public AppointmentController AppointmentController { get => appointmentController; set => appointmentController = value; }

        private AppointmentController appointmentController;
        private String diagnosis;
        private String doctorNote;
        private String patientNote;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void SavePatientNote(object data)
        {
            appointmentController.AddPatientNote(SelectedAppointment,patientNote);
            MessageBox.Show("Vaš komentar je zabelezen", "komentar", MessageBoxButton.OK);
            resetBaseView();
        }
        public bool CanExecuteSavePatientNote(object data)
        {
            bool retval = true;
            if (patientNote.Equals(null) || patientNote.Equals("")) 
                retval = false;

            return retval;
        }
        private void resetBaseView()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "patientBase")
                {
                    PatientViewBase baseWindow = (PatientViewBase)window;
                    baseWindow.refreshAppointmentView();
                }
            }
        }
    }
}
