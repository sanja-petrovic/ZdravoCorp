using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.Model
{
    public class EmployeeNotification : Notification
    {
        private EmployeeNotificationType type;
        private String notificationTitle;
        private String notificationId;

        public EmployeeNotificationType Type { get => type; set => type = value; }
        public string NotificationTitle { get => notificationTitle; set => notificationTitle = value; }
        public new string NotificationId { get => notificationId; set => notificationId = value; }

        public EmployeeNotification()
        {
            
        }

    }
}
