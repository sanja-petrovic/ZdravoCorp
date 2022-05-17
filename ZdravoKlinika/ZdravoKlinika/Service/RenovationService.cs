
using System;
using System.Collections.Generic;
using System.Timers;

public class RenovationService
{
    private RenovationRepository renovationRepository;
    private List<Room> rooms;
    private List<Appointment> appointments;
    private Timer timerRenovation;
    private Timer timerSplit;
    private Timer timerMerge;
    List<Room> entryRooms; 
    int numberOfExitRooms; 
    DateTime scheduledDateTime;

    public RenovationService()
    {
        this.renovationRepository = new RenovationRepository();
        this.timerRenovation = new Timer();
        this.timerSplit = new Timer();
        this.timerMerge = new Timer();
        this.appointments = new List<Appointment>();
    }

    public RenovationRepository RenovationRepository { get => renovationRepository; set => renovationRepository = value; }
    public List<Room> EntryRooms { get => entryRooms; set => entryRooms = value; }
    public int NumberOfExitRooms { get => numberOfExitRooms; set => numberOfExitRooms = value; }
    public DateTime ScheduledDateTime { get => scheduledDateTime; set => scheduledDateTime = value; }

    public List<Renovation> GetAll()
    {
        return this.renovationRepository.GetAll();
    }

    public Renovation GetById(String id)
    {
        return this.renovationRepository.GetById(id);
    }

    public void CreateRenovation(List<Room> entryRooms, int numberOfExitRooms, DateTime scheduledDateTime)
    {
        //NA OSNOVU BROJA ENTRYROOMS I NUMBEROFEXITROOMS ODREDI KOJI JE TIP RENOVIRANJA
        this.EntryRooms = entryRooms;
        this.NumberOfExitRooms = numberOfExitRooms;
        this.ScheduledDateTime = scheduledDateTime;

        RoomService roomService = new RoomService();
        rooms = roomService.GetAll();

        AppointmentService appointmentService = new AppointmentService();
        bool ok = true;

        foreach(Room room in entryRooms)
        {
            appointments.Clear();
            appointments = appointmentService.GetAppointmentsByRoom(room.RoomId);
            foreach(Appointment app in appointments)
            {  
                DateTime appointmentStart = app.DateAndTime;
                DateTime appointmentEnd = appointmentStart.AddMinutes(app.Duration);
                if ((scheduledDateTime > appointmentStart) && (scheduledDateTime < appointmentEnd))
                {
                    //SOBA JE ZAUZETA U TOM TRENUTKU
                    ok = false;
                }
            }

        }

        if (ok)
        {
            Renovation r = new Renovation(GenerateId().ToString(), numberOfExitRooms, scheduledDateTime, entryRooms, false);
            this.renovationRepository.CreateRenovation(r);
            if (entryRooms.Count == 1 && numberOfExitRooms == 1)
            {
                //U PITANJU JE OBICNO RENOVIRANJE
                RenovateTheRoom();

            }
            else if (entryRooms.Count == 1 && numberOfExitRooms > 1)
            {
                //U PITANJU JE DELJENJE PROSTORIJE NA VISE MANJIH
                SplitTheRoom();

            }
            else if (entryRooms.Count > 1 && numberOfExitRooms == 1)
            {
                //U PITANJU JE SPAJANJE PROSTORIJA U JEDNU
                MergeTheRooms();

            }
            //OVDE UPDATE TABELU SOBA

        } else
        {
            //OVDE IDE NEKA NOTIFIKACIJA "SOBA JE ZAUZETA U UNETOM VREMENSKOM PERIODU"
        }
    }

    public void UpdateRenovation(String id, List<Room> entryRooms, int numberOfExitRooms, DateTime scheduledDateTime, bool isRenovationFinished)
    {
        Renovation r = this.renovationRepository.GetById(id);
        r.EntryRooms = entryRooms;
        r.NumberOfExitRooms = numberOfExitRooms;
        r.ScheduledDateTime = scheduledDateTime;
        r.IsRenovationFinished = isRenovationFinished;
        this.renovationRepository.UpdateRenovation(r);
    }

