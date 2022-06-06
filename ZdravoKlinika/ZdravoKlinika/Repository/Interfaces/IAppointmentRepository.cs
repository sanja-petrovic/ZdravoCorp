using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IAppointmentRepository : IRepositoryBase<Appointment, int>
    {
        public List<Appointment> GetAppointmentsByRoom(Room room);
        public List<Appointment> GetPatientsUpcomingAppointments(RegisteredPatient patient);
        public List<Appointment> GetPatientsPastAppointments(RegisteredPatient patient);
        public void AddGrading(Appointment appointment, int[] grades);
        public List<Appointment> GetAppointmentsOnDate(DateTime date);
        public List<Appointment> GetAppointmentsByPatient(String patientId);
        public List<Appointment> GetAppointmentsByDoctor(String doctorId);
        public Appointment? GetAppointmentByDoctorDateTime(String doctorId, DateTime dateTime);
        public List<Appointment> GetAppointmentsByDoctorDate(String doctorId, DateTime dateTime);

    }
}
