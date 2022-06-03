using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class TimeOffViewModel
    {
        ObservableCollection<TimeOffRequest> requests;
        TimeOffRequestController requestController;

        public TimeOffViewModel()
        {
            requestController = new TimeOffRequestController();
            TimeOffRequest a = new TimeOffRequest();
            requests = new ObservableCollection<TimeOffRequest>(requestController.GetAllUnprocessed());
        }

        public ObservableCollection<TimeOffRequest> Requests { get => requests; set => requests = value; }


    }
}
