// File:    PatientRepository.cs
// Author:  sanya
// Created: Saturday, 9 April 2022 7:38:20 PM
// Purpose: Definition of Class PatientRepository

using System;

public class PatientRepository
{
   private PatientDataHandler patientDataHandler;
   private System.Collections.Generic.List<Patient> patient;
   
   /// <summary>
   /// Property for collection of Patient
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Patient> Patient
   {
      get
      {
         if (patient == null)
            patient = new System.Collections.Generic.List<Patient>();
         return patient;
      }
      set
      {
         RemoveAllPatient();
         if (value != null)
         {
            foreach (Patient oPatient in value)
               AddPatient(oPatient);
         }
      }
   }
   
   /// <summary>
   /// Add a new Patient in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPatient(Patient newPatient)
   {
      if (newPatient == null)
         return;
      if (this.patient == null)
         this.patient = new System.Collections.Generic.List<Patient>();
      if (!this.patient.Contains(newPatient))
         this.patient.Add(newPatient);
   }
   
   /// <summary>
   /// Remove an existing Patient from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePatient(Patient oldPatient)
   {
      if (oldPatient == null)
         return;
      if (this.patient != null)
         if (this.patient.Contains(oldPatient))
            this.patient.Remove(oldPatient);
   }
   
   /// <summary>
   /// Remove all instances of Patient from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPatient()
   {
      if (patient != null)
         patient.Clear();
   }
   
   public List<Patient> GetAll()
   {
      throw new NotImplementedException();
   }
   
   public Patient GetById(string id)
   {
      throw new NotImplementedException();
   }
   
   public void CreatePatient(Patient patient)
   {
      throw new NotImplementedException();
   }
   
   public void DeletePatient(Patient patient)
   {
      throw new NotImplementedException();
   }
   
   public void UpdatePatient(Patient patient)
   {
      throw new NotImplementedException();
   }

}