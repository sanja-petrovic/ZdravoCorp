using JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Data_Handler
{
    public class EmployeeNotificationDataHandler
    {
        private static String fileName = "employee_notifications.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
        public void Write(List<EmployeeNotification> notifications)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new RegisteredUserConverter());
            options.WriteIndented = true;
            var jsonList = JsonSerializer.Serialize(notifications, options);
            File.WriteAllText(fileLocation, jsonList);
        }

        public List<EmployeeNotification> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new RegisteredUserConverter());
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<EmployeeNotification>>(File.ReadAllText(fileLocation), options);
        }
    }
}
