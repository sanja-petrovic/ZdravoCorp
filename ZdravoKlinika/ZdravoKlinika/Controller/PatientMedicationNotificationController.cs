using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class PatientMedicationNotificationController
    {
        private PatientMedicationNotificationService notificationService;

        public PatientMedicationNotificationController()
        {
            notificationService = new PatientMedicationNotificationService();
        }
        public List<PatientMedicationNotification> GetAll()
        {
            return this.notificationService.GetAll();
        }
        public PatientMedicationNotification GetById(int id)
        {
            return this.notificationService.GetById(id);
        }
        public List<PatientMedicationNotification> GetByPatientForDate(String id, DateTime date)
        {
            return notificationService.GetByPatientForDate(id, date);
        }
        public List<DateTime> GetNotificationDatesForPatient(String id)
        {
            return notificationService.GetNotificationDatesForPatient(id);
        }
        public List<DateTime> GetPossibleTriggerTimes(PatientMedicationNotification notification)
        {
            return notificationService.GetPossibleTriggerTimes(notification);
        }
        public List<PatientMedicationNotification> GetUpcomingNotifications(string id, int hours)
        {
            return notificationService.GetUpcomingNotifications(id, hours);
        }
        public void CreateNotification(RegisteredUser sender,RegisteredUser Receiver, String notificationText, Prescription prescription, String note, DateTime time)
        {
            this.notificationService.CreateNotification(new PatientMedicationNotification(-1,sender,Receiver,notificationText,prescription,note,time));
        }
        public void DeleteNotification(int id)
        {
            this.notificationService.DeleteNotification(id);
        }
        public void DeleteAllNotifications()
        {
            this.notificationService.DeleteAllNotifications();
        }
        public void UpdateTriggerTime(int id, DateTime newTriggerTime)
        {
            this.notificationService.UpdateTriggerTime(this.GetById(id),newTriggerTime);
        }
        public void UpdateNotification(int id, RegisteredUser sender, RegisteredUser Receiver, String notificationText, Prescription prescription, String note, DateTime time)
        {
            this.notificationService.UpdateNotification(new PatientMedicationNotification(id,sender,Receiver,notificationText,prescription,note,time));
        }
    }
}
