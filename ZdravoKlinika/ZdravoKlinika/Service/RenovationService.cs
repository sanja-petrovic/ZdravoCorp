﻿
using System;
using System.Collections.Generic;
using System.Timers;

public class RenovationService
{
    private RenovationRepository renovationRepository;
    private List<Room> rooms;
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
        Renovation r = new Renovation(newRenovationId.ToString(),numberOfExitRooms, scheduledDateTime, entryRooms);
        this.renovationRepository.CreateRenovation(r);

        RoomService roomService = new RoomService();
        rooms = roomService.GetAll();



        //OVDE LOGIKA ZA SCHEDULE DA SE PROVERI!!!


        if (entryRooms.Count == 1 && numberOfExitRooms == 1)
        {
            //U PITANJU JE OBICNO RENOVIRANJE
            RenovateTheRoom();
            
        } else if (entryRooms.Count == 1 && numberOfExitRooms > 1)
        {
            //U PITANJU JE DELJENJE PROSTORIJE NA VISE MANJIH
            SplitTheRoom();

        } else if (entryRooms.Count > 1 && numberOfExitRooms == 1)
        {
            //U PITANJU JE SPAJANJE PROSTORIJA U JEDNU
            MergeTheRooms();

        } 
    }

    public void UpdateRenovation(String id, List<Room> entryRooms, int numberOfExitRooms, DateTime scheduledDateTime)
    {
        Renovation r = this.renovationRepository.GetById(id);
        r.EntryRooms = entryRooms;
        r.NumberOfExitRooms = numberOfExitRooms;
        r.ScheduledDateTime = scheduledDateTime;
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
        List<Renovation> renovations = this.GetAll();
        Room soba = null;
        foreach (Room r in rooms)
        {
            if (r.RoomId.Equals(entryRooms[0].RoomId))
            {
                roomService.FreeRoom(r.RoomId);
                soba = roomService.GetById(r.RoomId);
            }
        }

        timerRenovation.Stop();
        timerRenovation.Dispose();

        /*if(soba != null)
            foreach(Renovation reno in renovations)
            {
                if (reno.EntryRooms.Contains(soba))
                {
                    this.DeleteRenovation(reno.Id);
                }
            }*/
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
                    //roomService.UpdateRoom(r.RoomId, r.Name, r.Type, RoomStatus.renovation, r.Level, r.Number, r.Free);
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
}