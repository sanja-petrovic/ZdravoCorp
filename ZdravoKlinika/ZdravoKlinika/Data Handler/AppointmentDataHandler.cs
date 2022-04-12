using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class AppointmentDataHandler
{
    private string fileLocation;

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public AppointmentDataHandler(string fileLocation)
    {
        this.fileLocation = fileLocation;
    }

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
    }

}