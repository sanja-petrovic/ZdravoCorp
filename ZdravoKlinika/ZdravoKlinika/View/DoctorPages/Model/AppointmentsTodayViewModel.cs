using System;
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
        AppointmentController appointmentController;

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
                

                this.Appointments.Add(new AppointmentViewModel { Name = patient.Name + " " + patient.Lastname, Time = time.ToShortTimeString(), Type = appointment.getTranslatedType(), Room = room.Name });
            }
        }
    }
}
