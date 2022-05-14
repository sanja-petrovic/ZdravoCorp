using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Util;

public class AppointmentService
{
    private AppointmentRepository appointmentRepository;
    private DoctorRepository doctorRepository;
    private PatientRepository patientRepository;
    private RoomRepository roomRepository;
    private ZdravoKlinika.Util.ListHelper listHelper;
    private ZdravoKlinika.Util.DateBlock dateBlock = new ZdravoKlinika.Util.DateBlock();

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
    public List<Doctor> getFreeDoctorsForTime(DateBlock block, int startHours, int endHours)
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


        }
        return result;
    }

    //TODO these 2 need code cleanup, they can become one-ish ? 
    public List<DateBlock> getFreeTimeForDoctor(DateTime date,int duration, Doctor doctor, int startHours, int endHours)
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
                        foreach (DateBlock d in DateBlock.getIntervals(date.Date.AddHours(startHours), sortedAppointments[i].DateAndTime.AddMinutes(-duration)))
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
                        foreach (DateBlock d in DateBlock.getIntervals(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration), sortedAppointments[i].DateAndTime.AddMinutes(-duration)))
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
    public List<DateBlock> getFreeTimeForPatient(DateTime date, int duration, Patient patient, int startHours, int endHours)
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
                         foreach (DateBlock d in DateBlock.getIntervals(date.Date.AddHours(startHours), sortedAppointments[i].DateAndTime.AddMinutes(-duration)))
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
                         foreach (DateBlock d in DateBlock.getIntervals(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration), sortedAppointments[i].DateAndTime.AddMinutes(-duration)))
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
        appointment.Prescriptions = new List<Medication>();
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

        List<DateBlock> t = DateBlock.getIntersection(getFreeTimeForDoctor(dateAndTime.Date, duration, doc, 8, 20), getFreeTimeForPatient(dateAndTime.Date, duration, pat, 8, 20));

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
    public void LogAppointment(Appointment appointment, String diagnoses, String doctorsNote)
    {
        appointment.Diagnoses = diagnoses;
        appointment.DoctorsNotes = doctorsNote;
        appointment.Over = true;
        this.appointmentRepository.LogAppointment(appointment);
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
