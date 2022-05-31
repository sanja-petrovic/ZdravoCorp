using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class EmployeeNotificationRepository
    {
        private EmployeeNotificationDataHandler dataHandler;
        private List<EmployeeNotification> employeeNotifications;

        public EmployeeNotificationRepository()
        { 
            dataHandler = new EmployeeNotificationDataHandler();
            ReadData();
        }

        public void CreateNotification(EmployeeNotification notification)
        {
            employeeNotifications.Add(notification);
            dataHandler.Write(employeeNotifications);
        }

        private void ReadData() 
        {
            employeeNotifications = dataHandler.Read();
            if (employeeNotifications == null)
            {
                employeeNotifications = new List<EmployeeNotification>();
            }
        }

        public List<EmployeeNotification> GetAll()
        {
            ReadData();
            return employeeNotifications;
        }

        public List<EmployeeNotification> GetAllPersonalNotifications(RegisteredUser user)
        {
            ReadData();
            List<EmployeeNotification> notifsToReturn = new List<EmployeeNotification>();
            foreach (EmployeeNotification notif in employeeNotifications)
            {
                if (notif.Reciver.PersonalId.Equals(user.PersonalId))
                {
                    notifsToReturn.Add(notif);
                }
            }
            return notifsToReturn;
        }

        public List<EmployeeNotification> GetSpecificTypeOfNotifications(RegisteredUser user, EmployeeNotificationType type)
        {
            ReadData();
            List<EmployeeNotification> notifsToReturn = new List<EmployeeNotification>();
            foreach (EmployeeNotification notif in employeeNotifications)
            {
                if (notif.Reciver.PersonalId.Equals(user.PersonalId) && notif.Type == type)
                {
                    notifsToReturn.Add(notif);
                }
            }
            return notifsToReturn;
        }

        public void DeleteNotification(string notificationId)
        {
            ReadData();
            int indexToRemove = -1;
            foreach (EmployeeNotification notification in employeeNotifications)
            {
                if (notification.NotificationId.Equals(notificationId))
                { 
                    indexToRemove = employeeNotifications.IndexOf(notification);
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("Notification does not exist");
            }

            employeeNotifications.RemoveAt(indexToRemove);
            dataHandler.Write(employeeNotifications);
            return;
        }
    }
}
