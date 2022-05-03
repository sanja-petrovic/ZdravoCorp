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
    internal class PatientMedicationNotificationDataHandler
    {
        private static String fileName = "patient_notification.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<PatientMedicationNotification> notifications)
        {

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new PrescriptionConverter());
            options.Converters.Add(new RegisteredUserConverter());
            var json = JsonSerializer.Serialize(notifications, options);
            File.WriteAllText(fileLocation, json);
        }

        public List<PatientMedicationNotification> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new PrescriptionConverter());
            options.Converters.Add(new RegisteredUserConverter());
            return JsonSerializer.Deserialize<List<PatientMedicationNotification>>(File.ReadAllText(fileLocation), options);
        }
    }
}
