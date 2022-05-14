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
    internal class PrescriptionDataHandler
    {
        private static String fileName = "prescriptions.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public void Write(List<Prescription> prescriptions)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new MedicationConverter());
            options.Converters.Add(new DoctorConverter());
            options.Converters.Add(new PatientConverter());
            var json = JsonSerializer.Serialize(prescriptions, options);
            File.WriteAllText(fileLocation, json);

        }

        public List<Prescription> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new MedicationConverter());
            options.Converters.Add(new DoctorConverter());
            options.Converters.Add(new PatientConverter());
            return JsonSerializer.Deserialize<List<Prescription>>(File.ReadAllText(fileLocation), options);
        }
    }
}
