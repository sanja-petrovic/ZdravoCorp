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
        public void CreateNotification(RegisteredUser sender,RegisteredUser reciver, String notificationText, Prescription prescription)
        {
            this.notificationService.CreateNotification(sender,reciver,notificationText,prescription);
        }
        public void DeleteNotification(int id)
        {
            this.notificationService.DeleteNotification(id);
        }
        public void DeleteAllNotifications()
        {
            this.notificationService.DeleteAllNotifications();
        }
        public void UpdateNotification(int id, RegisteredUser sender, RegisteredUser reciver, String notificationText, Prescription prescription)
        {
            this.UpdateNotification(id, sender, reciver, notificationText, prescription);
        }
    }
}
