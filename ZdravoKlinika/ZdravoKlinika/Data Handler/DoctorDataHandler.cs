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
   
   private String fileLocation = Directory.GetCurrentDirectory()+ Path.PathSeparator + "doctors.json";
   
   public void Write(List<Doctor> doctors)
   {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var jsonList = JsonSerializer.Serialize(doctors, options);
        File.WriteAllText(fileLocation, jsonList);
    }
   
   public List<Doctor> Read()
   {
        try
        {
            var data = File.ReadAllText(fileLocation);
            List<Doctor> doctors = new List<Doctor>();

            doctors = JsonSerializer.Deserialize<List<Doctor>>(data);
            return doctors;
        }
        catch (Exception ex)
        {
            Console.WriteLine("err");
            return null;
        }
   }

}