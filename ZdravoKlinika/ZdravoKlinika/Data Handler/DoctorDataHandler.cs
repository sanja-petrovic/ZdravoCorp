// File:    PatientDataHandler.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientDataHandler

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class DoctorDataHandler
{
    private static String fileName = "doctors.json";
    private static String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + fileName;
    public void Write(List<Doctor> doctors)
   {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var jsonList = JsonSerializer.Serialize(doctors, options);
        File.WriteAllText(fileLocation, jsonList);
    }
   
   public List<Doctor> Read()
   {
        return JsonSerializer.Deserialize<List<Doctor>>(File.ReadAllText(fileLocation));
    }

}