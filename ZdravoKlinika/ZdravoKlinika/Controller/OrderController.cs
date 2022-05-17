using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class OrderController
    {
        private OrderService orderService;

        public OrderService OrderService { get => orderService; set => orderService = value; }

        public OrderController()
        { 
            orderService = new OrderService();
        }

        public List<Order> GetAllOrders()
        {
            return OrderService.GetAllOrders();
        }

        public Order? GetById(String orderId)
        {
            return OrderService.GetById(orderId);
        }

        public void CreateNewOrder(List<Equipment> equipmentToOrder)
        { 
            OrderService.CreateNewOrder(equipmentToOrder);
            return;
        }

    }
}
