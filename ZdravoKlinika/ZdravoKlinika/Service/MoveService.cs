
using System;
using System.Collections.Generic;
using System.Timers;

public class MoveService
{
    private MoveRepository moveRepository;
    private Room sourceRoom;
    private Room destinationRoom;
    private DateTime scheduledDateTime;
    private List<Equipment> equipmentToMove;
    private Timer timer;
    private RoomService roomService;
    private List<Room> rooms;
    private List<string> idList;

    public MoveService()
    {
        this.moveRepository = new MoveRepository();
        this.timer = new Timer();
    }

    public Room SourceRoom { get => sourceRoom; set => sourceRoom = value; }
    public Room DestinationRoom { get => destinationRoom; set => destinationRoom = value; }
    public DateTime ScheduledDateTime { get => scheduledDateTime; set => scheduledDateTime = value; }
    public List<Equipment> EquipmentToMove { get => equipmentToMove; set => equipmentToMove = value; }

    public List<Move> GetAll()
    {
        return this.moveRepository.GetAll();
    }

    public Move GetById(String id)
    {
        return this.moveRepository.GetById(id);
    }

    public void CreateMove(Move move)
    {
        SaveMoveValues(move);
        
        TimeSpan fireInterval = scheduledDateTime - DateTime.Now;
        timer.Interval = fireInterval.TotalMilliseconds;
        timer.Elapsed += ExecuteMove;
        timer.AutoReset = false;
        timer.Start();

        this.moveRepository.CreateMove(new Move(GenerateMoveId().ToString(), SourceRoom, DestinationRoom, ScheduledDateTime, EquipmentToMove));
    }

    private void ExecuteMove(object? sender, ElapsedEventArgs e)
    {
        FetchRooms();
        
        foreach (Room r in this.rooms)
        {
            if (r.RoomId.Equals(this.SourceRoom.RoomId))
            {
                SubstractEquipmentFromSourceRoom(r);
            }

            if (r.RoomId.Equals(this.DestinationRoom.RoomId))
            {
                AddEquipmentToDestinationRoom(r);
            }
        }

        SaveChangesToRooms();
        DestroyTimer();
    }

    private void SubstractEquipmentFromSourceRoom(Room r)
    {
        foreach (Equipment equipment in r.EquipmentInRoom)
        {
            foreach (Equipment equ in this.EquipmentToMove)
            {
                if (equipment.Id.Equals(equ.Id))
                {
                    equipment.Amount = equipment.Amount - equ.Amount;
                }
            }
        }
    }

    private void AddEquipmentToDestinationRoom(Room r)
    {
        if (r.EquipmentInRoom.Count != 0)
        {          
            GetAllEquipmentInRoomIds(r);

            foreach (Equipment equ in this.EquipmentToMove)
            {
                AddEquipmentToNonEmptyRoom(r, equ);
            }
        }
        else
        {
            AddEquipmentToEmptyRoom(r);
        }
    }

    private void AddEquipmentToNonEmptyRoom(Room r, Equipment equ)
    {
        if (idList.Contains(equ.Id))
        {           
            AddAmountToExistingEquipmentInRoom(r, equ);
        }
        else
        {                         
            r.AddEquipmentInRoom(equ);
        }
    }

    private void AddAmountToExistingEquipmentInRoom(Room r, Equipment equ)
    {
        foreach (Equipment equipment in r.EquipmentInRoom)
        {
            if (equipment.Id.Equals(equ.Id))
            {
                equipment.Amount = equipment.Amount + equ.Amount;
            }
        }
    }

    private void AddEquipmentToEmptyRoom(Room r)
    {
        foreach (Equipment equipment in this.EquipmentToMove)
        {
            r.AddEquipmentInRoom(equipment);
        }
    }

    private void GetAllEquipmentInRoomIds(Room r)
    {
        idList = new List<string>();
        foreach (Equipment equipment in r.EquipmentInRoom)
        {
            idList.Add(equipment.Id);
        }
    }

    private void SaveChangesToRooms()
    {
        RoomDataHandler roomDataHandler = new RoomDataHandler();
        roomDataHandler.Write(this.rooms);
    }

    private void DestroyTimer()
    {
        timer.Stop();
        timer.Dispose();
    }

    public void UpdateMove(Move changedMove)
    {
        Move move = this.moveRepository.GetById(changedMove.MoveId);
        UpdateMoveValues(move, changedMove);
        this.moveRepository.UpdateMove(move);
    }

    public void DeleteMove(String moveId)
    {
        Move m = moveRepository.GetById(moveId);
        this.moveRepository.DeleteMove(m);
    }

    private int GenerateMoveId()
    {
        List<Move> moves = this.moveRepository.GetAll();
        int newMoveId;
        if (moves.Count > 0)
        {
            int maxId = 0;
            int trenutniId = 0;
            foreach (Move move in moves)
            {
                trenutniId = Int32.Parse(move.MoveId);
                if (trenutniId > maxId) maxId = trenutniId;
            }

            newMoveId = maxId + 1;
        }
        else
        {
            newMoveId = 1;
        }
        return newMoveId;
    }

    private void SaveMoveValues(Move move)
    {
        SourceRoom = move.SourceRoom;
        DestinationRoom = move.DestinationRoom;
        EquipmentToMove = move.EquipmentToMove;
        ScheduledDateTime = move.ScheduledDateTime;
    }

    private void FetchRooms()
    {
        this.roomService = new RoomService();
        this.rooms = roomService.GetAll();
    }

    private void UpdateMoveValues(Move moveToBeUpdated, Move updatedValues)
    {
        moveToBeUpdated.SourceRoom = updatedValues.SourceRoom;
        moveToBeUpdated.DestinationRoom = updatedValues.DestinationRoom;
        moveToBeUpdated.ScheduledDateTime = updatedValues.ScheduledDateTime;
        moveToBeUpdated.EquipmentToMove = updatedValues.EquipmentToMove;
    }
}