using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZdravoKlinika.Data_Handler
{
    internal class MedicationDataHandler
    {
        private static String fileName = "medications.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public void Write(List<Medication> medications)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new JsonConverters.DoctorConverter());
            var json = JsonSerializer.Serialize(medications, options);
            File.WriteAllText(fileLocation, json);

        }

        public List<Medication> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonConverters.DoctorConverter());

            return JsonSerializer.Deserialize<List<Medication>>(File.ReadAllText(fileLocation), options);
        }
    }
}
