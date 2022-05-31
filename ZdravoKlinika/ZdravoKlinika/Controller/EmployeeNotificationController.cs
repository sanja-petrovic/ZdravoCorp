using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class EmployeeNotificationController
    {
        EmployeeNotificationService notificationService;
        RegisteredUserService registeredUserService;

        public EmployeeNotificationController()
        {
            notificationService = new EmployeeNotificationService();
            registeredUserService = new RegisteredUserService();
        }

        public void CreateNotification(String senderId, String receiverId, String notificationTitle, String notificationText, String typeOfNotification)
        {
            EmployeeNotification notification = new EmployeeNotification();
            RegisteredUser? sender = registeredUserService.GetUserById(senderId);
            RegisteredUser? receiver = registeredUserService.GetUserById(receiverId);

            if (sender == null || receiver == null)
            {
                throw new Exception("Bad receiver or sender id");
            }

            notification.NotificationTitle = notificationTitle;
            notification.NotificationText = notificationText;
            notification.Sender = sender;
            notification.Reciver = receiver;
            notification.Type = DecodeNotificationType(typeOfNotification);

            notificationService.SendNotification(notification);
        }

        private EmployeeNotificationType DecodeNotificationType(String stringToDecode) 
        {
            EmployeeNotificationType type = EmployeeNotificationType.Unknown;
            if (stringToDecode.Equals("MeetingCreated"))
            {
                type = EmployeeNotificationType.MeetingCreated;
            }
            else if (stringToDecode.Equals("TimeOffProcessed"))
            {
                type = EmployeeNotificationType.TimeOffProcessed;
            }
            else if (stringToDecode.Equals("TimeOffCreated"))
            {
                type = EmployeeNotificationType.TimeOffCreated;
            }
            return type;
        }


        public List<EmployeeNotification> GetAll()
        {
            return notificationService.GetAll();
        }

        public List<EmployeeNotification> GetAllPersonalNotifications(String userId)
        {
            RegisteredUser? user = registeredUserService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Bad user id");
            }
            return notificationService.GetAllPersonalNotifications(user);
        }

        public List<EmployeeNotification> GetSpecificTypeOfNotifications(String userId, String typeString)
        {
            RegisteredUser? user = registeredUserService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Bad user id");
            }
            EmployeeNotificationType type = DecodeNotificationType(typeString);
            return notificationService.GetSpecificTypeOfNotifications(user, type);
        }

        public void DeleteNotification(String notificationId)
        {
            notificationService.DeleteNotification(notificationId);
        }

    }
}
