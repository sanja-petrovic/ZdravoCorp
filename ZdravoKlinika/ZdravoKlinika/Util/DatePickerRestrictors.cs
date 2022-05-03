using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZdravoKlinika.Util
{
    internal class DatePickerRestrictors
    {
        //setDatePickerBlackoutRange(getValidDateRange(new DateTime(2022,4,24), 2), datePicker);
        //setDatePickerBlackoutForward(DateTime.Today.AddDays(2),datePicker);
        public DateTime[] getValidDateRange(DateTime date, int dateRange)
        {
            if (date == null)
            {
                return null;
            }
            else
            {
                DateTime[] dates = new DateTime[2];
                dates[0] = date.AddDays(-(dateRange));
                dates[1] = date.AddDays((dateRange));
                return dates;
            }
        }
        public void setDatePickerBlackoutRange(DateTime[] dates, DatePicker picker)
        {
            picker.BlackoutDates.Clear();

            picker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, dates[0].AddDays(-1)));
            picker.BlackoutDates.Add(new CalendarDateRange(dates[1].AddDays(1), DateTime.MaxValue));

            if ((dates[0] - DateTime.Today).TotalDays <= 2)
            {
                picker.BlackoutDates.Add(new CalendarDateRange(dates[0], DateTime.Now.AddDays(2).Date));
            }
        }
        public void setDatePickerBlackoutForward(DateTime date, DatePicker picker)
        {
            picker.BlackoutDates.Clear();
            picker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, date));
        }
    }
}
