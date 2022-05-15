using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Util;

public class AppointmentService
{
    private AppointmentRepository appointmentRepository;
    private DoctorRepository doctorRepository;
    private PatientRepository patientRepository;
    private RoomRepository roomRepository;
    private ZdravoKlinika.Util.ListHelper listHelper;
    private ZdravoKlinika.Util.DateBlock dateBlock = new ZdravoKlinika.Util.DateBlock();
    private ActionLogService actionLogService;
    private RegisteredPatientRepository registeredPatientRepository;

    public AppointmentService()
    {
        this.appointmentRepository = new AppointmentRepository();
        this.doctorRepository = new DoctorRepository();
        this.roomRepository = new RoomRepository();
        this.patientRepository = new PatientRepository();
        this.actionLogService = new ActionLogService();
        this.registeredPatientRepository = new RegisteredPatientRepository();
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

    public bool HasScheduledAppointments(String doctorId, DateBlock dateBlock)
    {
        bool result = false;
        foreach(Appointment a in this.GetAppointmentsByDoctorId(doctorId))
        {
            if(!a.Over)
            {
                if ((a.DateAndTime.CompareTo(dateBlock.Start)) >= 0 && (a.DateAndTime.CompareTo(dateBlock.End) <= 0)) {
                    result = true;
                }
            }
        }

        return result;
    }

    public List<Appointment> GetAppointmentsByPatientIdForDate(String id,DateTime date)
    {
        List<Appointment> appointments = new List<Appointment>();
        foreach (Appointment app in this.appointmentRepository.GetAppointmentsByPatient(id))
        {
            if(app.DateAndTime.Date == date.Date)
            {
                appointments.Add(app);
            }
        }
        return appointments;
    }
    public List<Appointment> GetAppointmentsByDoctorIdForDate(String id, DateTime date)
    {
        List<Appointment> appointments = new List<Appointment>();
        foreach (Appointment app in this.appointmentRepository.GetAppointmentsByDoctor(id))
        {
            if (app.DateAndTime.Date == date.Date)
            {
                appointments.Add(app);
            }
        }
        return appointments;
    }
    public List<Doctor> GetFreeDoctorsForTime(DateBlock block, int startHours, int endHours)
    {
        List<Doctor> doctors = doctorRepository.GetAll();
        List<Doctor> result =  new List<Doctor>();
        foreach(Doctor doc in doctors)
        {
            List<Appointment> appointments = GetAppointmentsByDoctorIdForDate(doc.PersonalId, block.Start.Date);
            if(appointments.Count == 0)
            {
                //doc has no appointmets for given day
                result.Add(doc);
                continue;
            }
            List<Appointment> sortedAppointments = appointments.OrderBy(o => o.DateAndTime).ToList();
            for (int i = 0; i < sortedAppointments.Count(); i++)
            {
                if( i == 0)
                {
                    //is there an interval before 1st and after the start of working hours
                    if (DateBlock.ContainsDateTime(block.Start.Date.AddHours(startHours), sortedAppointments[i].DateAndTime,block.Start,block.Duration))
                    {
                        result.Add(doc);
                        continue;
                    }
                }
                else
                {

                    if (DateBlock.ContainsDateTime(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration), sortedAppointments[i].DateAndTime, block.Start, block.Duration))
                    {
                        result.Add(doc);
                        continue;
                    }
                }
            }
            //all appointments have been looked at, is the needed time after the last doc appointment for the day
            if (DateBlock.ContainsDateTime(sortedAppointments.Last().DateAndTime.AddMinutes(sortedAppointments.Last().Duration), block.Start.Date.AddHours(endHours), block.Start, block.Duration))
            {
                result.Add(doc);
            }

        }
        return result;
    }

