using System;
using System.Collections.Generic;
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
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace ZdravoKlinika.View
{
    public partial class RenovationView : Window
    {
        private RoomController roomController;
        private RenovationController renovationController;
        List<Room> entryList;
        int numberOfExitRooms = 0;
        public ObservableCollection<Room> Rooms { get; set; }
        public List<Room> EntryList { get => entryList; set => entryList = value; }

        public RenovationView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = new RoomController();
            this.renovationController = new RenovationController();
            Rooms = new ObservableCollection<Room>(this.roomController.GetRenovatableRooms());
            EntryList = new List<Room>();
            listBox.ItemsSource = Rooms;
            RenovateButton.IsEnabled = false;
            SplitButton.IsEnabled = false;
            MergeButton.IsEnabled = false;
            ExecuteButton.IsEnabled = false;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox.SelectedItems.Count == 0)
            {
                RenovateButton.IsEnabled = false;
                SplitButton.IsEnabled = false;
                MergeButton.IsEnabled = false;
                labelAmount.IsEnabled = false;
                textAmount.IsEnabled = false;
            } else if (listBox.SelectedItems.Count == 1)
            {
                RenovateButton.IsEnabled = true;
                SplitButton.IsEnabled = true;
                MergeButton.IsEnabled = false;
                labelAmount.IsEnabled = true;
                textAmount.IsEnabled = true;
            } else if (listBox.SelectedItems.Count > 1)
            {
                RenovateButton.IsEnabled = false;
                SplitButton.IsEnabled = false;
                MergeButton.IsEnabled = true;
                labelAmount.IsEnabled = false;
                textAmount.IsEnabled = false;
            }
        }

        private void RenovateButton_Click(object sender, RoutedEventArgs e)
        {
            EntryList.Clear();
            textArea.Text = "Prostorija " + listBox.SelectedItem.ToString() + " ce biti renovirana datuma " +
                dateTimePicker.Value.ToString() + ".";
            foreach (Room r in listBox.SelectedItems)
            {
                foreach(Room r1 in Rooms)
                {
                    if (r.Name.Equals(r1.Name)) EntryList.Add(r1);
                }
            }
            numberOfExitRooms = 1;
            ExecuteButton.IsEnabled = true;       
        }

        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            if (textAmount.Text != "") { 
                EntryList.Clear();
                textArea.Text = "Prostorija " + listBox.SelectedItem.ToString() + " ce biti podeljena na " + textAmount.Text + " prostorija/e datuma " +
                    dateTimePicker.Value.ToString() + ".";
                foreach (Room r in listBox.SelectedItems)
                {
                    foreach (Room r1 in Rooms)
                    {
                        if (r.Name.Equals(r1.Name)) EntryList.Add(r1);
                    }
                }
                numberOfExitRooms = Int32.Parse(textAmount.Text);
                ExecuteButton.IsEnabled = true;
            } else
            {
                textArea.Text = "Morate uneti zeljeni broj soba nakon deljenja.";
            }
        }

        private void MergeButton_Click(object sender, RoutedEventArgs e)
        {
            EntryList.Clear();
            textArea.Text = "Prostorije ";
            for (int i = 0; i < listBox.SelectedItems.Count; i++)
            {
                textArea.Text += listBox.SelectedItems[i].ToString() + ",";
            }
            textArea.Text = textArea.Text.Remove(textArea.Text.Length - 1);
            textArea.Text += " ce biti spojene datuma " +
                dateTimePicker.Value.ToString() + ".";
            foreach (Room r in listBox.SelectedItems)
            {
                foreach (Room r1 in Rooms)
                {
                    if (r.Name.Equals(r1.Name)) EntryList.Add(r1);
                }
            }
            numberOfExitRooms = 1;
            ExecuteButton.IsEnabled = true;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            this.renovationController.CreateRenovation(EntryList, numberOfExitRooms, (DateTime)dateTimePicker.Value);
            ExecuteButton.IsEnabled = false;
            textArea.Text = "";
        }
    }
}
