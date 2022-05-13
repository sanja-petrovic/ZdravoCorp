using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class RoomDataHandler
{
    private static String fileName = "room.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

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