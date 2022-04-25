using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;

public class Appointment
{
    private int appointmentId;
    private DateTime dateAndTime;
    private List<String> diagnoses;
    private String doctorsNotes;
    private AppointmentType type;
    private bool emergency;
    private int duration;
    private Doctor doctor;
    private Patient patient;
    private Room room;
    private List<Medication> prescriptions;

    public Appointment() { }

    public Appointment(int appointmentId, Doctor doctor, Patient patient, Room room, int duration, bool emergency, AppointmentType type, DateTime dateTime)
    {
        this.appointmentId = appointmentId;
        this.doctor = doctor;
        this.patient = patient;
        this.room = room;
        this.duration = duration;
        this.emergency = emergency;
        this.type = type;
        this.dateAndTime = dateTime;
    }


    public List<Medication> Prescriptions
    {
        get
        {
            if (prescriptions == null)
                prescriptions = new List<Medication>();
            return prescriptions;
        }
        set
        {
            RemoveAllPrescriptions();
            if (value != null)
            {
                foreach (Medication oMedication in value)
                    AddPrescriptions(oMedication);
            }
        }
    }

    public int AppointmentId { get => appointmentId; set => appointmentId = value; }
    public DateTime DateAndTime { get => dateAndTime; set => dateAndTime = value; }
    public List<string> Diagnoses { get => diagnoses; set => diagnoses = value; }
    public string DoctorsNotes { get => doctorsNotes; set => doctorsNotes = value; }
    public AppointmentType Type { get => type; set => type = value; }

    public String getTranslatedType()
    {
        if(type.Equals(AppointmentType.Regular)) {
            return "Pregled";
        } else if(type.Equals(AppointmentType.Surgery))
        {
            return "Operacija";
        } else
        {
            return null;
        }
    }
    public bool Emergency { get => emergency; set => emergency = value; }
    public int Duration { get => duration; set => duration = value; }
    public Doctor Doctor { get => doctor; set => doctor = value; }
    public Patient Patient { get => patient; set => patient = value; }
    public Room Room { get => room; set => room = value; }

    public void AddPrescriptions(Medication newMedication)
    {
        if (newMedication == null)
            return;
        if (this.prescriptions == null)
            this.prescriptions = new List<Medication>();
        if (!this.prescriptions.Contains(newMedication))
            this.prescriptions.Add(newMedication);
    }
    public void RemovePrescriptions(Medication oldMedication)
    {
        if (oldMedication == null)
            return;
        if (this.prescriptions != null)
            if (this.prescriptions.Contains(oldMedication))
                this.prescriptions.Remove(oldMedication);
    }
    public void RemoveAllPrescriptions()
    {
        if (prescriptions != null)
            prescriptions.Clear();
    }

}