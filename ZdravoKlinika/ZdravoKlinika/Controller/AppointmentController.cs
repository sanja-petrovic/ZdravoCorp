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
    public List<Appointment> GetAppointmentsByPatientIdForDate(String id,DateTime date)
    {
        return this.appointmentService.GetAppointmentsByPatientIdForDate(id, date);
    }
    public List<Appointment> GetAppointmentsByDoctorIdForDate(String id, DateTime date)
    {
        return this.appointmentService.GetAppointmentsByDoctorIdForDate(id, date);
    }
    public List<Doctor> getFreeDoctorsForTime(DateTime time, int duration)
    {
        return this.appointmentService.getFreeDoctorsForTime(time, duration);
    }
    public List<DateTime> getFreeTimeForDoctor(DateTime date, int duration, Doctor doctor, int startHours, int endHours)
    {
        return this.appointmentService.getFreeTimeForDoctor(date, duration, doctor, startHours, endHours);
    }
    public List<DateTime> getFreeTimeForPatient(DateTime date, int duration, RegisteredPatient patient, int startHours, int endHours)
    {
        return this.appointmentService.getFreeTimeForPatient(date, duration, patient, startHours, endHours);
    }
    public Appointment CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        return this.appointmentService.CreateAppointment(doctorId, patientId, dateAndTime, emergency, type, roomId, duration);
    }

    public void DeleteAppointment(int id)
    {
        this.appointmentService.DeleteAppointment(id);
    }

    public void EditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        this.appointmentService.EditAppointment(appointmentId, doctorId, patientId, dateAndTime, emergency, type, roomId, duration);
    }

}