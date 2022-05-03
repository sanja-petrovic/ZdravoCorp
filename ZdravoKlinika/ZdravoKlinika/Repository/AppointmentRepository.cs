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
    private MedicationRepository mediicationRepository;
    private List<Appointment> appointments;
    

    public AppointmentRepository()
    {
        this.appointmentDataHandler = new AppointmentDataHandler();
        this.appointments = this.appointmentDataHandler.Read();
        
        doctorRepository = new DoctorRepository();
        roomRepository = new RoomRepository();
        patientRepository = new PatientRepository();
        mediicationRepository = new MedicationRepository();
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
    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }
    public PatientRepository PatientRepository1 { get => patientRepository; set => patientRepository = value; }

    private void updateReferences()
    {
        appointments = appointmentDataHandler.Read();
        List<Doctor> doctors = doctorRepository.GetAll();
        List<Room> rooms = roomRepository.GetAll();
        List<Patient> patients = patientRepository.GetAll();

        foreach (Appointment appointment in appointments)
        {
            appointment.Patient = patientRepository.GetById(appointment.Patient.GetPatientId());
            appointment.Doctor = doctorRepository.GetById(appointment.Doctor.PersonalId);
            appointment.Room = roomRepository.GetById(appointment.Room.RoomId);
            for(int i = 0; i < appointment.Prescriptions.Count; i++ )
            {
                appointment.Prescriptions[i] = this.mediicationRepository.GetById(appointment.Prescriptions[i].MedicationId);
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
        updateReferences();

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
        this.appointments = this.appointmentDataHandler.Read();
        updateReferences();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.Doctor.PersonalId.Equals(doctorId))
            {
                doctorsAppointments.Add(appointment);
            }
        }

        return doctorsAppointments;
    }

    public Appointment GetAppointmentByDoctorDateTime(String doctorId, DateTime dateTime)
    {
        List<Appointment> appointmentsByDoctor = GetAppointmentsByDoctor(doctorId);
        if(appointmentsByDoctor.Count != 0)
        {
            foreach(Appointment appointment in appointmentsByDoctor)
            {
                if(appointment.DateAndTime == dateTime)
                {
                    return appointment;
                }
            }
        }

        return null;
    }

    public List<Appointment> GetAppointmentsByDoctorDate(String doctorId, DateTime dateTime)
    {
        List<Appointment> appointmentsByDoctor = GetAppointmentsByDoctor(doctorId);
        List<Appointment> appointments = new List<Appointment>();
        if (appointmentsByDoctor.Count > 0)
        {
            foreach(Appointment appointment in appointmentsByDoctor)
            {
                if(appointment.DateAndTime.Date.Equals(dateTime.Date))
                {
                    appointments.Add(appointment);
                }
            }
        }

        return appointments;
    }
    public void CreateAppointment(Appointment appointment)
    {
        this.appointments.Add(appointment);
        appointmentDataHandler.Write(this.appointments);
    }

    public void DeleteAppointment(Appointment appointment)
    {

        var a = this.appointments.Find(x => x.AppointmentId == appointment.AppointmentId);
        this.appointments.Remove(a);
        appointmentDataHandler.Write(appointments);
    }

    public void EditAppointment(Appointment appointment)
    {
        int index = -1;
        foreach (Appointment a in this.appointments)
        {
            if (a.AppointmentId == a.AppointmentId)
            {
                index = this.appointments.IndexOf(a);
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Error");
            return;
        }

        appointments[index] = appointment;
        this.appointmentDataHandler.Write(this.appointments);

        return;

    }

    public void LogAppointment(Appointment appointment)
    {
        int index = -1;
        foreach(Appointment a in this.appointments)
        {
            if (a.AppointmentId == appointment.AppointmentId)
            {
                index = this.appointments.IndexOf(a);
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Error");
            return;
        }

        this.appointments[index] = appointment;
        this.appointmentDataHandler.Write(this.appointments);

        return;
    }

    public List<Appointment> GetPatientsPastAppointments(RegisteredPatient patient)
    {
        this.appointments = appointmentDataHandler.Read();
        updateReferences();
        List<Appointment> pastAppointments = new List<Appointment>();
        foreach(Appointment appointment in this.appointments)
        {
            if(appointment.Patient.GetPatientId().Equals(patient.PersonalId) && appointment.Over)
            {
                pastAppointments.Add(appointment);
            }
        }

        return pastAppointments;
    }

    public List<Appointment> GetPatientsUpcomingAppointments(RegisteredPatient patient)
    {
        List<Appointment> upcomingAppointments = new List<Appointment>();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.Patient.GetPatientId().Equals(patient.PersonalId) && !appointment.Over)
            {
                upcomingAppointments.Add(appointment);
            }
        }

        return upcomingAppointments;
    }

    public void appointmentListUpdated()
    {
        this.appointments = this.appointmentDataHandler.Read();
    }

}