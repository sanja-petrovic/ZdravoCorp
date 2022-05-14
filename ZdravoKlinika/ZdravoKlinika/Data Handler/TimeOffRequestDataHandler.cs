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
    public class TimeOffRequestDataHandler
    {
        private static String fileName = "time_off_requests.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public void Write(List<TimeOffRequest> requests)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonConverters.DoctorConverter());
            var json = JsonSerializer.Serialize(requests, options);
            File.WriteAllText(fileLocation, json);

        }

        public List<TimeOffRequest> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonConverters.DoctorConverter());

            return JsonSerializer.Deserialize<List<TimeOffRequest>>(File.ReadAllText(fileLocation), options);
        }
    }
}
