using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class EmployeeNotificationRepository : Interfaces.IEmployeeNotificationRepository
    {
        private EmployeeNotificationDataHandler dataHandler;
        private List<EmployeeNotification> employeeNotifications;

        public EmployeeNotificationRepository()
        { 
            dataHandler = new EmployeeNotificationDataHandler();
            ReadDataFromFile();
        }

        public void Add(EmployeeNotification notification)
        {
            employeeNotifications.Add(notification);
            dataHandler.Write(employeeNotifications);
        }

        private void ReadDataFromFile() 
        {
            employeeNotifications = dataHandler.Read();
            if (employeeNotifications == null)
            {
                employeeNotifications = new List<EmployeeNotification>();
            }
        }

        public List<EmployeeNotification> GetAll()
        {
            ReadDataFromFile();
            return employeeNotifications;
        }

        public List<EmployeeNotification> GetAllPersonalNotifications(RegisteredUser user)
        {
            ReadDataFromFile();
            List<EmployeeNotification> notifsToReturn = new List<EmployeeNotification>();
            foreach (EmployeeNotification notif in employeeNotifications)
            {
                if (notif.Receiver.PersonalId.Equals(user.PersonalId))
                {
                    notifsToReturn.Add(notif);
                }
            }
            return notifsToReturn;
        }

        public List<EmployeeNotification> GetSpecificTypeOfNotifications(RegisteredUser user, EmployeeNotificationType type)
        {
            ReadDataFromFile();
            List<EmployeeNotification> notifsToReturn = new List<EmployeeNotification>();
            foreach (EmployeeNotification notif in employeeNotifications)
            {
                if (notif.Receiver.PersonalId.Equals(user.PersonalId) && notif.Type == type)
                {
                    notifsToReturn.Add(notif);
                }
            }
            return notifsToReturn;
        }

        public void Remove(EmployeeNotification notification)
        {
            ReadDataFromFile();
            int indexToRemove = GetIndex(notification.NotificationId);
            employeeNotifications.RemoveAt(indexToRemove);
            dataHandler.Write(employeeNotifications);
            return;
        }
        private int GetIndex(String id) 
        {
            int indexToRemove = -1;
            foreach (EmployeeNotification notification in employeeNotifications)
            {
                if (notification.NotificationId.Equals(id))
                {
                    indexToRemove = employeeNotifications.IndexOf(notification);
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("Notification does not exist");
            }
            return indexToRemove;
        }

        public EmployeeNotification GetById(string id)
        {
            return employeeNotifications.Find(notif => notif.NotificationId.Equals(id));
        }

        public void Update(EmployeeNotification item)
        {
            int index = GetIndex(item.NotificationId);
            if (index != -1)
            {
                employeeNotifications[index] = item;
                dataHandler.Write(employeeNotifications);
            }
        }

        public void RemoveAll()
        {
            if (employeeNotifications != null)
                employeeNotifications.Clear();
            dataHandler.Write(employeeNotifications);
        }


        public void MarkAsRead(EmployeeNotification notification)
        {
            notification.Read = true;
            this.employeeNotifications[this.employeeNotifications.FindIndex(n => n.NotificationId.Equals(notification.NotificationId))] = notification;
        }

        public void MarkAllPersonalNotificationsAsRead(RegisteredUser user)
        {
            foreach(EmployeeNotification n in this.GetAllPersonalNotifications(user))
            {
                if(!n.Read)
                {
                    n.Read = true;
                }
            }
        }
    }
}
