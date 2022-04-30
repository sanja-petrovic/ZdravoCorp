using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class RoomDataHandler
{
    private String fileLocation;

    public String FileLocation { get => fileLocation; set => fileLocation = value; }

    public RoomDataHandler(string fileLocation)
    {
        this.fileLocation = fileLocation;
    }

    public void Write(List<Room> rooms)
    {
        var jsonList = JsonSerializer.Serialize(rooms, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(fileLocation, jsonList);
    }

    public List<Room> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Room> rooms = new List<Room>();
        if (jsonString != "")
        {
            rooms = JsonSerializer.Deserialize<List<Room>>(jsonString);
        }

        return rooms;
    }

}
