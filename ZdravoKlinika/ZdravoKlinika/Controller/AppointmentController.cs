using System;
using System.Collections.Generic;
public class AppointmentController
{
    private AppointmentService appointmentService;

    public AppointmentService AppointmentService { get => appointmentService; set => appointmentService = value; }

    public AppointmentController()
    {
        this.AppointmentService = new AppointmentService();
    }
    public List<Appointment> GetAll()
    {
        return this.AppointmentService.GetAll();
    }

    public Appointment GetAppointmentById(int id)
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

    public void CreateAppointment(String doctorId, Patient patient, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        this.appointmentService.CreateAppointment(doctorId, patient, dateAndTime, emergency, type, roomId, duration);
    }

    public void DeleteAppointment(int id)
    {
        this.appointmentService.DeleteAppointment(id);
    }

    public void EditAppointment(int appointmentId, String doctorId, Patient patient, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        this.appointmentService.EditAppointment(appointmentId, doctorId, patient, dateAndTime, emergency, type, roomId, duration);
    }

}