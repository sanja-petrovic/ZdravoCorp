using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

public class AppointmentRepository
{
    private AppointmentDataHandler appointmentDataHandler;
    private DoctorRepository doctorRepository;
    private PatientRepository patientRepository;
    private RoomRepository roomRepository;
    private List<Appointment> appointments;
    

    public AppointmentRepository()
    {
        this.appointmentDataHandler = new AppointmentDataHandler();
        this.appointments = this.appointmentDataHandler.Read();
        
        DoctorRepository = new DoctorRepository();
        RoomRepository = new RoomRepository();
        PatientRepository = new PatientRepository();
        updateReferences();
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
    public DoctorRepository DoctorRepository { get => doctorRepository; set => doctorRepository = value; }
    public PatientRepository PatientRepository { get => PatientRepository1; set => PatientRepository1 = value; }
    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }
    public PatientRepository PatientRepository1 { get => patientRepository; set => patientRepository = value; }

    private void updateReferences()
    {
        List<Doctor> doctors = DoctorRepository.GetAll();
        List<Room> rooms = RoomRepository.GetAll();
        List<Patient> patients = PatientRepository.GetAll();
        if (doctors != null && rooms != null && patients != null)
        {
            foreach (Doctor doc in doctors)
            {
                foreach (Appointment appointment in Appointments)
                {
                    if (appointment.Doctor.PersonalId.Equals(doc.PersonalId)) 
                    {
                        appointment.Doctor = doc;
                        break;
                    }
                }
            }
            foreach (Room room in rooms)
            {
                foreach (Appointment appointment in Appointments)
                {
                    if (appointment.Room.RoomId.Equals(room.RoomId))
                    {
                        appointment.Room = room;
                        break;
                    }
                }
            }
            foreach (Patient pat in patients)
            {
                foreach (Appointment appointment in Appointments)
                {
                    if (appointment.Patient.GetPatientId().Equals(pat.GetPatientId()))
                    {
                        appointment.Patient = pat;
                        break;
                    }
                }
            }

        }

    }

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
            if (appointment.Patient.IsPatientById(patientId))
            {
                patientAppointments.Add(appointment);
            }
        }
        return patientAppointments;
    }

    public List<Appointment> GetAppointmentsByDoctor(String doctorId)
    {
        List<Appointment> doctorsAppointments = new List<Appointment>();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.Doctor.PersonalId.Equals(doctorId))
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
            if (this.appointments.Find(appointment))
                this.appointments.Remove(appointment);*/

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