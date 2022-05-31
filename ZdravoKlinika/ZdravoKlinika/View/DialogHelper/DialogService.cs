using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.DoctorPages;
using ZdravoKlinika.Controller;
using System.Windows;
using System.ComponentModel;

namespace ZdravoKlinika.View.DialogHelper
{
    public class DialogService
    {
        private static List<Window> openDialogs;

        public List<Window> OpenDialogs { get => openDialogs; set => openDialogs = value; }

        public DialogService()
        {
            if(openDialogs == null) openDialogs = new List<Window>();
        }

        public void ShowCreateApptScheduleDialog()
        {
            CreateApptSchedule createApptSchedule = new CreateApptSchedule();
            createApptSchedule.Show();
            openDialogs.Add(createApptSchedule);
        }

        public void CloseDialog(ViewModelBase viewModel)
        {
            int index = -1;
            foreach (Window w in openDialogs)
            {
                if(w.DataContext == viewModel)
                {
                    w.Close();
                    index = openDialogs.IndexOf(w);
                    break;
                }
            }

            if (index != -1)
            {
                openDialogs.RemoveAt(index);
            }
        }

        public void ShowTimeOffDialog()
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            timeOffRequestView.Show();
            OpenDialogs.Add(timeOffRequestView);
        }

        public void ShowMedRequestDialog(int requestId)
        {
            ApproveMedView approveMedView = new ApproveMedView(requestId);
            approveMedView.Show();
            OpenDialogs.Add(approveMedView);
        }


        public void ShowLogApptDialog(int apptId)
        {
            LogAppointmentDialog logAppointmentDialog = new LogAppointmentDialog { SelectedAppointmentId = apptId };
            logAppointmentDialog.Init();
            logAppointmentDialog.Show();
            OpenDialogs.Add(logAppointmentDialog);
        }

    }
}
