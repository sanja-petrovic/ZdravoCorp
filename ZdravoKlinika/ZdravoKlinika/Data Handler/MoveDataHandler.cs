
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
        var jsonList = JsonSerializer.Serialize(moves, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(fileLocation, jsonList);
    }

    public List<Move> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Move> moves = new List<Move>();
        if (jsonString != "")
        {
            moves = JsonSerializer.Deserialize<List<Move>>(jsonString);
        }

        return moves;
    }

}