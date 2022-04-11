using System;
using System.Collections.Generic;
using System.Linq;

public class AppointmentRepository
{
    private AppointmentDataHandler appointmentDataHandler;
    private List<Appointment> appointments;

    public AppointmentRepository(AppointmentDataHandler appointmentDataHandler)
    {
        this.appointmentDataHandler = appointmentDataHandler;
        this.appointments = new List<Appointment>();
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
        return appointmentDataHandler.Read();
    }

    public Appointment GetAppointmentById(String id)
    {
        List<Appointment> appointments = appointmentDataHandler.Read();
        foreach(Appointment appointment in appointments)
        {
            if(appointment.AppointmentId.Equals(id))
            {
                return appointment;
            }
        }

        return null;
    }

    public List<Appointment> GetAppointmentsByPatient(String patientId)
    {
        throw new NotImplementedException();
    }

    public List<Appointment> GetAppointmentsByDoctor(String doctorId)
    {
        List<Appointment> appointments = appointmentDataHandler.Read();
        List<Appointment> doctorsAppointments = new List<Appointment>();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.Doctor.Equals(doctorId))
            {
                doctorsAppointments.Add(appointment);
            }
        }

        return doctorsAppointments;
    }

    public void CreateAppointment(Appointment appointment)
    {
        List<Appointment> appointments = appointmentDataHandler.Read();
        appointments.Add(appointment);
        appointmentDataHandler.Write(appointments);
    }

    public void DeleteAppointment(Appointment appointment)
    {
        List<Appointment> appointments = appointmentDataHandler.Read();
        appointments = appointments.Where(appt => (appt.AppointmentId).Equals(appointment.AppointmentId)).ToList();

        appointmentDataHandler.Write(appointments);
    }

    public void EditAppointment(Appointment appointment)
    {
        List<Appointment> appointments = appointmentDataHandler.Read();
        appointments = appointments.Where(appt => (appt.AppointmentId).Equals(appointment.AppointmentId)).ToList();
        appointments.Add(appointment);

        appointmentDataHandler.Write(appointments);

    }

}