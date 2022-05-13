using JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZdravoKlinika.Data_Handler
{
    public class CurrentUserDataHandler
    {
        private static String fileName = "current_user.json";
        private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
        public void Write(RegisteredUser user)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CurrentUserConverter());
            options.WriteIndented = true;
            if (user == null)
            {
                File.WriteAllText(fileLocation, string.Empty);
            }
            var jsonList = JsonSerializer.Serialize(user, options);
            File.WriteAllText(fileLocation, jsonList);
        }

        public RegisteredUser Read()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            var jsonString = File.ReadAllText(fileLocation);
            if ((!jsonString.Equals("[]")) && (!jsonString.Equals("")))
            {
                options.Converters.Add(new CurrentUserConverter());
                return JsonSerializer.Deserialize<RegisteredUser>(File.ReadAllText(fileLocation), options);
            }

            return null;
        }

        public void Clear()
        {
            File.WriteAllText(fileLocation, string.Empty);
        }
    }
}
