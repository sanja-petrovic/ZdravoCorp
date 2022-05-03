using System.Collections.Generic;
using System;

public class Doctor : Employee
{
    private String specialty;
    private String education; //TODO delete

    public Doctor()
    {

    }

    public string Specialty { get => specialty; set => specialty = value; }
    public string Education { get => education; set => education = value; }
    public string NameAndLast { get => Name + " " + Lastname + " " + PersonalId; }

    public bool IsSpecialist()
    {
        throw new NotImplementedException();
    }

    public static Doctor Parse(string id)
    {
        Doctor doc = new Doctor();
        doc.PersonalId = id;
        return doc;
    }
    
}