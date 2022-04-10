using System;
using System.Collections.Generic;

public class AppointmentDataHandler
{
    private int fileLocation;

    public int FileLocation { get => fileLocation; set => fileLocation = value; }

    public void Write(List<Appointment> appointments)
    {
        throw new NotImplementedException();
    }

    public List<Appointment> Read()
    {
        throw new NotImplementedException();
    }

}