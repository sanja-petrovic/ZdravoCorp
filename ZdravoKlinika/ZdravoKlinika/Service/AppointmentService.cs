using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

public class AppointmentService
{
    private AppointmentRepository appointmentRepository;
    private DoctorRepository doctorRepository;
    private PatientRepository patientRepositroy;
    private RoomRepository roomRepository;

    public AppointmentService()
    {
        this.AppointmentRepository = new AppointmentRepository();
        this.DoctorRepository = new DoctorRepository();
        this.RoomRepository = new RoomRepository();
        this.PatientRepositroy = new PatientRepository();
    }
 
    public AppointmentRepository AppointmentRepository { get => appointmentRepository; set => appointmentRepository = value; }
    public DoctorRepository DoctorRepository { get => doctorRepository; set => doctorRepository = value; }
    public RoomRepository RoomRepository { get => roomRepository; set => roomRepository = value; }
    public PatientRepository PatientRepositroy { get => patientRepositroy; set => patientRepositroy = value; }

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
        return this.appointmentRepository.GetAppointmentsByPatient(id);
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
        Doctor doc = DoctorRepository.GetById(doctorId);
        Room room = RoomRepository.GetById(roomId);
        Patient pat = PatientRepositroy.GetById(patientId);
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

        Doctor doc = DoctorRepository.GetById(doctorId);
        Room room = RoomRepository.GetById(roomId);
        Patient pat = PatientRepositroy.GetById(patientId);

        appointment.DateAndTime = dateAndTime;
        appointment.Emergency = emergency;
        appointment.Patient = pat;
        appointment.Doctor = doc;
        appointment.Room = room;

        this.appointmentRepository.EditAppointment(appointment);

    }

    public List<Appointment> GetAppointmentsByRoom(String roomId)
    {
        Room room = RoomRepository.GetById(roomId);
        return this.appointmentRepository.GetAppointmentsByRoom(room);
    }

}