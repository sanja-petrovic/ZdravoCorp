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
        /*{
            get
            {
                if(notifications == null)
                {
                    notifications = new List<PatientMedicationNotification>();
                }
                return notifications;
            }
            set
            {
                DeleteAllNotifications();
                if(value != null)
                {
                    foreach(PatientMedicationNotification notification in value)
                    {
                        CreateNotification(notification);
                    }
                }
            }
        }*/

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
            this.Notifications = null;
            this.notifications = PatientMedicationNotificationDataHandler.Read();
            updatePreferences();
        }
        private void updatePreferences()
        {
            foreach(PatientMedicationNotification notification in Notifications)
            {
                notification.Prescription = PrescriptionRepository.GetById(notification.Prescription.Id);
            }
        }
        public List<PatientMedicationNotification> GetAll()
        {
            updatePreferences();
            return Notifications;
        }
        public PatientMedicationNotification GetById(int id)
        {
            updatePreferences();
            foreach(PatientMedicationNotification notification in this.Notifications)
            {
                if(notification.NotificationId == id)
                {
                    return notification;
                }
            }
            return null;
        }
        public void CreateNotification(PatientMedicationNotification notification)
        {
            if(notification == null)
            {
                return;
            }
            if(this.Notifications == null)
            {
                this.Notifications = new List<PatientMedicationNotification>();
            }
            foreach(PatientMedicationNotification notif in Notifications)
            {
                if(notif.NotificationId == notification.NotificationId)
                {
                    return;
                }
            }
            this.Notifications.Add(notification);
            PatientMedicationNotificationDataHandler.Write(this.Notifications);
            return;
        }
        public void DeleteNotification(PatientMedicationNotification notification)
        {
            if (notification == null)
            {
                return;
            }
            if (this.Notifications != null)
            {
                if (this.Notifications.Contains(notification))
                {
                    this.Notifications.Remove(notification);
                }
            }
            PatientMedicationNotificationDataHandler.Write(this.Notifications);
        }
        public void DeleteAllNotifications()
        {
            if(this.Notifications != null)
            {
                Notifications.Clear();
            }
        }

        public void UpdateNotification(PatientMedicationNotification notification)
        {
            int index = -1;
            foreach(PatientMedicationNotification notif in this.Notifications)
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
