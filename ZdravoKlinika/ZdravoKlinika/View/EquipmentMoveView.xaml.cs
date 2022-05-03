using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoKlinika.View
{
    public partial class EquipmentMoveView : Window
    {
        private MoveController moveController;
        private RoomController roomController;
        private EquipmentController equipmentController;

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }

        public EquipmentMoveView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.moveController = new MoveController();
            this.roomController = new RoomController();
            this.equipmentController = new EquipmentController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAll());
            Equipment = new ObservableCollection<Equipment>(this.equipmentController.GetAll());
            sourceRoomListBox.ItemsSource = Rooms;
            destinationRoomListBox.ItemsSource = Rooms;
            equipmentDataGrid.ItemsSource = Equipment;
        }

        private void spremiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void premestiButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
