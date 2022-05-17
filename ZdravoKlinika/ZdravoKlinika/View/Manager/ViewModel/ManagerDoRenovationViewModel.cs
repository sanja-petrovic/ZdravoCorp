using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerDoRenovationViewModel : ManagerViewModelBase
    {
        private RoomController roomController;
        private ObservableCollection<Room> rooms;

        public ObservableCollection<Room> Rooms { get => rooms; set => rooms = value; }

        public ManagerDoRenovationViewModel()
        {
            this.roomController = new RoomController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetRenovatableRooms());
        }
    }
}
