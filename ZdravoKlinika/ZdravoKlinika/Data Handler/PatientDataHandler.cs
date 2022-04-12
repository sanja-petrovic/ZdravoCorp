using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class PatientDataHandler
{
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources"+ Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "patients.json";

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<Patient> patients)
    {
    
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var json = JsonSerializer.Serialize(patients, options);
        File.WriteAllText(fileLocation, json);

    }

    public List<Patient> Read()
    {
        return JsonSerializer.Deserialize<List<Patient>>(File.ReadAllText(fileLocation));
    }

}