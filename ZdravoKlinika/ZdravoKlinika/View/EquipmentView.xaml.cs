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

    }
}
