using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Util
{
    public class DateBlock
    {
        DateTime start;
        DateTime end;
        int duration;//number of minimal intervals
        static int minInterval = 15; //minutes

        public DateTime Start { get => start; set => start = value; }
        public int Duration { get => duration; set => duration = value; }
        public DateTime End { get => end; set => end = value; }

        public DateBlock()
        {

        }

        public DateBlock(DateTime start, DateTime end)
        {
            this.end = end;
            this.start = start; 
        }
        public DateBlock(DateTime start, int duration)
        {
            this.start = start;
            this.duration = duration;
            this.end = start.AddMinutes(duration);
        }
        static public List<DateBlock> getIntervals(DateTime start, DateTime end)
        {
            if(end < start)
            {
                end = end.AddDays(1);
            }
            List<DateBlock> result = new List<DateBlock>();
            int numOfIntervals = (int)((end - start).TotalMinutes / minInterval);
            if (numOfIntervals > 0)
            {
                for (int i = 0; i < numOfIntervals; i++)
                {
                    result.Add(new DateBlock(start.AddMinutes(i * minInterval), minInterval));
                }
            }
            return result;
        }

        static public List<DateBlock> GetIntervals(DateTime start, DateTime end, int interval=15)
        {

            List<DateBlock> result = new List<DateBlock>();
            int numOfIntervals = (int)((end - start).TotalMinutes / interval);
            if (numOfIntervals > 0)
            {
                for (int i = 0; i < numOfIntervals; i++)
                {
                    result.Add(new DateBlock(start.AddMinutes(i * interval), start.AddMinutes(i * interval + interval)));
                }
            }
            return result;
        }

        public static List<DateBlock> getIntervals(DateBlock block)
        {
            List<DateBlock> result = new List<DateBlock>();
            int numOfIntervals = (int)((block.start.AddMinutes(block.duration) - block.start).TotalMinutes / minInterval);
            if(numOfIntervals > 0)
            {
                for (int i = 0; i < numOfIntervals; i++)
                {
                    result.Add(new DateBlock(block.start.AddMinutes(i * minInterval), minInterval));
                }
            }
            return result;
        }
        static public List<DateTime> getStartTimes(List<DateBlock> input)
        {
            List<DateTime> result = new List<DateTime>();
            foreach(DateBlock db in input)
            {
                result.Add(db.start);
            }
            return result;
        }
        static public List<DateTime> GetHourlyIntervals(DateTime start, DateTime end) // includes both start and end [start, end]
        {
            List<DateTime> retVal = new List<DateTime>();
            int currHour = 0;
            while((start.AddHours(currHour) - end).TotalHours <= 0)
            {
                retVal.Add(start.AddHours(currHour));
                currHour++;
            }
            return retVal;
        }
        public static List<DateBlock> getIntersection(DateBlock a, DateBlock b)
        {
            List<DateBlock> result= new List<DateBlock>();
            List<DateBlock> bIntervals = DateBlock.getIntervals(b.Start, b.Start.AddMinutes(b.duration));
            foreach (DateBlock block in DateBlock.getIntervals(a.Start, a.Start.AddMinutes(a.duration)))
            {
                foreach (DateBlock db in bIntervals)
                {
                    if (db.start.Equals(block.start) && db.duration.Equals(block.duration))
                    {
                        result.Add(db);
                    }
                }
            }
            return result;
        }
        public static List<DateBlock> getIntersection(List<DateBlock> a, List<DateBlock> b)
        {
            List<DateBlock> result = new List<DateBlock>();
            
            foreach (DateBlock block in a)
            {
                foreach (DateBlock db in b)
                {
                    if (db.start.Equals(block.start) && db.duration.Equals(block.duration))
                    {
                        result.Add(db);
                    }
                }
            }
            return result;
        }
        public static bool ContainsDateTime(DateBlock block, DateTime time, int duration)
        {
            List<DateBlock> listBlocks = DateBlock.getIntervals(block);
            int numOfIntervals = duration / minInterval;

            for(int j=0; j< listBlocks.Count(); j++)
            {
                if (listBlocks[j].start.Equals(time))
                {
                    //contains start time, does it contain enough intervals
                    for (int i = 1; i < numOfIntervals; i++)
                    {
                        if (!listBlocks[j + i].start.Equals(time.AddMinutes(minInterval * i)))
                        {
                            return false;
                        }
                    }
                    return true;
                }

            }
            return false;
        }

        public static bool ContainsDateTime(DateTime start,DateTime end, DateTime time, int duration)
        {
            List<DateBlock> listBlocks = DateBlock.getIntervals(start,end);
            int numOfIntervals = duration / minInterval;

            for (int j = 0; j < listBlocks.Count(); j++)
            {
                if (listBlocks[j].start.Equals(time))
                {
                    //contains start time, does it contain enough intervals
                    for (int i = 1; i < numOfIntervals; i++)
                    {
                        if (!listBlocks[j + i].start.Equals(time.AddMinutes(minInterval * i)))
                        {
                            return false;
                        }
                    }
                    return true;
                }

            }
            return false;
        }

        public static bool DateBlocksIntersect(DateBlock a, DateBlock b) 
        {//period - a apptdateblock - b
            bool retVal = false;
            if (a.End < b.Start)
            {
                retVal = false;
            }
            if (a.Start < b.End && b.Start < a.End)
            {
                retVal = true;
            }

            return retVal;
        }

        public override string ToString()
        {
            return Start.ToString("HH:mm");
        }
    }
}
