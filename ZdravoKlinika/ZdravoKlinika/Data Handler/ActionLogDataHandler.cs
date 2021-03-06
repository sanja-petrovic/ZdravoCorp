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
    internal class ActionLogDataHandler
    {
        private static String fileName = "action_logs.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public static string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<ActionLog> actionLogList)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new RegisteredPatientConverter());
            var json = JsonSerializer.Serialize(actionLogList, options);
            File.WriteAllText(fileLocation, json);
        }

        public List<ActionLog> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new RegisteredPatientConverter());
            return JsonSerializer.Deserialize<List<ActionLog>>(File.ReadAllText(fileLocation), options);
        }
    }
}
