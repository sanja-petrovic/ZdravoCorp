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
    private RoomService roomService;

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

    public List<Appointment> GetAppointmentsByDoctorIdInSpecificTimeFrame(String id, DateTime start, DateTime finish)
    {
        List<Appointment> updatedAppointmentList = new List<Appointment>();
        foreach (Appointment appointment in GetAppointmentsByDoctorId(id))
        {
            if (appointment.DateAndTime.AddMinutes(appointment.Duration) > start && appointment.DateAndTime.AddMinutes(appointment.Duration) < finish)
            { 
                updatedAppointmentList.Add(appointment);
            }
        }
        return updatedAppointmentList;
    }

    public List<Appointment> GetAppointmentsByRoomIdInSpecificTimeFrame(string roomId, DateTime start, DateTime finish)
    {
        List<Appointment> updatedAppointmentList = new List<Appointment>();
        foreach (Appointment appointment in GetAppointmentsByRoom(roomId))
        {
            if (appointment.DateAndTime.AddMinutes(appointment.Duration) > start && appointment.DateAndTime.AddMinutes(appointment.Duration) < finish)
            {
                updatedAppointmentList.Add(appointment);
            }
        }
        return updatedAppointmentList;
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

    internal List<DateBlock> GetDateBlocksForDoctorInNextHour(int duration, Doctor doc)
    {
        List<DateBlock> freeBlocks = new List<DateBlock>();

        foreach (DateBlock dateBlock in GetFreeTimeForDoctor(DateTime.Now.Date, duration, doc, DateTime.Now.Hour, DateTime.Now.AddHours(1).Hour))
        {
            if (roomRepository.GetFreeRooms(dateBlock.Start, RoomType.operating).Count > 0)
            {
                freeBlocks.Add(dateBlock);
            }
        }

        return freeBlocks;
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
    public List<Doctor> GetFreeDoctorsBySpecialityForNextHour(int duration, String specialitty)
    { 
        List<Doctor> docs = new List<Doctor>();
        //docs = GetFreeDoctorsForTime(new DateBlock(DateTime.Now,duration),DateTime.Now.Hour,DateTime.Now.AddHours(1).Hour);
        DateTime dateNow = DateTime.Now.Date;
        dateNow = dateNow.AddHours(DateTime.Now.ToLocalTime().Hour);

        if (DateTime.Now.Minute < 15 && DateTime.Now.Minute > 0)
            dateNow = dateNow.AddMinutes(15);
        else if (DateTime.Now.Minute < 30)
            dateNow = dateNow.AddMinutes(30);
        else if (DateTime.Now.Minute < 45)
            dateNow = dateNow.AddMinutes(45);
        else
            dateNow = dateNow.AddHours(1);

        docs = GetFreeDoctorsForTime(new DateBlock(dateNow, duration),8,20);
        docs = PruneDoctorsBySpecialitty(docs, specialitty);
        if (docs.Count == 0)
            throw new Exception("1");

        docs = PruneDoctorsByFreeRooms(docs, duration);
        if (docs.Count == 0)
            throw new Exception("2");

        return docs;
    }
    private List<Doctor> PruneDoctorsByFreeRooms(List<Doctor> doctors, int duration) 
    {
        List<Doctor> prunedList = new List<Doctor>();

        foreach (Doctor doctor in doctors)
        {
            foreach (DateBlock dateBlock in GetFreeTimeForDoctor(DateTime.Now.Date, duration, doctor, DateTime.Now.Hour, DateTime.Now.AddHours(1).AddMinutes(duration).Hour))
            {
                if (roomRepository.GetFreeRooms(dateBlock.Start, RoomType.operating).Count > 0)
                {
                    prunedList.Add(doctor);
                    break;
                }
            }
        }
        return prunedList;
    }

    private List<Doctor> PruneDoctorsBySpecialitty(List<Doctor> doctors,String specialitty) 
    {
        List<Doctor> prunedList = new List<Doctor>();
        foreach (Doctor doctor in doctors)
        {
            if (doctor.Specialty.Equals(specialitty))
            {
                prunedList.Add(doctor);
            }
        }
        return prunedList;
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

    public void AddPatientNote(Appointment appointment, String note)
    {
        /*appointment.PatientNotes = note;
        EditAppointment(appointment);*/
    }

    public void CreateAppointment(Appointment appointment)
    {
        List<Appointment> appointments = this.AppointmentRepository.GetAll();
        int newAppointmentId = appointments.Count > 0 ? appointments.Last().AppointmentId + 1 : 1;
        appointment.AppointmentId = newAppointmentId;

        this.appointmentRepository.Add(appointment);
    }

    public void DeleteAppointment(int id)
    {
        Appointment appointment = appointmentRepository.GetAppointmentById(id);
        this.appointmentRepository.Delete(appointment);
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

                this.appointmentRepository.Update(appointment);
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
        this.appointmentRepository.Update(appointment);
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

    public int CountNumberOfGradesForDoctor(int questionNumber, int gradeToCount, Doctor doctor)
    {
        int count = 0;
        List<Appointment> doctorsAppointments = this.GetAppointmentsByDoctorId(doctor.PersonalId);

        foreach (Appointment app in doctorsAppointments)
        {
            if(app.Grading != null)
            {
                if (app.Grading[questionNumber] == gradeToCount)
                {
                    count++;
                }
            }          
        }
        return count;
    }

    public double GetAverageGradeForDoctor(int questionNumber, Doctor doctor)
    {
        double sum = 0;
        double count = 0;
        List<Appointment> doctorsAppointments = this.GetAppointmentsByDoctorId(doctor.PersonalId);

        foreach (Appointment app in doctorsAppointments)
        {
            if(app.Grading != null)
            {
                for (int i = 0; i < app.Grading.Length; i++)
                {
                    if (i == questionNumber)
                    {
                        sum += app.Grading[i];
                        count++;
                    }
                }
            }           
        }
        return sum / count;
    }

}
