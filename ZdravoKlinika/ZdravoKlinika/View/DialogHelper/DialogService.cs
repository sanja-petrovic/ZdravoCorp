using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.DoctorPages;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DialogHelper
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(string name, Action<string> callback)
        {
            
        }

        public void ShowCreateApptScheduleDialog()
        {
            CreateApptSchedule createApptSchedule = new CreateApptSchedule();
            createApptSchedule.ShowDialog();
        }

        public void ShowTimeOffDialog()
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            timeOffRequestView.ShowDialog();
        }

    }
}
