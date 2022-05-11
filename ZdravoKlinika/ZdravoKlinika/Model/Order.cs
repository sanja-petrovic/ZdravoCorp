using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Order
    {
        private string orderId;
        private bool isOrderFinished;
        private DateTime creationDate;
        private List<Equipment> equipmentToOrder;

        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public List<Equipment> EquipmentToOrder { get => equipmentToOrder; set => equipmentToOrder = value; }
        public string OrderId { get => orderId; set => orderId = value; }
        public bool IsOrderFinished { get => isOrderFinished; set => isOrderFinished = value; }

        public Order(String orderId) 
        {
            IsOrderFinished = false;
            OrderId = orderId;
            CreationDate = DateTime.Now;
            EquipmentToOrder = new List<Equipment>();
        }

        public void AddEquipment(Equipment equipment)
        { 
            EquipmentToOrder.Add(equipment);
        }

        public void AddListEquipment(List<Equipment> equipments)
        { 
            EquipmentToOrder = equipments;
        }
    }
}
