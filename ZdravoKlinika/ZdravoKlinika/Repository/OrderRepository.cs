using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class OrderRepository : Interfaces.IOrderRepository
    {
        private List<Order> orders;
        private OrderDataHandler orderDataHandler;
        private EquipmentRepository equipmentRepository;

        public List<Order> Orders { get => orders; set => orders = value; }
        public OrderDataHandler OrderDataHandler { get => orderDataHandler; set => orderDataHandler = value; }
        public EquipmentRepository EquipmentRepository { get => equipmentRepository; set => equipmentRepository = value; }

        public OrderRepository()
        {
            EquipmentRepository = new EquipmentRepository();

            OrderDataHandler = new OrderDataHandler();
            ReadDataFromFile();
        }

        private void ReadDataFromFile()
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

        public List<Order> GetAll()
        {
            ReadDataFromFile();
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

        public void Add(Order newOrder)
        {
            Orders.Add(newOrder);
            OrderDataHandler.Write(Orders);
        }

        public void Remove(Order item)
        {
            Orders.Remove(item);
            OrderDataHandler.Write(Orders);
        }

        public void Update(Order item)
        {
            int index = GetIndex(item.OrderId);
            if (index != -1)
            {
                orders[index] = item;
                orderDataHandler.Write(orders);
            }
        }

        private int GetIndex(String id)
        {
            int indexToRemove = -1;
            foreach (Order order in orders)
            {
                if (order.OrderId.Equals(id))
                {
                    indexToRemove = orders.IndexOf(order);
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("Order does not exist");
            }
            return indexToRemove;
        }

        public void RemoveAll()
        {
            if (Orders != null)
                Orders.Clear();
            OrderDataHandler.Write(Orders);
        }
    }
}
