using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    internal class AppointmentViewModel : ViewModelBase
    {
        private int id;
        private string name;
        private string type;
        private string time;
        private string room;
        private string diagnosis;
        private string prescriptions;
        private string opinion;

        public AppointmentViewModel()
        {
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }
        public string Time { get => time; set => SetProperty(ref time, value); }
        public string Room { get => room; set => SetProperty(ref room, value); }
        public int Id { get => id; set => id = value; }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string Prescriptions { get => prescriptions; set => SetProperty(ref prescriptions, value); }
        public string Opinion { get => opinion; set => SetProperty(ref opinion, value); }


    }
}
