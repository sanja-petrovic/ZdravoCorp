using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class MeetingController
    {
        private MeetingService meetingService;

        public MeetingController()
        { 
            meetingService = new MeetingService();
        }

        public void CreateMeeting(DateTime date, List<RegisteredUser> required, List<RegisteredUser> optional) 
        {
            Meeting meeting = new Meeting();
            meeting.Required = required;
            meeting.Date = date;
            meeting.Optional = optional;
            meetingService.CreateMeeting(meeting);
        }

        public List<Meeting> GetAll() 
        {
            return meetingService.GetAll();
        }

        public void DeleteMeeting(String meetingId)
        { 
            meetingService.DeleteMeeting(meetingId);
            return;
        }
    }
}
