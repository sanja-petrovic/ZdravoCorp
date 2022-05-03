using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class PatientMedicationNotificationService
    {
        private PatientMedicationNotificationRepository patientMedicationNotificationRepository;

        public PatientMedicationNotificationRepository PatientMedicationNotificationRepository { get => patientMedicationNotificationRepository; set => patientMedicationNotificationRepository = value; }

        public PatientMedicationNotificationService()
        {
        this.patientMedicationNotificationRepository = new PatientMedicationNotificationRepository();

        }

        public List<PatientMedicationNotification> GetAll()
        {
            return patientMedicationNotificationRepository.GetAll();
        }
        public PatientMedicationNotification GetById(int id)
        {
            return this.patientMedicationNotificationRepository.GetById(id);
        }
        public void CreateNotification(RegisteredUser sender,RegisteredUser reciver, string notificationText, Prescription prescription)
        {
            List<PatientMedicationNotification> notifications = new List<PatientMedicationNotification>();
            int newId;
            notifications = patientMedicationNotificationRepository.GetAll();
            if (notifications.Count > 0)
            {
                newId = notifications.Last().NotificationId + 1;
            }
            else
            {
                newId = 1;
            }
            PatientMedicationNotification notification = new PatientMedicationNotification(newId,sender, reciver, notificationText,prescription );
            patientMedicationNotificationRepository.CreateNotification( notification );
        }
        public void DeleteNotification(int id)
        {
            patientMedicationNotificationRepository.DeleteNotification(patientMedicationNotificationRepository.GetById(id));
        }
        public void DeleteAllNotifications()
        {
            patientMedicationNotificationRepository.DeleteAllNotifications();
        }
        
        public void UpdateNotification(int id, RegisteredUser sender, RegisteredUser reciver, string notificationText, Prescription prescription)
        {

            PatientMedicationNotification notification = new PatientMedicationNotification(id, sender, reciver, notificationText, prescription);
            patientMedicationNotificationRepository.UpdateNotification( notification );
        }
    }
    
}
