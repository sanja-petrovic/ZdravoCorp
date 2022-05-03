using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Util
{
    internal class ListHelper
    {
        public Appointment NextAppointment(List<Appointment> list, Appointment item)
        {
            var indexOf = list.IndexOf(item);
            return list[indexOf == list.Count - 1 ? 0 : indexOf + 1];
        }
    }
}
