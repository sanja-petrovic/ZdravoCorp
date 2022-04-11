using System;
using System.Collections.Generic;
public class AppointmentController
{
    private AppointmentService appointmentService;

    public AppointmentService AppointmentService { get => appointmentService; set => appointmentService = value; }

    public List<Appointment> GetAll()
    {
        return this.AppointmentService.GetAll();
    }

    public Appointment GetAppointmentById(String id)
    {
        return this.AppointmentService.GetAppointmentById(id);
    }

    public List<Appointment> GetAppointmentsByPatientId(String id)
    {
        return this.appointmentService.GetAppointmentsByPatientId(id);
    }

    public List<Appointment> GetAppointmentsByDoctorId(String id)
    {
        return this.appointmentService.GetAppointmentsByDoctorId(id);
    }

    public void CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        this.appointmentService.CreateAppointment(doctorId, patientId, dateAndTime, emergency, type, roomId, duration);
    }

    public void DeleteAppointment(String id)
    {
        this.appointmentService.DeleteAppointment(id);
    }

    public void EditAppointment(String apointmentId, DateTime dateTime, String roomId, String patientId, String doctorId, List<String> diagnosis, String doctorsNotes)
    {
        this.appointmentService.EditAppointment(apointmentId, dateTime, roomId, patientId, doctorId, diagnosis, doctorsNotes);
    }

}