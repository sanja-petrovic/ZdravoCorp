// File:    MedicalRecord.cs
// Author:  sanya
// Created: Tuesday, 5 April 2022 9:34:22 PM
// Purpose: Definition of Class MedicalRecord

using System;

public class MedicalRecord
{
   private Patient patient;
   private List<String> diagnoses;
   private List<String> alergies;
   
   private System.Collections.Generic.List<Medication> currentMedication;
   
   /// <summary>
   /// Property for collection of Medication
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Medication> CurrentMedication
   {
      get
      {
         if (currentMedication == null)
            currentMedication = new System.Collections.Generic.List<Medication>();
         return currentMedication;
      }
      set
      {
         RemoveAllCurrentMedication();
         if (value != null)
         {
            foreach (Medication oMedication in value)
               AddCurrentMedication(oMedication);
         }
      }
   }
   
   /// <summary>
   /// Add a new Medication in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddCurrentMedication(Medication newMedication)
   {
      if (newMedication == null)
         return;
      if (this.currentMedication == null)
         this.currentMedication = new System.Collections.Generic.List<Medication>();
      if (!this.currentMedication.Contains(newMedication))
         this.currentMedication.Add(newMedication);
   }
   
   /// <summary>
   /// Remove an existing Medication from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveCurrentMedication(Medication oldMedication)
   {
      if (oldMedication == null)
         return;
      if (this.currentMedication != null)
         if (this.currentMedication.Contains(oldMedication))
            this.currentMedication.Remove(oldMedication);
   }
   
   /// <summary>
   /// Remove all instances of Medication from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllCurrentMedication()
   {
      if (currentMedication != null)
         currentMedication.Clear();
   }
   private System.Collections.Generic.List<Medication> pastMedications;
   
   /// <summary>
   /// Property for collection of Medication
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Medication> PastMedications
   {
      get
      {
         if (pastMedications == null)
            pastMedications = new System.Collections.Generic.List<Medication>();
         return pastMedications;
      }
      set
      {
         RemoveAllPastMedications();
         if (value != null)
         {
            foreach (Medication oMedication in value)
               AddPastMedications(oMedication);
         }
      }
   }
   
   /// <summary>
   /// Add a new Medication in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPastMedications(Medication newMedication)
   {
      if (newMedication == null)
         return;
      if (this.pastMedications == null)
         this.pastMedications = new System.Collections.Generic.List<Medication>();
      if (!this.pastMedications.Contains(newMedication))
         this.pastMedications.Add(newMedication);
   }
   
   /// <summary>
   /// Remove an existing Medication from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePastMedications(Medication oldMedication)
   {
      if (oldMedication == null)
         return;
      if (this.pastMedications != null)
         if (this.pastMedications.Contains(oldMedication))
            this.pastMedications.Remove(oldMedication);
   }
   
   /// <summary>
   /// Remove all instances of Medication from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPastMedications()
   {
      if (pastMedications != null)
         pastMedications.Clear();
   }
   private System.Collections.Generic.List<Report> reports;
   
   /// <summary>
   /// Property for collection of Report
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Report> Reports
   {
      get
      {
         if (reports == null)
            reports = new System.Collections.Generic.List<Report>();
         return reports;
      }
      set
      {
         RemoveAllReports();
         if (value != null)
         {
            foreach (Report oReport in value)
               AddReports(oReport);
         }
      }
   }
   
   /// <summary>
   /// Add a new Report in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddReports(Report newReport)
   {
      if (newReport == null)
         return;
      if (this.reports == null)
         this.reports = new System.Collections.Generic.List<Report>();
      if (!this.reports.Contains(newReport))
         this.reports.Add(newReport);
   }
   
   /// <summary>
   /// Remove an existing Report from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveReports(Report oldReport)
   {
      if (oldReport == null)
         return;
      if (this.reports != null)
         if (this.reports.Contains(oldReport))
            this.reports.Remove(oldReport);
   }
   
   /// <summary>
   /// Remove all instances of Report from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllReports()
   {
      if (reports != null)
         reports.Clear();
   }
   private System.Collections.Generic.List<Appointment> pastAppointments;
   
   /// <summary>
   /// Property for collection of Appointment
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Appointment> PastAppointments
   {
      get
      {
         if (pastAppointments == null)
            pastAppointments = new System.Collections.Generic.List<Appointment>();
         return pastAppointments;
      }
      set
      {
         RemoveAllPastAppointments();
         if (value != null)
         {
            foreach (Appointment oAppointment in value)
               AddPastAppointments(oAppointment);
         }
      }
   }
   
   /// <summary>
   /// Add a new Appointment in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPastAppointments(Appointment newAppointment)
   {
      if (newAppointment == null)
         return;
      if (this.pastAppointments == null)
         this.pastAppointments = new System.Collections.Generic.List<Appointment>();
      if (!this.pastAppointments.Contains(newAppointment))
         this.pastAppointments.Add(newAppointment);
   }
   
   /// <summary>
   /// Remove an existing Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePastAppointments(Appointment oldAppointment)
   {
      if (oldAppointment == null)
         return;
      if (this.pastAppointments != null)
         if (this.pastAppointments.Contains(oldAppointment))
            this.pastAppointments.Remove(oldAppointment);
   }
   
   /// <summary>
   /// Remove all instances of Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPastAppointments()
   {
      if (pastAppointments != null)
         pastAppointments.Clear();
   }
   private System.Collections.Generic.List<Appointment> upcomingAppointments;
   
   /// <summary>
   /// Property for collection of Appointment
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Appointment> UpcomingAppointments
   {
      get
      {
         if (upcomingAppointments == null)
            upcomingAppointments = new System.Collections.Generic.List<Appointment>();
         return upcomingAppointments;
      }
      set
      {
         RemoveAllUpcomingAppointments();
         if (value != null)
         {
            foreach (Appointment oAppointment in value)
               AddUpcomingAppointments(oAppointment);
         }
      }
   }
   
   /// <summary>
   /// Add a new Appointment in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddUpcomingAppointments(Appointment newAppointment)
   {
      if (newAppointment == null)
         return;
      if (this.upcomingAppointments == null)
         this.upcomingAppointments = new System.Collections.Generic.List<Appointment>();
      if (!this.upcomingAppointments.Contains(newAppointment))
         this.upcomingAppointments.Add(newAppointment);
   }
   
   /// <summary>
   /// Remove an existing Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveUpcomingAppointments(Appointment oldAppointment)
   {
      if (oldAppointment == null)
         return;
      if (this.upcomingAppointments != null)
         if (this.upcomingAppointments.Contains(oldAppointment))
            this.upcomingAppointments.Remove(oldAppointment);
   }
   
   /// <summary>
   /// Remove all instances of Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllUpcomingAppointments()
   {
      if (upcomingAppointments != null)
         upcomingAppointments.Clear();
   }

}