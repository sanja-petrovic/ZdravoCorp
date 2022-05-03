using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Notification
    {
        private int notificationId;
        private string notificationText;

        private RegisteredUser reciver;
        private RegisteredUser sender;

        public int NotificationId { get => notificationId; set => notificationId = value; }
        public string NotificationText { get => notificationText; set => notificationText = value; }
        public RegisteredUser Reciver { get => reciver; set => reciver = value; }
        public RegisteredUser Sender { get => sender; set => sender = value; }
    }
}
