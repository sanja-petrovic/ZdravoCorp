using JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Data_Handler
{
    public class OrderDataHandler
    {
        private static String fileName = "orders.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
        public void Write(List<Order> orders)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new EquipmentConverter());
            options.WriteIndented = true;
            var jsonList = JsonSerializer.Serialize(orders, options);
            File.WriteAllText(fileLocation, jsonList);
        }

        public List<Order> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new EquipmentConverter());
            return JsonSerializer.Deserialize<List<Order>>(File.ReadAllText(fileLocation), options);
        }
    }
}
