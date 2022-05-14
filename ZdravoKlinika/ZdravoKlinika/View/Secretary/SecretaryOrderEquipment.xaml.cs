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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryOrderEquipment.xaml
    /// </summary>
    public partial class SecretaryOrderEquipment : Page
    {
        List<Equipment> equipmentInOrder;
        EquipmentController equipmentController;
        OrderController orderController;

        public SecretaryOrderEquipment()
        {
            InitializeComponent();
            equipmentInOrder = new List<Equipment>();

            equipmentController = new EquipmentController();
            orderController = new OrderController();

            List<Equipment> eq =  equipmentController.GetAll();

            ComboBoxEquipment.ItemsSource = eq;
            ComboBoxEquipment.SelectedIndex = 0;

            DataGridStorageEquipment.ItemsSource = eq;

            
        }

        private void OrderEquipment(object sender, RoutedEventArgs e)
        {
            if (equipmentInOrder.Count > 0)
            {
                orderController.CreateNewOrder(equipmentInOrder);
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new SecretaryOrderEquipment());
            }
        }
        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            foreach (Equipment equipment in equipmentInOrder)
            {
                if (equipment.Id == ComboBoxEquipment.SelectedValue.ToString())
                {
                    equipment.Amount += Int32.Parse(TextBoxAmount.Text);
                    RefreshGrid();
                    return;
                }
            }

            Equipment eq = new Equipment();
            eq.Id = ComboBoxEquipment.SelectedValue.ToString();
            eq.Amount = Int32.Parse(TextBoxAmount.Text);
            eq.Name = ComboBoxEquipment.Text;
            equipmentInOrder.Add(eq);
            RefreshGrid();

        }

        private void RefreshGrid()
        {
            DataGridOrderEquipment.ItemsSource = null;
            DataGridOrderEquipment.ItemsSource = equipmentInOrder;
        }

        private void RemoveFromOrder(object sender, RoutedEventArgs e)
        {
            Equipment eq = (Equipment)DataGridOrderEquipment.SelectedItem;
            if (eq != null)
            { 
                equipmentInOrder.Remove(eq);
                RefreshGrid();
            }
        }

        private void DataGridStorageEquipment_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxEquipment.SelectedIndex = DataGridStorageEquipment.SelectedIndex;
        }
    }
}
