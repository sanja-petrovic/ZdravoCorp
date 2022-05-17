using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerTransferEquipmentViewModel : ManagerViewModelBase
    {
        private RoomController roomController;
        public ObservableCollection<Room> rooms;

        public ObservableCollection<Room> Rooms { get; set; }

        public ManagerTransferEquipmentViewModel()
        {
            this.roomController = new RoomController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAll());
        }
    }
}
