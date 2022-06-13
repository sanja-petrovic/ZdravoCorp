using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.DialogHelper;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PromptViewModel : ViewModelBase
    {
        private string title;
        private string message;
        private int appointmentId;
        private DialogHelper.DialogService dialogService;
        private Action action;
        private string confirmText;
        private string giveUpText;

        public MyICommand ConfirmCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }
        

        private AppointmentController appointmentController;

        public PromptViewModel(string title, string message, Action action, string confirmText="Potvrdite", string giveUpText="Odustanite")
        {
            Title = title;
            Message = message;
            AppointmentId = appointmentId;
            DialogService = new DialogHelper.DialogService();
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
            Action = action;
            ConfirmText = confirmText;
            GiveUpText = giveUpText;
        }

        public virtual void ExecuteConfirm()
        {
            action();
            DialogService.CloseDialog(this);

        }

        public void ExecuteGiveUp()
        {
            DialogService.CloseDialog(this);
        }

        public string Title { get => title; set => title = value; }
        public string Message { get => message; set => message = value; }
        public AppointmentController AppointmentController { get => appointmentController; set => appointmentController = value; }
        public int AppointmentId { get => appointmentId; set => appointmentId = value; }
        public Action Action { get => action; set => action = value; }
        public DialogService DialogService { get => dialogService; set => dialogService = value; }
        public string ConfirmText { get => confirmText; set => confirmText = value; }
        public string GiveUpText { get => giveUpText; set => giveUpText = value; }
    }
}
