using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class MeetingRepository
    {
        private MeetingDataHandler dataHandler;
        private List<Meeting> meetings;
        public MeetingRepository()
        { 
            dataHandler = new MeetingDataHandler();
            ReadData();
        }

        private void ReadData() 
        {
            meetings = dataHandler.Read();
            if (meetings == null)
            {
                meetings = new List<Meeting>();
            }
            return;
        }

        public void CreateMeeting(Meeting meeting) 
        {
            ReadData();
            meetings.Add(meeting);
            dataHandler.Write(meetings);
            return;
        }

        public List<Meeting> GetAll() 
        {
            ReadData();
            return meetings;
        }

        public void DeleteMeeting(String id)
        {
            ReadData();
            int indexToRemove = -1;
            foreach (Meeting meeting in meetings)
            {
                if (meeting.MeetingId.Equals(id))
                {
                    indexToRemove = meetings.IndexOf(meeting);
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("Meeting does not exist");
            }

            meetings.RemoveAt(indexToRemove);
            dataHandler.Write(meetings);
            return;
        }

    }
}
