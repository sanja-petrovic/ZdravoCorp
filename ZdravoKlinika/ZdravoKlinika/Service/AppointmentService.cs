using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

public class AppointmentService
{
    private AppointmentRepository appointmentRepository;
    private DoctorRepository doctorRepository;
    private PatientRepository patientRepository;
    private RoomRepository roomRepository;

    public AppointmentService()
    {
        this.appointmentRepository = new AppointmentRepository();
        this.doctorRepository = new DoctorRepository();
        this.roomRepository = new RoomRepository();
        this.patientRepository = new PatientRepository();
    }
 
    public AppointmentRepository AppointmentRepository { get => appointmentRepository; set => appointmentRepository = value; }
    public DoctorRepository DoctorRepository { get => doctorRepository; set => doctorRepository = value; }
    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }
    public PatientRepository PatientRepository { get => patientRepository; set => patientRepository = value; }

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
        return this.appointmentRepository.GetAppointmentsByDoctor(id);
    }

    public Appointment GetAppointmentByDoctorDateTime(String doctorId, DateTime dateTime)
    {
        return this.appointmentRepository.GetAppointmentByDoctorDateTime(doctorId, dateTime);
    }

    public List<Appointment> GetAppointmentsByDoctorDate(String doctorId, DateTime dateTime)
    {
        return this.appointmentRepository.GetAppointmentsByDoctorDate(doctorId, dateTime);
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
        //TODO temp fix
        Doctor doc = doctorRepository.GetById(doctorId);
        Room room = roomRepository.GetById(roomId);
        Patient pat = patientRepository.GetById(patientId);
        Appointment appointment = new Appointment(newAppointmentId, doc, pat, room, duration, emergency, type, dateAndTime);
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

        Doctor doc = doctorRepository.GetById(doctorId);
        Room room = roomRepository.GetById(roomId);
        Patient pat = patientRepository.GetById(patientId);

        appointment.DateAndTime = dateAndTime;
        appointment.Emergency = emergency;
        appointment.Patient = pat;
        appointment.Doctor = doc;
        appointment.Room = room;

        this.appointmentRepository.EditAppointment(appointment);

    }

    public void LogAppointment(Appointment appointment, String diagnoses, String doctorsNote)
    {
        appointment.Diagnoses = diagnoses;
        appointment.DoctorsNotes = doctorsNote;
        appointment.Over = true;
        this.appointmentRepository.LogAppointment(appointment);
    }
}

