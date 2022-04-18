using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PatientDataHandler
{
    private static String fileName = "doctors.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<RegisteredPatient> patients)
    {
        

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.Converters.Add(new MedicalRecordConverter());
        options.Converters.Add(new JsonStringEnumConverter());
        var json = JsonSerializer.Serialize(patients, options);
        File.WriteAllText(fileLocation, json);

    }

    public List<RegisteredPatient> Read()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new MedicalRecordConverter());
        options.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<List<RegisteredPatient>>(File.ReadAllText(fileLocation),options);
    }

}
