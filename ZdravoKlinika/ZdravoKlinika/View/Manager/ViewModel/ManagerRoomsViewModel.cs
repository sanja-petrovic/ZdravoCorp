using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.Manager.Views;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerRoomsViewModel : ManagerViewModelBase
    {
        private ManagerDoRenovationViewModel doRenovationViewModel = new ManagerDoRenovationViewModel();
        private ManagerCancelRenovationViewModel cancelRenovationViewModel = new ManagerCancelRenovationViewModel();
        private ManagerTransferEquipmentViewModel transferEquipmentViewModel = new ManagerTransferEquipmentViewModel();
        private ManagerViewModelBase currentViewModel;
        private RoomController roomController;
        private ObservableCollection<Room> rooms;
        public MyICommand AddCommand { get; private set; }
        public MyICommand EditCommand { get; private set; }
        public MyICommand DeleteCommand { get; private set; }

        public ObservableCollection<Room> Rooms { get => rooms; set => rooms = value; }

        public ManagerRoomsViewModel()
        {
            this.roomController = new RoomController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAll());
            AddCommand = new MyICommand(ExecuteAdd);
            EditCommand = new MyICommand(ExecuteEdit);
            DeleteCommand = new MyICommand(ExecuteDelete);
        }

        public ManagerViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public ManagerDoRenovationViewModel DoRenovationViewModel
        {
            get { return doRenovationViewModel; }
            set 
            {
                SetProperty(ref doRenovationViewModel, value);
            } 
        }

        public ManagerCancelRenovationViewModel CancelRenovationViewModel
        {
            get { return cancelRenovationViewModel; }
            set
            {
                SetProperty(ref cancelRenovationViewModel, value);
            }
        }

        public ManagerTransferEquipmentViewModel TransferEquipmentViewModel
        {
            get { return transferEquipmentViewModel; }
            set
            {
                SetProperty(ref transferEquipmentViewModel, value);
            }
        }

        private void ExecuteAdd()
        {
            ManagerAddRoomWindow window = new ManagerAddRoomWindow();
            window.Show();
        }

        private void ExecuteEdit()
        {
            ManagerEditRoomWindow window = new ManagerEditRoomWindow();
            window.Show();
        }

        private void ExecuteDelete()
        {

        }

    }
}
