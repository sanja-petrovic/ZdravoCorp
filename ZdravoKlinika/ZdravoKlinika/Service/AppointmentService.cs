using System;
using System.Collections.Generic;
using System.Linq;
public class AppointmentService
{
    private AppointmentRepository appointmentRepository;

    public AppointmentService()
    {
        this.AppointmentRepository = new AppointmentRepository();
    }
 
    public AppointmentRepository AppointmentRepository { get => appointmentRepository; set => appointmentRepository = value; }

    public List<Appointment> GetAll()
    {
        return this.appointmentRepository.GetAll();
    }

    public Appointment GetAppointmentById(int id)
    {
        return this.appointmentRepository.GetAppointmentById(id);
    }

    public List<Appointment> GetAppointmentsByPatientId(String id)
    {
        return this.appointmentRepository.GetAppointmentsByPatient(id);
    }

    public List<Appointment> GetAppointmentsByDoctorId(String id)
    {
        return this.appointmentRepository.GetAppointmentsByPatient(id);
    }
    
    public Appointment CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        List<Appointment> appointments = this.AppointmentRepository.GetAll();
        int newAppointmentId;
        if(appointments.Count > 0)
        {
            newAppointmentId = appointments.Last().AppointmentId + 1;
        } else
        {
            newAppointmentId = 1;
        }
        //TODO
        Appointment appointment = new Appointment(newAppointmentId, doctorId, patientId, roomId, duration, emergency, type, dateAndTime);
        this.appointmentRepository.CreateAppointment(appointment);

        return appointment;
    }

    public void DeleteAppointment(int id)
    {
        Appointment appointment = appointmentRepository.GetAppointmentById(id);
        this.appointmentRepository.DeleteAppointment(appointment);
    }

    public void EditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        Appointment appointment = this.appointmentRepository.GetAppointmentById(appointmentId);
        appointment.DateAndTime = dateAndTime;
        appointment.RoomId = roomId;
        appointment.PatientId = patientId;
        appointment.DoctorId = doctorId;
        appointment.Emergency = emergency;
        this.appointmentRepository.EditAppointment(appointment);

    }

}