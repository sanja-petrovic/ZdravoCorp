using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoKlinika.View.Manager.Views
{
    /// <summary>
    /// Interaction logic for RoomReportUserControl.xaml
    /// </summary>
    public partial class RoomReportUserControl : UserControl
    {
        private List<Appointment> appointments;
        private Room room;
        private DateTime startDate;
        private DateTime endDate;
        private string text;

        public RoomReportUserControl(List<Appointment> apps, Room r, DateTime start, DateTime end)
        {
            InitializeComponent();
            Appointments = apps;
            Room = r;
            StartDate = start;
            EndDate = end;
            text = "Izveštaj o zauzetosti prostorije " + r.Name + " za period od " + StartDate.ToShortDateString() + " do " + EndDate.ToShortDateString() + "." ;
            infoLabel.Content = text;
            dataGridAppointments.ItemsSource = Appointments;
        }

        public List<Appointment> Appointments { get => appointments; set => appointments = value; }
        public Room Room { get => room; set => room = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
    }
}
