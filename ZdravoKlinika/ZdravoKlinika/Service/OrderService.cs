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
            return OrderRepository.GetAllOrders();
        }

        public Order? GetById(String orderId)
        {
            return OrderRepository.GetById(orderId);
        }

        public void CreateNewOrder(List<Equipment> equipmentToOrder)
        {
            bool orderIsUnique = false;
            String newId = "";
            while (!orderIsUnique)
            { 
                List<Order> orders = orderRepository.GetAllOrders();
                newId = IdGenerator();
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
            Order orderToAdd = new Order(newId);
            orderToAdd.EquipmentToOrder = equipmentToOrder;

            orderRepository.CreateNewOrder(orderToAdd);
            return;
        }

        private String IdGenerator() 
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

    }
}
