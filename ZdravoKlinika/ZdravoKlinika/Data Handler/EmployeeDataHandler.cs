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
    public class EmployeeDataHandler
    {
        private static String fileName = "employees.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<Employee> patients)
        {


            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter());
            var json = JsonSerializer.Serialize(patients, options);
            File.WriteAllText(fileLocation, json);

        }

        public List<Employee> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(fileLocation), options);
        }
    }
}
