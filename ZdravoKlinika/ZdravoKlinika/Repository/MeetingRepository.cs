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
            ReadDataFromFile();
        }

        private void ReadDataFromFile() 
        {
            meetings = dataHandler.Read();
            if (meetings == null)
            {
                meetings = new List<Meeting>();
            }
            return;
        }

        public void Add(Meeting meeting) 
        {
            ReadDataFromFile();
            meetings.Add(meeting);
            dataHandler.Write(meetings);
            return;
        }

        public List<Meeting> GetAll() 
        {
            ReadDataFromFile();
            return meetings;
        }

        public void Remove(String id)
        {
            ReadDataFromFile();
            int indexToRemove = GetIndex(id);
            meetings.RemoveAt(indexToRemove);
            dataHandler.Write(meetings);
            return;
        }

        private int GetIndex(String id) 
        {
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
            return indexToRemove;
        }

    }
}
