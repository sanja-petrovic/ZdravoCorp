using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class EmployeeNotificationService
    {
        EmployeeNotificationRepository notificationRepository;

        public EmployeeNotificationService()
        { 
            notificationRepository = new EmployeeNotificationRepository();
        }

        public void SendNotification(EmployeeNotification notification) 
        {
            notification.NotificationId = GetFreeNotificationId();
            notificationRepository.Add(notification);
        }

        public List<EmployeeNotification> GetAll() 
        {
            return notificationRepository.GetAll();
        }

        public List<EmployeeNotification> GetAllPersonalNotifications(RegisteredUser user)
        {
            return notificationRepository.GetAllPersonalNotifications(user);
        }

        public List<EmployeeNotification> GetSpecificTypeOfNotifications(RegisteredUser user, EmployeeNotificationType type)
        {
            return notificationRepository.GetSpecificTypeOfNotifications(user, type);
        }

        public void Remove(String notificationId)
        {
            notificationRepository.Remove(GetById(notificationId));
        }

        public EmployeeNotification GetById(String id)
        {
            return notificationRepository.GetById(id);
        }

        private String GetFreeNotificationId()
        { 
            bool notificationIdIsUnique = false;
            String newId = "";
            while (!notificationIdIsUnique)
            {
                List<EmployeeNotification> notifs = GetAll();
                newId = Util.IdGenerator.Generate();
                notificationIdIsUnique = true;
                foreach (EmployeeNotification notification in notifs)
                {
                    if (notification.NotificationId.Equals(newId))
                    {
                        notificationIdIsUnique = false;
                        break;
                    }
                }
            }
            return newId;
        }

        public void MarkAsRead(string notificationId)
        {
            notificationRepository.MarkAsRead(notificationRepository.GetById(notificationId));
        }

        public void MarkAllPersonalNotificationsAsRead(RegisteredUser user)
        {
            notificationRepository.MarkAllPersonalNotificationsAsRead(user);
        }

        public bool HasEveryNotifBeenRead(RegisteredUser user)
        {
            return notificationRepository.HasEveryNotifBeenRead(user);
        }

    }
}
