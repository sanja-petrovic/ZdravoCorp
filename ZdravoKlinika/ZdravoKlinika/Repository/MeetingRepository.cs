using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class MeetingRepository : Interfaces.IMeetingRepository
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

        public void Remove(Meeting meeting)
        {
            ReadDataFromFile();
            int indexToRemove = GetIndex(meeting.MeetingId);
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

        public Meeting GetById(string id)
        {
            return meetings.Find(x => x.MeetingId.Equals(id));
        }

        public void Update(Meeting item)
        {
            int index = GetIndex(item.MeetingId);
            if (index != -1)
            {
                meetings[index] = item;
                dataHandler.Write(meetings);
            }
        }

        public void RemoveAll()
        {
            if (meetings != null)
                meetings.Clear();
            dataHandler.Write(meetings);
        }
    }
}
