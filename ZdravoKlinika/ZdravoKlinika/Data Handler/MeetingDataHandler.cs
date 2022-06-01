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
    public class MeetingDataHandler
    {
        private static String fileName = "meetings.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public static string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<Meeting> meetings)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new RegisteredUserConverter());
            var json = JsonSerializer.Serialize(meetings, options);
            File.WriteAllText(fileLocation, json);
        }

        public List<Meeting> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new RegisteredUserConverter());
            return JsonSerializer.Deserialize<List<Meeting>>(File.ReadAllText(fileLocation), options);
        }
    }
}
