using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AppointmentRepository
{
    private AppointmentDataHandler appointmentDataHandler;
    private List<Appointment> appointments;
    

    public AppointmentRepository()
    {
        this.appointmentDataHandler = new AppointmentDataHandler();
        this.appointments = this.appointmentDataHandler.Read();
    }

    public List<Appointment> Appointments
    {
        get
        {
            if (appointments == null)
                appointments = new List<Appointment>();
            return appointments;
        }
        set
        {
            RemoveAllAppointment();
            if (value != null)
            {
                foreach (Appointment oAppointment in value)
                    AddAppointment(oAppointment);
            }
        }
    }

    public AppointmentDataHandler AppointmentDataHandler { get => appointmentDataHandler; set => appointmentDataHandler = value; }

    public void AddAppointment(Appointment newAppointment)
    {
        if (newAppointment == null)
            return;
        if (this.appointments == null)
            this.appointments = new List<Appointment>();
        if (!this.appointments.Contains(newAppointment))
            this.appointments.Add(newAppointment);
    }
    public void RemoveAppointment(Appointment oldAppointment)
    {
        if (oldAppointment == null)
            return;
        if (this.appointments != null)
            if (this.appointments.Contains(oldAppointment))
                this.appointments.Remove(oldAppointment);
    }
    public void RemoveAllAppointment()
    {
        if (appointments != null)
            appointments.Clear();
    }

    public List<Appointment> GetAll()
    {
        return this.appointmentDataHandler.Read();
    }

    public Appointment GetAppointmentById(int id)
    {

        this.appointments = this.appointmentDataHandler.Read();

        foreach(Appointment appointment in this.appointments)
        {
            if(appointment.AppointmentId == id)
            {
                return appointment;
            }
        }

        return null;
    }

    public List<Appointment> GetAppointmentsByPatient(String patientId)
    {
        List<Appointment> patientAppointments = new List<Appointment>();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.PatientId.Equals(patientId))
            {
                patientAppointments.Add(appointment);
            }
        }
        return patientAppointments;
    }

    public List<Appointment> GetAppointmentsByDoctor(String doctorId)
    {
        List<Appointment> doctorsAppointments = new List<Appointment>();
        this.appointments = this.appointmentDataHandler.Read();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.DoctorId == doctorId)
            {
                doctorsAppointments.Add(appointment);
            }
        }

        return doctorsAppointments;
    }

    public void CreateAppointment(Appointment appointment)
    {
        this.appointments.Add(appointment);
        appointmentDataHandler.Write(this.appointments);
    }

    public void DeleteAppointment(Appointment appointment)
    {
        /*if (appointment == null)
            return;
        if (this.appointments != null) 
        {
            if (this.appointments.Find(appointment)) 
            {
                this.appointments.Remove(appointment)
            }
        };*/

        var a = this.appointments.Find(x => x.AppointmentId == appointment.AppointmentId);
        this.appointments.Remove(a);
        appointmentDataHandler.Write(appointments);
    }

    public void EditAppointment(Appointment appointment)
    {
        if (appointment == null)
            return;
        if (this.appointments != null)
            if (this.appointments.Contains(appointment))
                this.appointments.Remove(appointment);
        this.CreateAppointment(appointment);

    }

}