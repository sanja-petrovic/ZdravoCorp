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
    internal class AppReviewDataHandler
    {
        private static String fileName = "reviews.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

        public static string FileLocation { get => fileLocation; set => fileLocation = value; }

        public void Write(List<AppReview> appReviewList)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new RegisteredPatientConverter());
            var json = JsonSerializer.Serialize(appReviewList, options);
            File.WriteAllText(fileLocation, json);
        }

        public List<AppReview> Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new RegisteredPatientConverter());
            return JsonSerializer.Deserialize<List<AppReview>>(File.ReadAllText(fileLocation), options);
        }
    }
}
