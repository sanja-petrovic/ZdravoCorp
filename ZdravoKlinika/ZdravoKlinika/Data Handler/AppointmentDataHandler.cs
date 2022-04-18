using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class AppointmentDataHandler
{
    private static String fileName = "appointments.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<Appointment> appointments)
    {
        var jsonList = JsonSerializer.Serialize(appointments, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(fileLocation, jsonList);
    }

    public List<Appointment> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Appointment> appointments = new List<Appointment>();
        if (jsonString != "")
        {
            appointments = JsonSerializer.Deserialize<List<Appointment>>(jsonString);
        }

        return appointments;
        // return JsonSerializer.Deserialize<List<RegisteredPatient>>(File.ReadAllText(fileLocation),options);
    }

}