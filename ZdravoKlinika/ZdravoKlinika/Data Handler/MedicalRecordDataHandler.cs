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
        private static String fileName = "medicalRecords.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<MedicalRecord> medicalRecord)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new MedicalRecordConverter());
            var json = JsonSerializer.Serialize(medicalRecord, options);
            File.WriteAllText(fileLocation, json);

        }
        // TODO

        public List<MedicalRecord> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new MedicalRecordConverter());
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<MedicalRecord>>(File.ReadAllText(fileLocation), options);
        }
    }
}
