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
            equipmentRepository = new EquipmentRepository();


            orderDataHandler = new OrderDataHandler();
            orders = OrderDataHandler.Read();
            if (orders == null) orders = new List<Order>();

            UpdateReferences();
        }

        private void UpdateReferences()
        {
            foreach (Order order in orders)
            {
                foreach (Equipment equipment in order.EquipmentToOrder)
                {
                    Equipment eq = EquipmentRepository.GetById(equipment.Id);
                    equipment.Expendable = eq.Expendable;
                    equipment.Name = eq.Name;
                    equipment.Amount = eq.Amount;
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            return orders;
        }

        public Order? GetById(String orderId)
        {
            foreach (var order in orders)
            {
                if (order.OrderId.Equals(orderId))
                {
                    return order;
                }
            }
            return null;
        }

        public void CreateNewOrder(Order newOrder)
        {
            orders.Add(newOrder);
            OrderDataHandler.Write(orders);
        }
    }
}
