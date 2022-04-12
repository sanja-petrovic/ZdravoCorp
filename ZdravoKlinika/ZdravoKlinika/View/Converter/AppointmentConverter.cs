using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Converter
{
    class AppointmentConverter : AbstractConverter
    {
        public static AppointmentView ConvertAppointmentToAppointmentView(Appointment appointment)
            => new AppointmentView
            {
                DateAndTime = appointment.DateAndTime,
                PatientName = appointment.Patient.Name,
                Type = appointment.Type.ToString(),
                Emergency = appointment.Emergency.ToString()

            };


        public static IList<AppointmentView> ConvertAppointmentListToAppointmentViewList(IList<Appointment> appointments)
            => ConvertEntityListToViewList(appointments, ConvertAppointmentToAppointmentView);
    }
}
