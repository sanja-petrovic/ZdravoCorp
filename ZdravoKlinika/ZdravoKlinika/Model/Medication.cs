using System;
using System.Collections.Generic;

public class Medication
{
    private string medicationId;
    private string medicationCode;
    private String brandName;
    private int dosage;
    private String genericName;
    private List<String> notes;

    public string MedicationId { get => medicationId; set => medicationId = value; }
    public string BrandName { get => brandName; set => brandName = value; }
    public int Dosage { get => dosage; set => dosage = value; }
    public string GenericName { get => genericName; set => genericName = value; }
    public List<string> Notes { get => notes; set => notes = value; }
    public string MedicationCode { get => medicationCode; set => medicationCode = value; }

    public static Medication Parse(string id)
    {
        Medication medication = new Medication();
        medication.medicationId = id;
        return medication;
    }
}