using System;

public class Employee : RegisteredUser
{
    private int yearsOfService;
    private string educationLevel;
    private bool working;

    public int YearsOfService { get => yearsOfService; set => yearsOfService = value; }
    public string EducationLevel { get => educationLevel; set => educationLevel = value; }
    public bool Working { get => working; set => working = value; }
}