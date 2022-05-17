using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class OrderRepository
    {
        private List<Order> orders;
        private OrderDataHandler orderDataHandler;
        private EquipmentRepository equipmentRepository;

        public List<Order> Orders { get => orders; set => orders = value; }
        public OrderDataHandler OrderDataHandler { get => orderDataHandler; set => orderDataHandler = value; }
        public EquipmentRepository EquipmentRepository { get => equipmentRepository; set => equipmentRepository = value; }

        public OrderRepository()
        {
            // let this first update finished orders 
            EquipmentRepository = new EquipmentRepository();

            OrderDataHandler = new OrderDataHandler();
            ReadDataFromFiles();
        }

        private void ReadDataFromFiles()
        {
            Orders = OrderDataHandler.Read();
            if (Orders == null) Orders = new List<Order>();
        }

        private void UpdateReferences(Order order)
        {
            foreach (Equipment equipment in order.EquipmentToOrder)
            {
                Equipment eq = EquipmentRepository.GetById(equipment.Id);
                equipment.Expendable = eq.Expendable;
                equipment.Name = eq.Name;
            }
        }

        public List<Order> GetAllOrders()
        {
            ReadDataFromFiles();
            foreach (Order order in Orders)
            {
                UpdateReferences(order);
            }
            return Orders;
        }

        public Order? GetById(String orderId)
        {
            Order? orderToReturn = null;
            foreach (var order in Orders)
            {
                if (order.OrderId.Equals(orderId))
                {
                    UpdateReferences(order);
                    orderToReturn = order;
                    break;
                }
            }
            return orderToReturn;
        }

        public void CreateNewOrder(Order newOrder)
        {
            Orders.Add(newOrder);
            OrderDataHandler.Write(Orders);
        }
    }
}
