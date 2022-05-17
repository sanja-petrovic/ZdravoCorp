using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonConverters;

public class RenovationDataHandler
{
    private static String fileName = "renovation.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

    public void Write(List<Renovation> renovationList)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.Converters.Add(new RoomConverter());
        var json = JsonSerializer.Serialize(renovationList, options);
        File.WriteAllText(fileLocation, json);
    }

    public List<Renovation> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Renovation> renovations = new List<Renovation>();

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new RoomConverter());

        if (jsonString != "")
        {
            renovations = JsonSerializer.Deserialize<List<Renovation>>(jsonString, options);
        }

        return renovations;
    }

}