using System;

public class Report
{
    private int reportId;
    private String reportName;
    private DateTime timeOfCreation;

    public int ReportId { get => reportId; set => reportId = value; }
    public string ReportName { get => reportName; set => reportName = value; }
    public DateTime TimeOfCreation { get => timeOfCreation; set => timeOfCreation = value; }
}