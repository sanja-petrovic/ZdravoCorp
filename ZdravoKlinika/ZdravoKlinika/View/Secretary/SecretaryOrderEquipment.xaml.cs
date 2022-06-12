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
using ZdravoKlinika.View.Secretary.SecretaryViewModel;

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
            DataContext = new OrderEquipmentViewModel();
            InitializeComponent();
            equipmentInOrder = new List<Equipment>();

            equipmentController = new EquipmentController();
            orderController = new OrderController();

            List<Equipment> eq =  equipmentController.GetByExpendability(true);

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
                if (equipment.Name.Equals(TextBoxName.Text))
                {
                    equipment.Amount += Int32.Parse(TextBoxAmount.Text);
                    RefreshGrid();
                    return;
                }
            }

            Equipment eq = new Equipment();
            
            foreach (Equipment equip in equipmentController.GetByExpendability(true))
            {
                if (equip.Name.Equals(TextBoxName.Text))
                {
                    eq.Id = equip.Id;
                    eq.Amount = Int32.Parse(TextBoxAmount.Text);
                    eq.Name = equip.Name;
                    break;
                }
            }

            if (eq.Id == null)
            {
                equipmentController.CreateEquipment(TextBoxName.Text,0,true);
                foreach (Equipment equip in equipmentController.GetByExpendability(true))
                {
                    if (equip.Name.Equals(TextBoxName.Text))
                    {
                        eq.Id = equip.Id;
                        eq.Amount = Int32.Parse(TextBoxAmount.Text);
                        eq.Name = equip.Name;
                        break;
                    }
                }
            }

            
            equipmentInOrder.Add(eq);
            RefreshGrid();
            RefreshGridAllEquiptment();

        }

        private void RefreshGrid()
        {
            DataGridOrderEquipment.ItemsSource = null;
            DataGridOrderEquipment.ItemsSource = equipmentInOrder;
        }

        private void RefreshGridAllEquiptment()
        {
            equipmentController = new EquipmentController();
            List<Equipment> eq = equipmentController.GetByExpendability(true);

            DataGridStorageEquipment.ItemsSource = null;
            DataGridStorageEquipment.ItemsSource = eq;
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
            if (DataGridStorageEquipment.SelectedIndex != -1)
            {
                TextBoxName.Text = ((Equipment)DataGridStorageEquipment.SelectedItem).Name;
                TextBoxAmount.Text = "";
            }
        }
    }
}
