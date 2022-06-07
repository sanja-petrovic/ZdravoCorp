using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class PatientNotes : Notification
    {
        private DateTime trigger;

        public PatientNotes() {
            this.NotificationId = -1;
        }
        public PatientNotes(int id, DateTime trigger, RegisteredUser patient, String note)
        {
            this.NotificationId = id;
            this.Sender = patient;
            this.Receiver = patient;
            this.NotificationText = note;
            this.Trigger = trigger;
        }


        public DateTime Trigger { get => trigger; set => trigger = value; }
    }
}