    //TODO these 2 need code cleanup, they can become one-ish ? 
    public List<DateBlock> GetFreeTimeForDoctor(DateTime date,int duration, Doctor doctor, int startHours, int endHours)
    {
        List<DateBlock> result = new List<DateBlock>();
        List<Appointment> appointments = GetAppointmentsByDoctorIdForDate(doctor.PersonalId, date.Date);
        {
            if(appointments.Count == 0)
            {
                //he has no appointmets let him choose anyhour

                result = DateBlock.getIntervals(date.AddHours(startHours), date.AddHours(endHours));
                return result;
            }
            
            List<Appointment> sortedAppointments = appointments.OrderBy(o => o.DateAndTime).ToList();

            for(int i=0; i< sortedAppointments.Count(); i++)
            {
                if (i == 0)
                {
                    //is there an interval before 1st and after the start of working hours

                    if ((date.Date.AddHours(startHours).AddMinutes(duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                    {
                        foreach (DateBlock d in DateBlock.getIntervals(date.Date.AddHours(startHours), sortedAppointments[i].DateAndTime))
                        {
                            result.Add(d);
                        }
                    }
                }
                else
                {
                    
                    if((sortedAppointments[i-1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration + duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                    {
                        //last and its duration + NEEDED duration is before the start of next
                        foreach (DateBlock d in DateBlock.getIntervals(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration), sortedAppointments[i].DateAndTime))
                        {
                            result.Add(d);
                        }
                    }
                }
            }
            //all appointments have been looked at, add all free spaces after the last
            Appointment lastAppointmentForTheDay = sortedAppointments[sortedAppointments.Count() - 1];
            DateTime end = date.AddHours(endHours);
            DateTime start = lastAppointmentForTheDay.DateAndTime.AddMinutes(lastAppointmentForTheDay.Duration);
            foreach(DateBlock d in DateBlock.getIntervals(start,end))
            {
                result.Add(d);
            }
            return result;
        }
    }
    public List<DateBlock> GetFreeTimeForPatient(DateTime date, int duration, Patient patient, int startHours, int endHours)
    {
        List<DateBlock> result = new List<DateBlock>();
        List<Appointment> appointments = new List<Appointment>();
        if (patient.GetPatientType() == PatientType.Registered)
        {
            appointments = GetAppointmentsByPatientIdForDate(((RegisteredPatient)patient).PersonalId, date.Date);
        }
        else 
        {
            appointments = GetAppointmentsByPatientIdForDate(((GuestPatient)patient).PersonalId, date.Date);
        }
         
         {
             if (appointments.Count == 0)
             {
                 //he has no appointmets let him choose anyhour

                 result = DateBlock.getIntervals(date.AddHours(startHours), date.AddHours(endHours));
                 return result;
             }

             List<Appointment> sortedAppointments = appointments.OrderBy(o => o.DateAndTime).ToList();

             for (int i = 0; i < sortedAppointments.Count(); i++)
             {
                 if (i == 0)
                 {
                     //is there an interval before 1st and after the start of working hours

                     if ((date.Date.AddHours(startHours).AddMinutes(duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                     {
                         foreach (DateBlock d in DateBlock.getIntervals(date.Date.AddHours(startHours), sortedAppointments[i].DateAndTime))
                         {
                             result.Add(d);
                         }
                     }
                 }
                 else
                 {

                     if ((sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration + duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                     {
                         //last and its duration + NEEDED duration is before the start of next
                         foreach (DateBlock d in DateBlock.getIntervals(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration), sortedAppointments[i].DateAndTime))
                         {
                             result.Add(d);
                         }
                     }
                 }
             }
             //all appointments have been looked at, add all free spaces after the last
             Appointment lastAppointmentForTheDay = sortedAppointments[sortedAppointments.Count() - 1];
             DateTime end = date.AddHours(endHours);
             DateTime start = lastAppointmentForTheDay.DateAndTime.AddMinutes(lastAppointmentForTheDay.Duration);
             foreach (DateBlock d in DateBlock.getIntervals(start, end))
             {
                 result.Add(d);
             }
             return result;
         }
       
    }


    public void CreateAppointment(Appointment appointment)
    {
        List<Appointment> appointments = this.AppointmentRepository.GetAll();
        int newAppointmentId = appointments.Count > 0 ? appointments.Last().AppointmentId + 1 : 1;
        appointment.AppointmentId = newAppointmentId;

        this.appointmentRepository.CreateAppointment(appointment);
    }

    public void DeleteAppointment(int id)
    {
        Appointment appointment = appointmentRepository.GetAppointmentById(id);
        this.appointmentRepository.DeleteAppointment(appointment);
    }
    public void PatientDeleteAppointment(int id, String patientId)
    {
        if (actionLogService.IsUserBannable(patientId))
        {
            registeredPatientRepository.Ban(registeredPatientRepository.GetById(patientId));
            throw new Exception("Ban");
        }
        else
        {
            DeleteAppointment(id);
            actionLogService.AddLog(DateTime.Now, "Remove Appointment", registeredPatientRepository.GetById(patientId));
        }
    }
    public void EditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        Appointment appointment = this.appointmentRepository.GetAppointmentById(appointmentId); 

        Doctor doc = doctorRepository.GetById(doctorId);
        Room room = roomRepository.GetById(roomId);
        Patient pat = patientRepository.GetById(patientId);

        List<DateBlock> t = DateBlock.getIntersection(GetFreeTimeForDoctor(dateAndTime.Date, duration, doc, 8, 20), GetFreeTimeForPatient(dateAndTime.Date, duration, pat, 8, 20));

        foreach (DateBlock block in t) 
        {
            if (block.Start.TimeOfDay.Equals(dateAndTime.TimeOfDay) || dateAndTime.TimeOfDay.Equals(appointment.DateAndTime.TimeOfDay))
            {
                appointment.DateAndTime = dateAndTime;
                appointment.Emergency = emergency;
                appointment.Patient = pat;
                appointment.Doctor = doc;
                appointment.Room = room;

                this.appointmentRepository.EditAppointment(appointment);
                return;
            }
        }
    }
    public void PatientEditAppointment(int appointmentId, String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
    {
        if (actionLogService.IsUserBannable(patientId))
        {
            registeredPatientRepository.Ban(registeredPatientRepository.GetById(patientId));
            throw new Exception("Ban");
        }
        else
        {
            EditAppointment(appointmentId, doctorId, patientId, dateAndTime, emergency, type, roomId, duration);
            actionLogService.AddLog(DateTime.Now, "Edit Appointment", registeredPatientRepository.GetById(patientId));
        }
    }
    public void LogAppointment(Appointment appointment, String diagnoses, String doctorsNote)
    {
        appointment.Diagnoses = diagnoses;
        appointment.DoctorsNotes = doctorsNote;
        appointment.Over = true;
        this.appointmentRepository.LogAppointment(appointment);
    }
    public void AddGrading(int appointmentId, int[] grades)
    {
        this.appointmentRepository.AddGrading(this.appointmentRepository.GetAppointmentById(appointmentId), grades);
    }
    public List<Appointment> GetPatientsPastAppointments(RegisteredPatient patient)
    {
        return this.appointmentRepository.GetPatientsPastAppointments(patient);
    }

    public List<Appointment> GetPatientsUpcomingAppointments(RegisteredPatient patient)
    {
        return this.appointmentRepository.GetPatientsUpcomingAppointments(patient);
    }


    public List<Appointment> GetAppointmentsByRoom(String roomId)
    {
       Room room = RoomRepository.GetById(roomId);
       return this.appointmentRepository.GetAppointmentsByRoom(room);
    }
}
