using System;
using System.Collections.Generic;

public class Medication
{
    private int medicationId;
    private String brandName;
    private int dosage;
    private String genericName;
    private List<String> notes;

    public int MedicationId { get => medicationId; set => medicationId = value; }
    public string BrandName { get => brandName; set => brandName = value; }
    public int Dosage { get => dosage; set => dosage = value; }
    public string GenericName { get => genericName; set => genericName = value; }
    public List<string> Notes { get => notes; set => notes = value; }
}