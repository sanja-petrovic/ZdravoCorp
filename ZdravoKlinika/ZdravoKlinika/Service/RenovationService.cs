
using System;
using System.Collections.Generic;
using System.Timers;

public class RenovationService
{
    private RenovationRepository renovationRepository;
    private RoomService roomService;
    private List<Room> rooms;
    private List<Appointment> appointments;
    private List<Renovation> renovations;
    private Timer timerRenovation;
    private Timer timerSplit;
    private Timer timerMerge;
    private bool roomFree;
    private List<Room> entryRooms;
    private int numberOfExitRooms;
    private DateTime scheduledDateTime;

    public RenovationService()
    {
        this.renovationRepository = new RenovationRepository();
        this.timerRenovation = new Timer();
        this.timerSplit = new Timer();
        this.timerMerge = new Timer();
        this.appointments = new List<Appointment>();
        this.renovations = new List<Renovation>();
        this.roomFree = true;
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

    public void CreateRenovation(Renovation renovation)
    {
        SaveRenovationValues(renovation);

        foreach (Room room in entryRooms)
        {
            CheckIfRoomIsFree(room);
        }

        if (roomFree)
        {
            this.renovationRepository.CreateRenovation(new Renovation(GenerateRenovationId().ToString(), numberOfExitRooms, scheduledDateTime, entryRooms, false));
            DetermineRenovationType();
            //TO-DO: OVDE UPDATE TABELU SOBA(?)

        }
        else
        {
            //TO-DO: OVDE NEKA NOTIFIKACIJA KAO "JEDNA OD ENTRYROOMS JE ZAUZETA U TOM INTERVALU"
        }
    }

    public void UpdateRenovation(Renovation changedRenovation)
    {
        Renovation renovation = this.renovationRepository.GetById(changedRenovation.Id);
        UpdateRenovationValues(renovation, changedRenovation);
        this.renovationRepository.UpdateRenovation(renovation);
    }

    public void DeleteRenovation(String id)
    {
        Renovation r = this.renovationRepository.GetById(id);
        this.renovationRepository.DeleteRenovation(r);
    }

    public void RenovateTheRoom()
    {
        FetchRooms();

        foreach (Room r in rooms)
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
        FetchRooms();

        foreach (Room r in rooms)
        {
            if (r.RoomId.Equals(entryRooms[0].RoomId))
            {
                roomService.FreeRoom(r.RoomId);
            }
        }

        FinishRenovation();

        timerRenovation.Stop();
        timerRenovation.Dispose();
    }

    public void SplitTheRoom()
    {
        FetchRooms();

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
        this.roomService = new RoomService();

        roomService.DeleteRoom(entryRooms[0].RoomId);
        for (int i = 0; i < numberOfExitRooms; i++)
        {
            roomService.CreateRoom(new Room("placeholder", "placeholder", RoomType.checkup, 1, 1, RoomStatus.available, true));
        }

        FinishRenovation();

        timerSplit.Stop();
        timerSplit.Dispose();
    }

    private void MergeTheRooms()
    {
        FetchRooms();

        foreach (Room r in rooms)
        {
            foreach (Room r1 in EntryRooms)
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
        this.roomService = new RoomService();

        foreach (Room r in EntryRooms)
        {
            roomService.DeleteRoom(r.RoomId);
        }
        roomService.CreateRoom(new Room("placeholder", "placeholder", RoomType.checkup, 1, 1, RoomStatus.available, true));

        FinishRenovation();

        timerMerge.Dispose();
        timerMerge.Dispose();
    }

    private int GenerateRenovationId()
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

    private void SaveRenovationValues(Renovation renovation)
    {
        this.EntryRooms = renovation.EntryRooms;
        this.NumberOfExitRooms = renovation.NumberOfExitRooms;
        this.ScheduledDateTime = renovation.ScheduledDateTime;
    }

    private void CheckIfRoomIsFree(Room room)
    {
        appointments.Clear();
        AppointmentService appointmentService = new AppointmentService();
        appointments = appointmentService.GetAppointmentsByRoom(room.RoomId);

        foreach (Appointment app in appointments)
        {
            CheckIfTimeIsInInterval(app);
        }
    }

    private void CheckIfTimeIsInInterval(Appointment app)
    {
        DateTime appointmentStart = app.DateAndTime;
        DateTime appointmentEnd = appointmentStart.AddMinutes(app.Duration);
        if ((ScheduledDateTime > appointmentStart) && (ScheduledDateTime < appointmentEnd))
        {
            roomFree = false;
        }
    }

    private void DetermineRenovationType()
    {
        if (entryRooms.Count == 1 && numberOfExitRooms == 1)
        {
            //REGULAR RENOVATION
            RenovateTheRoom();

        }
        else if (entryRooms.Count == 1 && numberOfExitRooms > 1)
        {
            //SPLITTING ONE ROOM INTO MULTIPLE
            SplitTheRoom();

        }
        else if (entryRooms.Count > 1 && numberOfExitRooms == 1)
        {
            //MERGE MULTIPLE ROOMS INTO ONE
            MergeTheRooms();

        }
    }

    private void UpdateRenovationValues(Renovation renovationToBeUpdated, Renovation updatingValues)
    {
        renovationToBeUpdated.EntryRooms = updatingValues.EntryRooms;
        renovationToBeUpdated.NumberOfExitRooms = updatingValues.NumberOfExitRooms;
        renovationToBeUpdated.ScheduledDateTime = updatingValues.ScheduledDateTime;
        renovationToBeUpdated.IsRenovationFinished = updatingValues.IsRenovationFinished;
    }

    private void FetchRooms()
    {
        this.roomService = new RoomService();
        this.rooms = roomService.GetAll();
    }

    //TO-DO: FIND BUG
    private void FinishRenovation()
    {
        foreach(Renovation r in this.renovations)
        {
            if (r.EntryRooms.Equals(EntryRooms) && r.NumberOfExitRooms.Equals(NumberOfExitRooms) && r.IsRenovationFinished == false)
            {
                UpdateRenovation(new Renovation(r.Id, r.NumberOfExitRooms, r.ScheduledDateTime, r.EntryRooms, true));
            }
        }
    }
}