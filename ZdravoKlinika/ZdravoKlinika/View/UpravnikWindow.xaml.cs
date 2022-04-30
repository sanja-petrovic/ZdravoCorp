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
    public partial class UpravnikWindow : Window
    {
        private int selectedIndex = -1;
        private DataGridRow selectedRow;
        private int colNum = 0;
        private RoomController roomController;
        private String roomId;
        private RoomType type;
        private String name;
        private int level;
        private int number;
        private RoomStatus status;
        public ObservableCollection<Room> Rooms { get; set; }
        public UpravnikWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = new RoomController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAll());

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            String naziv = NazivTextBox.Text;
            int sprat = Int32.Parse(SpratTextBox.Text);
            int broj = Int32.Parse(BrojTextBox.Text);
            RoomType tip = 0;
            switch (TipComboBox.SelectedIndex)
            {
                case 0:
                    tip = RoomType.operating;
                    break;
                case 1:
                    tip = RoomType.checkup;
                    break;
                case 2:
                    tip = RoomType.resting;
                    break;
                default:
                    break;
            }
            RoomStatus status = 0;
            switch (StatusComboBox.SelectedIndex)
            {
                case 0:
                    status = RoomStatus.occupied;
                    break;
                case 1:
                    status = RoomStatus.available;
                    break;
                case 2:
                    status = RoomStatus.renovation;
                    break;
                default:
                    break;
            }
            this.roomController.CreateRoom(naziv, tip, status, sprat, broj, true);
            Refresh_Display();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            String sifra = SifraTextBox.Text;
            String naziv = NazivTextBox.Text;
            int sprat = Int32.Parse(SpratTextBox.Text);
            int broj = Int32.Parse(BrojTextBox.Text);
            RoomType tip = 0;
            switch (TipComboBox.SelectedIndex)
            {
                case 0:
                    tip = RoomType.operating;
                    break;
                case 1:
                    tip = RoomType.checkup;
                    break;
                case 2:
                    tip = RoomType.resting;
                    break;
                default:
                    break;
            }
            RoomStatus status = 0;
            switch (StatusComboBox.SelectedIndex)
            {
                case 0:
                    status = RoomStatus.occupied;
                    break;
                case 1:
                    status = RoomStatus.available;
                    break;
                case 2:
                    status = RoomStatus.renovation;
                    break;
                default:
                    break;
            }
            this.roomController.UpdateRoom(sifra, naziv, tip, status, sprat, broj, true);
            Refresh_Display();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            roomController.DeleteRoom(SifraTextBox.Text);
            Refresh_Display();
        }

        private void Refresh_Display()
        {
            dataGridRooms.ItemsSource = null;
            dataGridRooms.ItemsSource = roomController.GetAll();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg == null) return;
            this.selectedIndex = dg.SelectedIndex;

            Room r = (Room)dg.SelectedItem;
            if (r != null)
            {
                switch (r.Type)
                {
                    case RoomType.checkup:
                        TipComboBox.SelectedIndex = 1;
                        break;
                    case RoomType.resting:
                        TipComboBox.SelectedIndex = 2;
                        break;
                    case RoomType.operating:
                        TipComboBox.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }

                switch(r.Status)
                {
                    case RoomStatus.occupied:
                        StatusComboBox.SelectedIndex = 0;
                        break;
                    case RoomStatus.available:
                        StatusComboBox.SelectedIndex = 1;
                        break;
                    case RoomStatus.renovation:
                        StatusComboBox.SelectedIndex = 2;
                        break;
                    default:
                        break;
                }
            }

        }
    }
}