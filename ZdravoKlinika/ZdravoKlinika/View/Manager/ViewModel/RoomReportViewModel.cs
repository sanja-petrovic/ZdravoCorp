using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class RoomReportViewModel : ManagerViewModelBase
    {
        private List<Appointment> appointments;
        private Room room;
        private DateTime startDate;
        private DateTime endDate;

        public RoomReportViewModel(List<Appointment> apps, Room r, DateTime start, DateTime end)
        {
            Appointments = apps;
            Room = r;
            StartDate = start;
            EndDate = end;
        }

        public List<Appointment> Appointments { get => appointments; set => appointments = value; }
        public Room Room { get => room; set => room = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
    }
}
