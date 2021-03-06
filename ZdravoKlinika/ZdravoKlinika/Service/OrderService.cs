using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class OrderService
    {
        private OrderRepository orderRepository;

        public OrderRepository OrderRepository { get => orderRepository; set => orderRepository = value; }

        public OrderService()
        { 
            orderRepository = new OrderRepository();
        }

        public List<Order> GetAllOrders()
        {
            return OrderRepository.GetAll();
        }

        public Order? GetById(String orderId)
        {
            return OrderRepository.GetById(orderId);
        }

        public void CreateNewOrder(List<Equipment> equipmentToOrder)
        {
            String newId = GetFreeId();
            Order orderToAdd = new Order(newId);
            orderToAdd.EquipmentToOrder = equipmentToOrder;

            orderRepository.Add(orderToAdd);
            return;
        }

        private String GetFreeId() 
        {
            bool orderIsUnique = false;
            String newId = "";
            while (!orderIsUnique)
            {
                List<Order> orders = orderRepository.GetAll();
                newId = Util.IdGenerator.Generate();
                orderIsUnique = true;
                foreach (Order order in orders)
                {
                    if (order.OrderId.Equals(newId))
                    {
                        orderIsUnique = false;
                        break;
                    }
                }
            }
            return newId;
        }
    }
}
