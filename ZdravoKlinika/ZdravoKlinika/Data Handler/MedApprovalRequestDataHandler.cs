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
    public class MedApprovalRequestDataHandler
    {

        private static String fileName = "med_approval_requests.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public void Write(List<MedApprovalRequest> requests)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DoctorConverter());
            options.Converters.Add(new MedicationConverter());
            options.WriteIndented = true;
            var jsonList = JsonSerializer.Serialize(requests, options);
            File.WriteAllText(fileLocation, jsonList);
        }

        public List<MedApprovalRequest> Read()
        {
            string jsonString = File.ReadAllText(fileLocation);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DoctorConverter());
            options.Converters.Add(new MedicationConverter());
            return JsonSerializer.Deserialize<List<MedApprovalRequest>>(File.ReadAllText(fileLocation), options);
        }
    }
}
