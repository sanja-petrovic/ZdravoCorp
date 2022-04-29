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
    private ZdravoKlinika.Util.ListHelper listHelper;

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
    public List<Doctor> getFreeDoctorsForTime(DateTime time,int duration)
    {
        List<Doctor> doctors = doctorRepository.GetAll();
        List<Doctor> result =  new List<Doctor>();
        foreach(Doctor doc in doctors)
        {
            List<Appointment> appointments = GetAppointmentsByDoctorIdForDate(doc.PersonalId, time.Date);
            if(appointments.Count == 0)
            {
                //doc has no appointmets for given day
                result.Add(doc);
                continue;
            }

            /*foreach(Appointment app in appointments)
            {
                if((time.Minute - app.DateAndTime.AddMinutes(app.Duration).Minute)>0)
                {
                    //start time is ok
                    if((time.AddMinutes(duration).Minute - listHelper.NextAppointment(appointments, app).DateAndTime.Minute) < 0)
                    {
                        //duration is ok
                        return doc;
                    }
                }
            }*/
            
        }
        return result;
    }

    //TODO these 2 need code cleanup, they can become one-ish ? 
    public List<DateTime> getFreeTimeForDoctor(DateTime date,int duration, Doctor doctor, int startHours, int endHours)
    {
        List<DateTime> result = new List<DateTime>();
        List<Appointment> appointments = GetAppointmentsByDoctorIdForDate(doctor.PersonalId, date.Date);
        {
            if(appointments.Count == 0)
            {
                //he has no appointmets let him choose anyhour
                
                DateTime startTime = date.AddHours(startHours);
                DateTime endTime = date.AddHours(endHours);
                for (int i = 0; i < (endTime.Hour - startTime.Hour); i++)
                {
                    result.Add(startTime.AddHours(i));
                }
                return result;
            }
            
            List<Appointment> sortedAppointments = appointments.OrderBy(o => o.DateAndTime).ToList();

            for(int i=0; i< sortedAppointments.Count(); i++)
            {
                if (i == 0)
                {
                    //is there an interval before 1st and after the start of working hours
                    
                    int help = 0;
                    while (true)
                    {
                        
                        if ((date.Date.AddHours(startHours).AddMinutes(duration + help) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                        {
                            result.Add(date.Date.AddHours(startHours).AddMinutes(help));
                            help += duration;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if((sortedAppointments[i-1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration + duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                    {
                        //last and its duration + NEEDED duration is before the start of next
                        result.Add(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration));
                    }
                }
            }
            //all appointments have been looked at, add all free spaces after the last
            Appointment lastAppointmentForTheDay = sortedAppointments[sortedAppointments.Count() - 1];
            DateTime end = date.AddHours(endHours);
            DateTime start = lastAppointmentForTheDay.DateAndTime.AddMinutes(lastAppointmentForTheDay.Duration);
            for (int i = 0; i < (end.Hour - start.Hour); i++)
            {
                result.Add(start.AddHours(i));
            }
            return result;
        }
    }
    public List<DateTime> getFreeTimeForPatient(DateTime date, int duration, RegisteredPatient patient, int startHours, int endHours)
    {
        List<DateTime> result = new List<DateTime>();
        List<Appointment> appointments = GetAppointmentsByPatientIdForDate(patient.PersonalId, date.Date);

        if (appointments.Count == 0)
        {
            //he has no appointmets let him choose any hour

            DateTime startTime = date.AddHours(startHours);
            DateTime endTime = date.AddHours(endHours);
            for (int i = 0; i < (endTime.Hour - startTime.Hour); i++)
            {
                result.Add(startTime.AddHours(i));
            }
            return result;
        }

        List<Appointment> sortedAppointments = appointments.OrderBy(o => o.DateAndTime).ToList();

        for (int i = 0; i < sortedAppointments.Count(); i++)
        {
            if (i == 0)
            {
                //is there an interval before 1st and after the start of working hours
                int help = 0;
                while (true)
                {

                    if ((date.Date.AddHours(startHours).AddMinutes(duration + help) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                    {
                        result.Add(date.Date.AddHours(startHours).AddMinutes(help));
                        help += duration;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                if ((sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration + duration) - sortedAppointments[i].DateAndTime).TotalMinutes < 0)
                {
                    //last and its duration + NEEDED duration is before the start of next
                    result.Add(sortedAppointments[i - 1].DateAndTime.AddMinutes(sortedAppointments[i - 1].Duration));
                }
            }
        }
        //all appointments have been looked at, add all free spaces after the last
        Appointment lastAppointmentForTheDay = sortedAppointments[sortedAppointments.Count() - 1];
        DateTime end = date.AddHours(endHours);
        DateTime start = lastAppointmentForTheDay.DateAndTime.AddMinutes(lastAppointmentForTheDay.Duration);
        for (int i = 0; i < (end.Hour - start.Hour); i++)
        {
            result.Add(start.AddHours(i));
        }
        return result;
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

}