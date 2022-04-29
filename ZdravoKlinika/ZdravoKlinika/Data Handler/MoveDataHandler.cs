
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonConverters;

public class MoveDataHandler
{
    private String fileLocation;

    public String FileLocation { get => fileLocation; set => fileLocation = value; }

    public MoveDataHandler(string fileLocation)
    {
        this.fileLocation = fileLocation;
    }

    public void Write(List<Move> moves)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.Converters.Add(new RoomConverter());
        options.Converters.Add(new EquipmentConverter());
        var json = JsonSerializer.Serialize(moves, options);
        File.WriteAllText(fileLocation, json);
    }

    public List<Move> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Move> moves = new List<Move>();

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new RoomConverter());
        options.Converters.Add(new EquipmentConverter());

        if (jsonString != "")
        {
            moves = JsonSerializer.Deserialize<List<Move>>(jsonString, options);
        }

        return moves;
    }

}