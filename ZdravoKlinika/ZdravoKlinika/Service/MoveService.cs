
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

    public void CreateMove(Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        this.SourceRoom = sourceRoom;
        this.DestinationRoom = destinationRoom;
        this.EquipmentToMove = equipmentToMove;
        this.ScheduledDateTime = scheduledDateTime;

        TimeSpan fireInterval = scheduledDateTime - DateTime.Now;
        timer.Interval = fireInterval.TotalMilliseconds;
        timer.Elapsed += ExecuteMove;
        timer.AutoReset = false;
        timer.Start();

        Move m = new Move(GenerateId().ToString(), sourceRoom, destinationRoom, scheduledDateTime, equipmentToMove);
        this.moveRepository.CreateMove(m);
    }

    private void ExecuteMove(object? sender, ElapsedEventArgs e)
    {
        RoomService roomService = new RoomService();
        List<Room> rooms = roomService.GetAll();

       /* 
        * 1.NADJI SOURCE ROOM
        2.KOPIRAJ EQUIPMENT LISTU SOURCE ROOM
        3.UZMI LISTU EQUIPMENT TO MOVE
        4.PRODJI KROZ LISTU EQUIPMENT TO MOVE I ODUZMI KOLICINU AMOUNT IZ LISTE EQUIPMENT
        5. ?
       */

        /*//ITERIRAJ KROZ SVE SOBE 
        foreach(Room r in rooms)
        {
            //NADJI SOURCE ROOM
            if (r.RoomId.Equals(this.SourceRoom.RoomId))
            {
                List<Equipment> equipmentHolder = new List<Equipment>();
                //NASAO SI SOURCE ROOM r, PRODJI KROZ SAV EQUIPMENT U TOJ SOBI
                foreach(Equipment equipment in r.EquipmentInRoom)
                {
                    foreach(Equipment equ in this.EquipmentToMove)
                    {
                        if (equipment.Id.Equals(equ.Id))
                        {
                            equipment.Amount = equipment.Amount - equ.Amount;     
                        }
                        
                    }
                    equipmentHolder.Add(equipment);
                }
                                
                roomService.UpdateRoom(r.RoomId, r.Name, r.Type, r.Status, r.Level, r.Number, r.Free, equipmentHolder);
            }
        }*/

        timer.Stop();
        timer.Dispose();
    }

    public void UpdateMove(String moveId, Room sourceRoom, Room destinationRoom, List<Equipment> equipmentToMove, DateTime scheduledDateTime)
    {
        Move m = this.moveRepository.GetById(moveId);
        m.SourceRoom = sourceRoom;
        m.DestinationRoom = destinationRoom;
        m.ScheduledDateTime = scheduledDateTime;
        m.EquipmentToMove = equipmentToMove;
        this.moveRepository.UpdateMove(m);
    }

    public void DeleteMove(String moveId)
    {
        Move m = moveRepository.GetById(moveId);
        this.moveRepository.DeleteMove(m);
    }

    public int GenerateId()
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

}