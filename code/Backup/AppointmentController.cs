// File:    AppointmentController.cs
// Author:  sanya
// Created: Tuesday, 5 April 2022 9:29:12 PM
// Purpose: Definition of Class AppointmentController

using System;

public class AppointmentController
{
   private AppointmentService appointmentService;
   
   public List<Appointment> GetAll()
   {
      throw new NotImplementedException();
   }
   
   public Appointment GetAppointmentById(String id)
   {
      throw new NotImplementedException();
   }
   
   public List<Appointment> GetAppointmentsByPatientId(String id)
   {
      throw new NotImplementedException();
   }
   
   public List<Appointment> GetAppointmentsByDoctorId(String id)
   {
      throw new NotImplementedException();
   }
   
   public void CreateAppointment(String doctorId, String patientId, DateTime dateAndTime, bool emergency, AppointmentType type, String roomId, int duration)
   {
      throw new NotImplementedException();
   }
   
   public void DeleteAppointment(String id)
   {
      throw new NotImplementedException();
   }
   
   public void EditAppointment(String apointmentId, DateTime dateTime, String roomId, String patientId, String doctorId, List<String> diagnosis, String doctorsNotes)
   {
      throw new NotImplementedException();
   }

}