using System;
using System.Collections.Generic;

public class PatientDataHandler
{
    private static String fileLocation = "path";

    public string FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<Patient> patients)
    {
        throw new NotImplementedException();
    }

    public List<Patient> Read()
    {
        throw new NotImplementedException();
    }

}