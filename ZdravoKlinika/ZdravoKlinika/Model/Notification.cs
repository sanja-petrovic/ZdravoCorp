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

        private RegisteredUser receiver;
        private RegisteredUser sender;

        private bool read;
        private DateTime timeOfCreation;

        public int NotificationId { get => notificationId; set => notificationId = value; }
        public string NotificationText { get => notificationText; set => notificationText = value; }
        public RegisteredUser Receiver { get => receiver; set => receiver = value; }
        public RegisteredUser Sender { get => sender; set => sender = value; }
        public bool Read { get => read; set => read = value; }
        public DateTime TimeOfCreation { get => timeOfCreation; set => timeOfCreation = value; }
    } 
}
