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
    private MedicationRepository medicationRepository;
    private List<Appointment> appointments;
    

    public AppointmentRepository()
    {
        appointmentDataHandler = new AppointmentDataHandler();
        ReadDataFromFile();

        doctorRepository = new DoctorRepository();
        roomRepository = new RoomRepository();
        patientRepository = new PatientRepository();
        medicationRepository = new MedicationRepository();
        
    }

    private void ReadDataFromFile()
    {
        appointments = appointmentDataHandler.Read();
        if (appointments == null) appointments = new List<Appointment>();
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

    private void UpdateReferences(Appointment appointment)
    {

        if(appointment.Patient != null)
            appointment.Patient = patientRepository.GetById(appointment.Patient.GetPatientId());
        if(appointment.Doctor != null)
            appointment.Doctor = doctorRepository.GetById(appointment.Doctor.PersonalId);
        if(appointment.Room != null)
            appointment.Room = roomRepository.GetById(appointment.Room.RoomId);
        if(appointment.Prescriptions != null)
        {
            UpdatePrescriptionsForAppointment(appointment);
        } 
        
    }

    private void UpdatePrescriptionsForAppointment(Appointment appointment)
    {
        for (int i = 0; i < appointment.Prescriptions.Count; i++)
        {
            appointment.Prescriptions[i] = this.medicationRepository.GetById(appointment.Prescriptions[i].MedicationId);
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
        ReadDataFromFile();
        foreach (Appointment appointment in appointments)
        {
            UpdateReferences(appointment);
        }
        return this.appointmentDataHandler.Read();
    }

    public Appointment? GetAppointmentById(int id)
    {
        Appointment? appointmentToReturn = null;
        foreach(Appointment appointment in this.appointments)
        {
            if(appointment.AppointmentId == id)
            {
                UpdateReferences(appointment);
                appointmentToReturn = appointment;
                break;
            }
        }

        return appointmentToReturn;
    }

    public List<Appointment> GetAppointmentsByPatient(String patientId)
    {
        List<Appointment> patientAppointments = new List<Appointment>();
        foreach (Appointment appointment in this.appointments)
        {
            if (appointment.Patient.IsPatientById(patientId))
            {
                UpdateReferences(appointment);
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
                UpdateReferences(appointment);
                doctorsAppointments.Add(appointment);
            }
        }

        return doctorsAppointments;
    }

    public Appointment? GetAppointmentByDoctorDateTime(String doctorId, DateTime dateTime)
    {
        Appointment? appointmentToReturn = null;
        List<Appointment> appointmentsByDoctor = GetAppointmentsByDoctor(doctorId);
        if(appointmentsByDoctor.Count != 0)
        {
            foreach(Appointment appointment in appointmentsByDoctor)
            {
                if(appointment.DateAndTime == dateTime)
                {
                    UpdateReferences(appointment);
                    appointmentToReturn = appointment;
                    break;
                }
            }
        }

        return appointmentToReturn;
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
                    UpdateReferences(appointment);
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
            throw new Exception("BAD");
        }
        appointments[index] = appointment;
        appointmentDataHandler.Write(this.appointments);

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
            throw new Exception("BAD");
        }

        appointments[index] = appointment;
        appointmentDataHandler.Write(appointments);

        return;
    }

    public List<Appointment> GetPatientsPastAppointments(RegisteredPatient patient)
    {
        List<Appointment> pastAppointments = new List<Appointment>();
        foreach(Appointment appointment in this.appointments)
        {
            if(appointment.Patient.GetPatientId().Equals(patient.PersonalId) && appointment.Over)
            {
                UpdateReferences(appointment);
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
                UpdateReferences(appointment);
                upcomingAppointments.Add(appointment);
            }
        }

        return upcomingAppointments;
    }

    public List<Appointment> GetAppointmentsByRoom(Room room)
    {
        List<Appointment> appointmentsByRoom = new List<Appointment>();
        foreach(Appointment appointment in this.appointments)
        {
            if(appointment.Room.RoomId.Equals(room.RoomId))
            {
                UpdateReferences(appointment);
                appointmentsByRoom.Add(appointment);
            }
        }

        return appointmentsByRoom;
    }


}