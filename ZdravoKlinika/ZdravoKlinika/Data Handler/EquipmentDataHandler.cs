using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class EquipmentDataHandler
{
    private static String fileName = "equipment.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;

    public void Write(List<Equipment> equipmentList)
    {
        var jsonList = JsonSerializer.Serialize(equipmentList, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(fileLocation, jsonList);
    }

    public List<Equipment> Read()
    {
        string jsonString = File.ReadAllText(fileLocation);
        List<Equipment> eq = new List<Equipment>();
        if (jsonString != "")
        {
            eq = JsonSerializer.Deserialize<List<Equipment>>(jsonString);
        }

        return eq;
    }

}