using System;
using System.Collections.Generic;

public class Appointment
{
    private int appointmentId;
    private DateTime dateAndTime;
    private List<String> diagnoses;
    private String doctorsNotes;
    private AppointmentType type;
    private bool emergency;
    private int duration;
    private String doctorId;
    private String patientId;
    private String roomId;
    private List<Medication> prescriptions;

    public Appointment() { }

    public Appointment(int appointmentId, String doctorId, String patientId, String roomId, int duration, bool emergency, AppointmentType type, DateTime dateTime)
    {
        this.appointmentId = appointmentId;
        this.doctorId = doctorId;
        this.patientId = patientId;
        this.roomId = roomId;
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
    public bool Emergency { get => emergency; set => emergency = value; }
    public int Duration { get => duration; set => duration = value; }
    public string DoctorId { get => doctorId; set => doctorId = value; }
    public String PatientId { get => patientId; set => patientId = value; }
    public string RoomId { get => roomId; set => roomId = value; }

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