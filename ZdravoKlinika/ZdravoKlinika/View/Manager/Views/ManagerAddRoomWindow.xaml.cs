using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZdravoKlinika.View.Manager.Views
{
    /// <summary>
    /// Interaction logic for ManagerAddRoomWindow.xaml
    /// </summary>
    public partial class ManagerAddRoomWindow : Window, INotifyPropertyChanged
    {
        private RoomController roomController;
        private int levelField;
        private int numberField;

        public ManagerAddRoomWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = new RoomController();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int LevelField
        {
            get
            {
                return levelField;
            }
            set
            {
                if (value != levelField)
                {
                    levelField = value;
                    OnPropertyChanged("LevelField");
                }
            }
        }

        public int NumberField
        {
            get
            {
                return numberField;
            }
            set
            {
                if (value != numberField)
                {
                    numberField = value;
                    OnPropertyChanged("NumberField");
                }
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
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
            this.roomController.CreateRoom(NazivTextBox.Text, tip, status, Int32.Parse(SpratTextBox.Text), Int32.Parse(BrojTextBox.Text), true);
        }

        private void SpratTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int parsedValue1;
            int parsedValue2;
            if (!int.TryParse(SpratTextBox.Text, out parsedValue1) || !int.TryParse(BrojTextBox.Text, out parsedValue2))
            {
                AddButton.IsEnabled = false;
            } else
            {
                AddButton.IsEnabled = true;
            }
        }

        private void BrojTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int parsedValue1;
            int parsedValue2;
            if (!int.TryParse(SpratTextBox.Text, out parsedValue1) || !int.TryParse(BrojTextBox.Text, out parsedValue2))
            {
                AddButton.IsEnabled = false;
            }
            else
            {
                AddButton.IsEnabled = true;
            }
        }
    }
}
