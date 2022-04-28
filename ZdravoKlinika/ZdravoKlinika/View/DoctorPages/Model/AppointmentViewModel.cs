using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class AppointmentViewModel : ViewModelBase
    {
        private string name;
        private string type;
        private string time;
        private string room;

        public AppointmentViewModel()
        {
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }
        public string Time { get => time; set => SetProperty(ref time, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }


    }
}
