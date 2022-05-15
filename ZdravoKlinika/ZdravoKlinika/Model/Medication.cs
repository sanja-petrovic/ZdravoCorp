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
    private String note; 
    private List<string> allergens;
    private bool validated;
    private List<Medication> alternatives;
    private string classification; //analgetik, antipiretik, antibiotik...
    private string indications; //za glavobolju, protiv povisene temperature, protiv bakterijske infekcije...
    private string sideEffects; //gastritis,...
    //private Doctor reviewer;
    //private string comment;
    private string dosageInstructions;
    private int amount;

    public string MedicationId { get => medicationId; set => medicationId = value; }
    public string BrandName { get => brandName; set => brandName = value; }
    public string Dosage { get => dosage; set => dosage = value; }
    public String Note { get => note; set => note = value; }
    public string MedicationCode { get => medicationCode; set => medicationCode = value; }
    public string Form { get => form; set => form = value; }
    public List<string> ActiveSubstances { get => activeSubstances; set => activeSubstances = value; }
    public List<string> Allergens { get => allergens; set => allergens = value; }
    public bool Validated { get => validated; set => validated = value; }
    public List<Medication> Alternatives { get => alternatives; set => alternatives = value; }
    public string Classification { get => classification; set => classification = value; }
    public string SideEffects { get => sideEffects; set => sideEffects = value; }
    //public Doctor Reviewer { get => reviewer; set => reviewer = value; }
    public string Indications { get => indications; set => indications = value; }
    //public string Comment { get => comment; set => comment = value; }
    public string DosageInstructions { get => dosageInstructions; set => dosageInstructions = value; }
    public int Amount { get => amount; set => amount = value; }

    public Medication()
    {
        this.validated = false;
        this.Alternatives = new List<Medication>();
    }

    public Medication(string medicationId, string medicationCode, string brandName, string dosage, List<string> activeSubstances, string form, String note, List<string> allergens, bool validated, List<Medication> alternatives, string classification, string indications, string sideEffects, string dosageInstructions, int amount)
    {
        this.medicationId = medicationId;
        this.medicationCode = medicationCode;
        this.brandName = brandName;
        this.dosage = dosage;
        this.activeSubstances = activeSubstances;
        this.form = form;
        this.note = note;
        this.allergens = allergens;
        this.validated = validated;
        this.alternatives = alternatives;
        this.classification = classification;
        this.indications = indications;
        this.sideEffects = sideEffects;
        this.dosageInstructions = dosageInstructions;
        this.amount = amount;
    }

    public static Medication Parse(string id)
    {
        Medication medication = new Medication();
        medication.medicationId = id;
        return medication;
    }

    public override string ToString()
    {
        return this.brandName + " " + this.dosage + ", " + this.form;
    }

}
