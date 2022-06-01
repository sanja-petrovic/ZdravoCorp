using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Meeting
    {
        private String meetingId;
        private DateTime date;
        private List<RegisteredUser> required;
        private List<RegisteredUser> optional;

        public DateTime Date { get => date; set => date = value; }
        public List<RegisteredUser> Required { get => required; set => required = value; }
        public List<RegisteredUser> Optional { get => optional; set => optional = value; }
        public string MeetingId { get => meetingId; set => meetingId = value; }

        public Meeting()
        { 
        }

    }
}
