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
    private List<string> allergens;
    bool validated;
    private List<Medication> alternatives;
    private string classification; //analgetik, antipiretik, antibiotik...
    private string indications; //za glavobolju, protiv povisene temperature, protiv bakterijske infekcije...
    private string sideEffects; //gastritis,...
    private Doctor approvedBy;
    private string note;
    private string dosingInstructions;

    public string MedicationId { get => medicationId; set => medicationId = value; }
    public string BrandName { get => brandName; set => brandName = value; }
    public string Dosage { get => dosage; set => dosage = value; }
    public List<string> Notes { get => notes; set => notes = value; }
    public string MedicationCode { get => medicationCode; set => medicationCode = value; }
    public string Form { get => form; set => form = value; }
    public List<string> ActiveSubstances { get => activeSubstances; set => activeSubstances = value; }
    public List<string> Allergens { get => allergens; set => allergens = value; }
    public bool Validated { get => validated; set => validated = value; }
    public List<Medication> Alternatives { get => alternatives; set => alternatives = value; }
    public string Classification { get => classification; set => classification = value; }
    public string SideEffects { get => sideEffects; set => sideEffects = value; }
    public Doctor ApprovedBy { get => approvedBy; set => approvedBy = value; }
    public string Indications { get => indications; set => indications = value; }
    public string Note { get => note; set => note = value; }
    public string DosingInstructions { get => dosingInstructions; set => dosingInstructions = value; }

    public Medication()
    {
        this.validated = false;
        this.Alternatives = new List<Medication>();
    }

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