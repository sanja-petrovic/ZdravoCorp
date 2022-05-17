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
using System.Linq;

namespace ZdravoKlinika.View
{
    public partial class EquipmentMoveView : Window
    {
        private MoveController moveController;
        private RoomController roomController;
        private EquipmentController equipmentController;

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }
        public ObservableCollection<Equipment> ReadyEquipment { get; set; }

        public EquipmentMoveView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.moveController = new MoveController();
            this.roomController = new RoomController();
            this.equipmentController = new EquipmentController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAll());
            Equipment = new ObservableCollection<Equipment>();
            ReadyEquipment = new ObservableCollection<Equipment>();
            sourceRoomListBox.ItemsSource = Rooms;
            destinationRoomListBox.ItemsSource = Rooms;
        }

        private void spremiButton_Click(object sender, RoutedEventArgs e)
        {
            Equipment eq = (Equipment) equipmentDataGrid.SelectedItem;
            int amount = Int32.Parse(textAmount.Text);
            
            if(amount > eq.Amount)
            {
                infoLabel.Content = "Ne mozete premestiti toliko opreme!";
            }
            else
            {
                infoLabel.Content = "";
                eq.Amount = amount;
                ReadyEquipment.Add(eq);
                readyEquipmentDataGrid.ItemsSource = ReadyEquipment;
            }
        }

        private void ukloniButton_Click(object sender, RoutedEventArgs e)
        {
            Equipment eq = (Equipment) readyEquipmentDataGrid.SelectedItem;
            ReadyEquipment.Remove(eq);
            readyEquipmentDataGrid.ItemsSource = ReadyEquipment;
        }

        private void premestiButton_Click(object sender, RoutedEventArgs e)
        {
            Room source = (Room)sourceRoomListBox.SelectedItem;
            Room destination = (Room)destinationRoomListBox.SelectedItem;
            DateTime scheduledDateTime = (DateTime)dateTimePicker.Value;
            List<Equipment> equipmentToMove = ReadyEquipment.ToList();

            if (!source.Equals(destination))
            {
                ResetData();
                this.moveController.CreateMove(source, destination, equipmentToMove, scheduledDateTime);
            }          
        }

        private void sourceRoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Equipment.Clear();
            Room r = (Room)sourceRoomListBox.SelectedItem;
            foreach (Equipment eq in r.EquipmentInRoom)
            {
                Equipment.Add(eq);
            }
            equipmentDataGrid.ItemsSource = Equipment;
        }

        private void destinationRoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room source = (Room)sourceRoomListBox.SelectedItem;
            Room destination = (Room)destinationRoomListBox.SelectedItem;

            if (source.Equals(destination))
            {
                infoLabel.Content = "Izvorna i odredisna prostorija su iste!";
            }
            else
            {
                infoLabel.Content = "";
            }
        }

        private void ResetData()
        {
            
        }

    }
}
