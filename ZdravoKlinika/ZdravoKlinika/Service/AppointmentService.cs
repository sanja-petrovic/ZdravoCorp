using System;
using System.Collections.Generic;
public class AppointmentService
{
    private AppointmentRepository appointmentRepository;

    public AppointmentService(AppointmentRepository appointmentRepository)
    {
        this.AppointmentRepository = appointmentRepository;
    }
 
    public AppointmentRepository AppointmentRepository { get => appointmentRepository; set => appointmentRepository = value; }

    public List<Appointment> GetAll()
    {
        return this.appointmentRepository.GetAll();
    }

    public Appointment GetAppointmentById(String id)
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

    public void CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        Appointment appointment = new Appointment();
        this.appointmentRepository.CreateAppointment(appointment);
    }

    public void DeleteAppointment(String id)
    {
        Appointment appointment = appointmentRepository.GetAppointmentById(id);
        this.appointmentRepository.DeleteAppointment(appointment);
    }

    public void EditAppointment(String apointmentId, DateTime dateTime, String roomId, String patientId, String doctorId, List<String> diagnosis, String doctorsNotes)
    {
        Appointment appointment = new Appointment();
        this.appointmentRepository.EditAppointment(appointment);
    }

}