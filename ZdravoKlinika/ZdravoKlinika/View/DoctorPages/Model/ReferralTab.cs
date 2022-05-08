using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ReferralTab : ViewModelBase, ITabViewModel
    {
        private string header;
        private int appointmentId;

        public string Header { get => header; set => SetProperty(ref header, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }


        public ReferralTab() { }

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}
