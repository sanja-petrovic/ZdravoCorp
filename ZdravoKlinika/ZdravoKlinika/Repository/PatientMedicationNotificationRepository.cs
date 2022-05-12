using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class PatientMedicationNotificationRepository
    {
        private PatientMedicationNotificationDataHandler patientMedicationNotificationDataHandler;
        private PrescriptionRepository prescriptionRepository;

        private List<PatientMedicationNotification> notifications;

        internal PatientMedicationNotificationDataHandler PatientMedicationNotificationDataHandler { get => patientMedicationNotificationDataHandler; set => patientMedicationNotificationDataHandler = value; }
        internal PrescriptionRepository PrescriptionRepository { get => prescriptionRepository; set => prescriptionRepository = value; }
        public List<PatientMedicationNotification> Notifications 
        {
            get
            {
                if (notifications == null)
                {
                    notifications = new List<PatientMedicationNotification>();
                }
                return notifications;
            }
            set
            {
                DeleteAllNotifications();
                if (value != null)
                {
                    foreach (PatientMedicationNotification notification in value)
                    {
                        CreateNotification(notification);
                    }
                }
            }
        }

        public PatientMedicationNotificationRepository()
        {
            PatientMedicationNotificationDataHandler = new PatientMedicationNotificationDataHandler();
            PrescriptionRepository = new PrescriptionRepository();
            ReadDataFromFiles();
            
        }

        private void ReadDataFromFiles()
        {
            notifications = PatientMedicationNotificationDataHandler.Read();
            if (notifications == null) notifications = new List<PatientMedicationNotification>();
        }

        private void UpdateReferences(PatientMedicationNotification notification)
        {
            notification.Prescription = PrescriptionRepository.GetById(notification.Prescription.Id);
        }
        public List<PatientMedicationNotification> GetAll()
        {
            ReadDataFromFiles();
            foreach (PatientMedicationNotification notification in Notifications)
            {
                UpdateReferences(notification);
            }
            return Notifications;
        }
        public PatientMedicationNotification? GetById(int id)
        {
            PatientMedicationNotification? notificationToReturn = null;
            foreach(PatientMedicationNotification notification in Notifications)
            {
                if(notification.NotificationId == id)
                {
                    UpdateReferences(notification);
                    notificationToReturn = notification;
                    break;
                }
            }
            return notificationToReturn;
        }
        public void CreateNotification(PatientMedicationNotification notification)
        {
            if(notification == null)
            {
                return;
            }
            if(Notifications == null)
            {
                Notifications = new List<PatientMedicationNotification>();
            }
            foreach(PatientMedicationNotification notif in Notifications)
            {
                if(notif.NotificationId == notification.NotificationId)
                {
                    return;
                }
            }
            Notifications.Add(notification);
            PatientMedicationNotificationDataHandler.Write(Notifications);
            return;
        }
        public void DeleteNotification(PatientMedicationNotification notification)
        {
            if (notification == null)
            {
                return;
            }
            if (Notifications != null)
            {
                if (Notifications.Contains(notification))
                {
                    Notifications.Remove(notification);
                }
            }
            PatientMedicationNotificationDataHandler.Write(Notifications);
        }
        public void DeleteAllNotifications()
        {
            if(Notifications != null)
            {
                Notifications.Clear();
            }
        }

        public void UpdateNotification(PatientMedicationNotification notification)
        {
            int index = -1;
            foreach(PatientMedicationNotification notif in Notifications)
            {
                if(notif.NotificationId == notification.NotificationId)
                {
                    index = Notifications.IndexOf(notif);
                }
            }

            if(index == -1)
            {
                Console.WriteLine("err");
                return;
            }
            Notifications[index] = notification;
            PatientMedicationNotificationDataHandler.Write(Notifications);
            return ;
        }
    }
}
