using System;
using System.Collections.Generic;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Util;

public class AppointmentController
{
    private AppointmentService appointmentService;
    private PatientService patientService;
    private DoctorService doctorService;
    private RoomService roomService;

    public AppointmentService AppointmentService { get => appointmentService; set => appointmentService = value; }

    public AppointmentController()
    {
        this.AppointmentService = new AppointmentService();
        this.patientService = new PatientService();
        this.doctorService = new DoctorService();
        this.roomService = new RoomService();
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

    public Appointment GetAppointmentByDoctorDateTime(String doctorId, DateTime dateTime)
    {
        return this.appointmentService.GetAppointmentByDoctorDateTime(doctorId, dateTime);
    }

    public List<Appointment> GetAppointmentsByDoctorDate(String doctorId, DateTime dateTime)
    {
        return this.appointmentService.GetAppointmentsByDoctorDate(doctorId, dateTime);
    }

    public List<Appointment> GetAppointmentsByPatientIdForDate(String id,DateTime date)
    {
        return this.appointmentService.GetAppointmentsByPatientIdForDate(id, date);
    }
    public List<Appointment> GetAppointmentsByDoctorIdForDate(String id, DateTime date)
    {
        return this.appointmentService.GetAppointmentsByDoctorIdForDate(id, date);
    }
    public List<Doctor> getFreeDoctorsForTime(DateBlock block, int startHours, int endHours)
    {
        return this.appointmentService.GetFreeDoctorsForTime(block,startHours,endHours);
    }

    public List<Appointment> GetAppointmentsByDoctorIdInSpecificTimeFrame(String id, DateTime start, DateTime finish)
    {
        return this.appointmentService.GetAppointmentsByDoctorIdInSpecificTimeFrame(id,start,finish);
    }

    public List<Appointment> GetAppointmentsByRoomIdInSpecificTimeFrame(string roomId, DateTime start, DateTime finish)
    {
        return this.appointmentService.GetAppointmentsByRoomIdInSpecificTimeFrame(roomId, start, finish);
    }

    public List<DateBlock> getFreeTimeForDoctor(DateTime date, int duration, Doctor doctor, int startHours, int endHours)
    {
        return this.appointmentService.GetFreeTimeForDoctor(date, duration, doctor, startHours, endHours);
    }
    public List<DateBlock> getFreeTimeForPatient(DateTime date, int duration, IPatient patient, int startHours, int endHours)
    {
        return this.appointmentService.GetFreeTimeForPatient(date, duration, patient, startHours, endHours);
    }
    public List<Doctor> GetFreeDoctorsBySpecialityForNextHour(int duration, String specialitty)
    {
        return this.appointmentService.GetFreeDoctorsBySpecialityForNextHour(duration, specialitty);
    }

    public List<String> ConvertDateBlockToString()
    {
        List<String> list = new List<String>();

        return list;
    }
    public void AddPatientNote(Appointment appointment, String note)
    {
        this.appointmentService.AddPatientNote(appointment, note);
    }
    public void CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        PatientController patientController = new PatientController();
        DoctorController doctorController = new DoctorController();
        RoomController roomController = new RoomController();
        Appointment appointment = new Appointment(-1, doctorController.GetById(doctorId), patientController.GetById(patientId), roomController.GetById(roomId), duration, emergency, type, dateAndTime);
        this.appointmentService.CreateAppointment(appointment);
    }


    public void DeleteAppointment(int id)
    {
        this.appointmentService.DeleteAppointment(id);
    }
    public void PatientDeleteAppointment(int id, String patientId)
    {
        try
        {
            this.appointmentService.PatientDeleteAppointment(id, patientId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void EditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        Appointment appointment = new Appointment();
        appointment.AppointmentId = appointmentId;
        appointment.Patient = patientService.GetById(patientId);
        appointment.Doctor = doctorService.GetById(doctorId);
        appointment.DateAndTime = dateAndTime;
        appointment.Emergency = emergency;
        appointment.Type = type;
        appointment.Room = roomService.GetById(roomId);
        appointment.Duration = duration;
        this.appointmentService.EditAppointment(appointment);
    }

    public void EditAppointment(int appointmentId, Doctor doctor, IPatient patient, DateTime dateAndTime, bool emergency, AppointmentType type, Room room, int duration)
    {
        Appointment a = new Appointment(appointmentId, doctor, patient, room, duration, emergency, type, dateAndTime);
        this.appointmentService.EditAppointment(a);
    }

    public void UpdateAnamnesis(int appointmentId, string note, string diagnosis)
    {
        Appointment a = GetAppointmentById(appointmentId);
        a.DoctorsNotes = note;
        a.Diagnoses = diagnosis;
        this.appointmentService.EditAppointment(a);
    }
    public void PatientEditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        Appointment appointment = new Appointment();
        appointment.AppointmentId = appointmentId;
        appointment.Patient = patientService.GetById(patientId);
        appointment.Doctor = doctorService.GetById(doctorId);
        appointment.DateAndTime = dateAndTime;
        appointment.Emergency = emergency;
        appointment.Type = type;
        appointment.Room = roomService.GetById(roomId);
        appointment.Duration = duration;
        try
        {
            this.appointmentService.PatientEditAppointment(appointment);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void LogAppointment(Appointment appointment, String diagnoses, String doctorsNote)
    {
        this.appointmentService.LogAppointment(appointment, diagnoses, doctorsNote);
    }
    public void AddGrading(int appointmentId, int[] grades)
    {
        this.appointmentService.AddGrading(appointmentId, grades);
    }

    public List<Appointment> GetPatientsPastAppointments(RegisteredPatient patient)
    {
        return this.AppointmentService.GetPatientsPastAppointments(patient);
    }

    public List<Appointment> GetPatientsUpcomingAppointments(RegisteredPatient patient)
    {
        return this.appointmentService.GetPatientsUpcomingAppointments(patient);
    }

    public Appointment GetLatestAppointment(RegisteredPatient patient)
    {
        List<Appointment> allPast = this.GetPatientsPastAppointments(patient);

        return null;
    }
    public List<Appointment> GetAppointmentsByRoom(String roomId)
    {
        return this.appointmentService.GetAppointmentsByRoom(roomId);
    }

    public List<DateBlock> GetDateBlocksForDoctorInNextHour(int duration, Doctor doc)
    {
        return appointmentService.GetDateBlocksForDoctorInNextHour(duration, doc);
    }

    public int CountNumberOfGradesForDoctor(int questionNumber, int gradeToCount, Doctor doctor)
    {
        return appointmentService.CountNumberOfGradesForDoctor(questionNumber, gradeToCount, doctor);
    }

    public double GetAverageGradeForDoctor(int questionNumber, Doctor doctor)
    {
        return appointmentService.GetAverageGradeForDoctor(questionNumber, doctor);
    }
    public List<DateBlock> GetFreeTime(string doctorId, string patientId, DateBlock block)
    {
        PatientController patientController = new PatientController();
        DoctorController doctorController = new DoctorController();
        return this.appointmentService.GetFreeTime(doctorController.GetById(doctorId), patientController.GetById(patientId), block);
    }

    public Appointment GetPatientsLatestAppointment(String patientId)
    {
        RegisteredPatientController patientController = new RegisteredPatientController();
        return this.appointmentService.GetPatientsLatestAppointment(patientController.GetById(patientId));
    }

}