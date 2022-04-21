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
    public class PatientDataHandler
    {
        private static String fileName = "all_patients.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
        public void Write(List<Patient> patients)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var jsonList = JsonSerializer.Serialize(patients, options);
            File.WriteAllText(fileLocation, jsonList);
        }

        public List<Patient> Read()
        {
            return JsonSerializer.Deserialize<List<Patient>>(File.ReadAllText(fileLocation));
        }
    }
}