    public void DeleteRenovation(String id)
    {
        Renovation r = this.renovationRepository.GetById(id);
        this.renovationRepository.DeleteRenovation(r);
    }

    public void RenovateTheRoom()
    {
        RoomService roomService = new RoomService();
        List<Room> rooms = roomService.GetAll();

        foreach(Room r in rooms)
        {
            if (r.RoomId.Equals(entryRooms[0].RoomId))
            {
                roomService.RenovateRoom(r.RoomId);
            }
        }

        TimeSpan fireInterval = ScheduledDateTime - DateTime.Now;
        timerRenovation.Interval = fireInterval.TotalMilliseconds;
        timerRenovation.Elapsed += ExecuteRenovation;
        timerRenovation.AutoReset = false;
        timerRenovation.Start();
    }

    private void ExecuteRenovation(object? sender, ElapsedEventArgs e)
    {
        RoomService roomService = new RoomService();
        List<Room> rooms = roomService.GetAll();

        foreach (Room r in rooms)
        {
            if (r.RoomId.Equals(entryRooms[0].RoomId))
            {
                roomService.FreeRoom(r.RoomId);
            }
        }

        timerRenovation.Stop();
        timerRenovation.Dispose();
    }

    public void SplitTheRoom()
    {
        RoomService roomService = new RoomService();
        List<Room> rooms = roomService.GetAll();

        foreach (Room r in rooms)
        {
            if (r.RoomId.Equals(entryRooms[0].RoomId))
            {
                roomService.RenovateRoom(r.RoomId);
            }
        }

        TimeSpan fireInterval = ScheduledDateTime - DateTime.Now;
        timerSplit.Interval = fireInterval.TotalMilliseconds;
        timerSplit.Elapsed += ExecuteSplit;
        timerSplit.Start();
    }

    private void ExecuteSplit(object? sender, ElapsedEventArgs e)
    {
        RoomService roomService = new RoomService();

        roomService.DeleteRoom(entryRooms[0].RoomId);
        for(int i = 0; i < numberOfExitRooms; i++)
        {
            roomService.CreateRoom("placeholder", RoomType.checkup, RoomStatus.available, 1, 1, true);
        }

        timerSplit.Stop();
        timerSplit.Dispose();
    }

    private void MergeTheRooms()
    {
        RoomService roomService = new RoomService();
        List<Room> rooms = roomService.GetAll();

        foreach (Room r in rooms)
        {
            foreach(Room r1 in EntryRooms)
            {
                if (r1.RoomId.Equals(r.RoomId))
                {
                    roomService.RenovateRoom(r.RoomId);
                }
            }
        }

        TimeSpan fireInterval = ScheduledDateTime - DateTime.Now;
        timerMerge.Interval = fireInterval.TotalMilliseconds;
        timerMerge.Elapsed += ExecuteMerge;
        timerMerge.Start();
    }

    private void ExecuteMerge(object? sender, ElapsedEventArgs e)
    {
        RoomService roomService = new RoomService();

        foreach (Room r in EntryRooms)
        {
            roomService.DeleteRoom(r.RoomId);
        }
        roomService.CreateRoom("placeholder", RoomType.checkup, RoomStatus.available, 1, 1, true);

        timerMerge.Dispose();
        timerMerge.Dispose();
    }

    public int GenerateId()
    {
        List<Renovation> renovations = this.renovationRepository.GetAll();
        int newRenovationId;
        if (renovations.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach (Renovation ren in renovations)
            {
                trenutniId = Int32.Parse(ren.Id);
                if (trenutniId > maxId) maxId = trenutniId;
            }

            newRenovationId = maxId + 1;
        }
        else
        {
            newRenovationId = 1;
        }
        return newRenovationId;
    }
}