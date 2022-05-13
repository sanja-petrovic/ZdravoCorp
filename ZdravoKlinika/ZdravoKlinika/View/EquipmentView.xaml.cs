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
    public partial class EquipmentView : Window
    {

        private EquipmentController equipmentController;
        public ObservableCollection<Equipment> Equipment { get; set; }

        public EquipmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.equipmentController = new EquipmentController();
            this.Equipment = new ObservableCollection<Equipment>(this.equipmentController.GetAll());
            dataGridEquipment.ItemsSource = this.Equipment;
        }

        private void dataGridEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Refresh_Display()
        {
            dataGridEquipment.ItemsSource = null;
            dataGridEquipment.ItemsSource = this.equipmentController.GetAll();
        }

        private void MoveEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentMoveView equipmentMoveView = new EquipmentMoveView();
            equipmentMoveView.Show();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String query = searchTextBox.Text;
            ObservableCollection<Equipment> filteredEquipment = new ObservableCollection<Equipment>();
            foreach(Equipment eq in Equipment)
            {
                if (eq.Name.ToLower().Contains(query))
                {
                    filteredEquipment.Add(eq);
                }
            }
            dataGridEquipment.ItemsSource = filteredEquipment;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int query = filteriComboBox.SelectedIndex;
            ObservableCollection<Equipment> expendableGoods = new ObservableCollection<Equipment>(this.equipmentController.GetByExpendability(true));
            ObservableCollection<Equipment> inexpendableGoods = new ObservableCollection<Equipment>(this.equipmentController.GetByExpendability(false));
            ObservableCollection<Equipment> expiringGoods = new ObservableCollection<Equipment>();
            switch (query)
            {
                case 0:
                    dataGridEquipment.ItemsSource = expendableGoods;
                    break;
                case 1: 
                    dataGridEquipment.ItemsSource = inexpendableGoods;
                    break;
                case 2:
                    foreach (Equipment eq in expendableGoods)
                    {
                        if(eq.Amount < 50)
                        {
                            expiringGoods.Add(eq);
                        }
                    }
                    dataGridEquipment.ItemsSource = expiringGoods;
                    break;
                default:
                    break;
            }
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            dataGridEquipment.ItemsSource = Equipment;
            filteriComboBox.SelectedIndex = -1;
            searchTextBox.Text = "";
        }
    }
}
