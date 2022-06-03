using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class MeetingService
    {
        MeetingRepository meetingRepository;

        public MeetingService()
        {
            meetingRepository = new MeetingRepository();
        }

        public void CreateMeeting(Meeting meeting)
        { 
            meeting.MeetingId = GetFreeMeetingId();
            meetingRepository.CreateMeeting(meeting);
        }

        public List<Meeting> GetAll()
        {
            return meetingRepository.GetAll();
        }

        public void DeleteMeeting(String meetingId)
        {
            meetingRepository.DeleteMeeting(meetingId);
        }

        private String GetFreeMeetingId()
        {
            bool meetingIdIsUnique = false;
            String newId = "";
            while (!meetingIdIsUnique)
            {
                List<Meeting> meetings = GetAll();
                newId = Util.IdGenerator.Generate();
                meetingIdIsUnique = true;
                foreach (Meeting meeting in meetings)
                {
                    if (meeting.MeetingId.Equals(newId))
                    {
                        meetingIdIsUnique = false;
                        break;
                    }
                }
            }
            return newId;
        }
    }
}
