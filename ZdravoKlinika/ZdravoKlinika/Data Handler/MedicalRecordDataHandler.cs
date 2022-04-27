using JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZdravoKlinika.Data_Handler
{
    public class MedicalRecordDataHandler
    {
        private static String fileName = "medical_records.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public void Write(List<MedicalRecord> medicalRecord)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var json = JsonSerializer.Serialize(medicalRecord, options);
            File.WriteAllText(fileLocation, json);

        }

        public List<MedicalRecord> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            //options.Converters.Add(new MedicationConverter());
            return JsonSerializer.Deserialize<List<MedicalRecord>>(File.ReadAllText(fileLocation), options);
        }
    }
}
