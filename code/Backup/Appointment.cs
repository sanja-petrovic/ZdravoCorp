// File:    Appointment.cs
// Author:  sanya
// Created: Monday, 4 April 2022 8:23:55 PM
// Purpose: Definition of Class Appointment

using System;

public class Appointment
{
   private int appointmentId;
   private DateTime dateAndTime;
   private List<String> diagnoses;
   private String doctorsNotes;
   private AppointmentType type;
   private bool emergency;
   private int duration;
   
   private Room room;
   private System.Collections.Generic.List<Medication> prescriptions;
   
   /// <summary>
   /// Property for collection of Medication
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Medication> Prescriptions
   {
      get
      {
         if (prescriptions == null)
            prescriptions = new System.Collections.Generic.List<Medication>();
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
   
   /// <summary>
   /// Add a new Medication in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPrescriptions(Medication newMedication)
   {
      if (newMedication == null)
         return;
      if (this.prescriptions == null)
         this.prescriptions = new System.Collections.Generic.List<Medication>();
      if (!this.prescriptions.Contains(newMedication))
         this.prescriptions.Add(newMedication);
   }
   
   /// <summary>
   /// Remove an existing Medication from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePrescriptions(Medication oldMedication)
   {
      if (oldMedication == null)
         return;
      if (this.prescriptions != null)
         if (this.prescriptions.Contains(oldMedication))
            this.prescriptions.Remove(oldMedication);
   }
   
   /// <summary>
   /// Remove all instances of Medication from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPrescriptions()
   {
      if (prescriptions != null)
         prescriptions.Clear();
   }
   private Doctor doctor;

}