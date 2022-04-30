using System;
using System.Collections.Generic;

public class Medication
{
    private string medicationId;
    private string medicationCode;
    private String brandName;
    private string dosage;
    private List<String> activeSubstances;
    private string form;
    private List<String> notes;

    public string MedicationId { get => medicationId; set => medicationId = value; }
    public string BrandName { get => brandName; set => brandName = value; }
    public string Dosage { get => dosage; set => dosage = value; }
    public List<string> Notes { get => notes; set => notes = value; }
    public string MedicationCode { get => medicationCode; set => medicationCode = value; }
    public string Form { get => form; set => form = value; }
    public List<string> ActiveSubstances { get => activeSubstances; set => activeSubstances = value; }

    public static Medication Parse(string id)
    {
        Medication medication = new Medication();
        medication.medicationId = id;
        return medication;
    }

    public string ToString()
    {
        return this.brandName + " " + this.dosage + ", " + this.form;
    }
}