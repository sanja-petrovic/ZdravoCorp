using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AllRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<TimeOffRequestViewModel> Requests { get; set; }

        public AllRequestsViewModel(Doctor doctor)
        {
            Requests = new ObservableCollection<TimeOffRequestViewModel>();
        }
    }
}
