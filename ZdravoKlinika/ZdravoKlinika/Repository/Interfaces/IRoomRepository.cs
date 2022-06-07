using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IRoomRepository : IRepositoryBase<Room, String>
    {
        public List<Room> GetFreeRooms(DateTime enteredTime, RoomType roomType);

        public RoomType GetRoomTypeForAppointmentType(AppointmentType type);

        public List<Room> GetRenovatableRooms();

        public void OccupyRoom(Room room);

        public void FreeRoom(Room room);

        public void RenovateRoom(Room room);
    }
}
