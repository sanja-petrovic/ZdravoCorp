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
    public class PatientNotesDataHandler
    {
        private static String fileName = "patient_notes.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
        public string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<PatientNotes> notes)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new RegisteredUserConverter());
            var json = JsonSerializer.Serialize(notes, options);
            File.WriteAllText(fileLocation, json);
        }
        public List<PatientNotes> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new RegisteredUserConverter());
            return JsonSerializer.Deserialize<List<PatientNotes>>(File.ReadAllText(fileLocation), options);
        }
    }
}
