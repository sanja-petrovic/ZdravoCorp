// File:    AppointmentService.cs
// Author:  asd
// Created: Sunday, 10 April 2022 1:16:49 AM
// Purpose: Definition of Class AppointmentService

using System;

public class AppointmentService
{
   private AppointmentRepository appointmentRepository;
   
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