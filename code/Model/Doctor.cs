using System.Collections.Generic;
using System;

public class Doctor : Employee
{
    private String specialty;
    private String education;

    public string Specialty { get => specialty; set => specialty = value; }
    public string Education { get => education; set => education = value; }

    public bool IsSpecialist()
    {
        throw new NotImplementedException();
    }

}