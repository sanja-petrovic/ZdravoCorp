using JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class AppointmentDataHandler
{
    private static String fileName = "appointments.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<Appointment> appointments)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.Converters.Add(new DoctorConverter());
        options.Converters.Add(new PatientConverter());
        options.Converters.Add(new RoomConverter());
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new PrescriptionConverter());
        var json = JsonSerializer.Serialize(appointments, options);
        File.WriteAllText(fileLocation, json);
    }

    public List<Appointment> Read()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new DoctorConverter());
        options.Converters.Add(new PatientConverter());
        options.Converters.Add(new RoomConverter());
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new PrescriptionConverter());
        return JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText(fileLocation), options);
    }

}