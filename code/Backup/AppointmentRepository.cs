// File:    AppointmentRepository.cs
// Author:  sanya
// Created: Tuesday, 5 April 2022 9:29:11 PM
// Purpose: Definition of Class AppointmentRepository

using System;

public class AppointmentRepository
{
   private AppointmentDataHandler appointmentDataHandler;
   private System.Collections.Generic.List<Appointment> appointment;
   
   /// <summary>
   /// Property for collection of Appointment
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Appointment> Appointment
   {
      get
      {
         if (appointment == null)
            appointment = new System.Collections.Generic.List<Appointment>();
         return appointment;
      }
      set
      {
         RemoveAllAppointment();
         if (value != null)
         {
            foreach (Appointment oAppointment in value)
               AddAppointment(oAppointment);
         }
      }
   }
   
   /// <summary>
   /// Add a new Appointment in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddAppointment(Appointment newAppointment)
   {
      if (newAppointment == null)
         return;
      if (this.appointment == null)
         this.appointment = new System.Collections.Generic.List<Appointment>();
      if (!this.appointment.Contains(newAppointment))
         this.appointment.Add(newAppointment);
   }
   
   /// <summary>
   /// Remove an existing Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveAppointment(Appointment oldAppointment)
   {
      if (oldAppointment == null)
         return;
      if (this.appointment != null)
         if (this.appointment.Contains(oldAppointment))
            this.appointment.Remove(oldAppointment);
   }
   
   /// <summary>
   /// Remove all instances of Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllAppointment()
   {
      if (appointment != null)
         appointment.Clear();
   }
   
   public List<Appointment> GetAll()
   {
      throw new NotImplementedException();
   }
   
   public List<Appointment> GetAppointmentById(String id)
   {
      throw new NotImplementedException();
   }
   
   public List<Appointment> GetAppointmentsByPatient(String patientId)
   {
      throw new NotImplementedException();
   }
   
   public List<Appointment> GetAppointmentsByDoctor(String doctorId)
   {
      throw new NotImplementedException();
   }
   
   public void CreateAppointment(Appointment appointment)
   {
      throw new NotImplementedException();
   }
   
   public void DeleteAppointment(Appointment appointment)
   {
      throw new NotImplementedException();
   }
   
   public void EditAppointment(Appointment appointment)
   {
      throw new NotImplementedException();
   }

}