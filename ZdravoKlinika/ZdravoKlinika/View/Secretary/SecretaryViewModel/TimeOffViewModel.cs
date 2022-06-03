using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class TimeOffViewModel : ViewModelBase
    {
        private ObservableCollection<TimeOffRequest> requests;
        private TimeOffRequestController requestController;
        private EmployeeNotificationController employeeNotificationController;
        private RegisteredUser usr;
        private Visibility commentValue;
        private String commentText;
        private bool goodToGo;
        private int selectedIndex;

        private ICommand denyCommand;
        private ICommand acceptCommand;
        private ICommand goBack;
        private ICommand finishCommand;

        public ICommand FinishCommand
        {
            get
            {
                return finishCommand ?? (finishCommand = new MyICommand(() => ProcessRequest(), () => FinishCommandCanExecute));
            }
        }

        public ICommand GoBack
        {
            get
            {
                return goBack ?? (goBack = new MyICommand(() => GoBackMethod(), () => GoBackCanExecute));
            }
        }

        public ICommand AcceptCommand
        {
            get
            {
                return acceptCommand ?? (acceptCommand = new MyICommand(() => AcceptRequest(), () => AcceptCanExecute));
            }
        }

        public ICommand DenyCommand
        {
            get
            {
                return denyCommand ?? (denyCommand = new MyICommand(() => DenyRequest(), () => DenyCanExecute));
            }
        }

        public TimeOffViewModel(RegisteredUser usr)
        {
            requestController = new TimeOffRequestController();
            employeeNotificationController = new EmployeeNotificationController();
            TimeOffRequest a = new TimeOffRequest();
            requests = new ObservableCollection<TimeOffRequest>(requestController.GetAllUnprocessed());
            CommentValue = Visibility.Collapsed;
            commentText = "";
            SelectedIndex = -1;
            goodToGo = false;
            this.usr = usr;
        }

        public ObservableCollection<TimeOffRequest> Requests { get => requests; set => requests = value; }
        public Visibility CommentValue { get => commentValue; set => SetProperty(ref commentValue, value); }
        public string CommentText { get => commentText; set => SetProperty(ref commentText, value); }
        public bool GoodToGo { get => goodToGo; set => goodToGo = value; }
        public bool DenyCanExecute { get => true; }
        public bool AcceptCanExecute { get => true; }
        public bool GoBackCanExecute { get => true; }
        public bool FinishCommandCanExecute { get => true; }
        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        private void DenyRequest()
        {
            if (SelectedIndex == -1)
                return;
            GoodToGo = false;
            CommentValue = Visibility.Visible;
        }

        private void AcceptRequest()
        {
            if (SelectedIndex == -1)
                return;
            GoodToGo = true;
            CommentValue = Visibility.Visible;
        }

        private void GoBackMethod()
        {
            CommentText = "";
            CommentValue = Visibility.Collapsed;
        }

        private void ProcessRequest()
        {
            if (CommentText.Equals(""))
            {
                return;
            }

            CommentValue = Visibility.Collapsed;
            RequestState state;
            string text;
            if (goodToGo) 
            {
                state = RequestState.Approved;
                text = "Odobren";
            }
            else 
            {
                state = RequestState.Denied;
                text = "Obdijen";
            }
            requestController.ProcessRequest(Requests[selectedIndex].Id,CommentText,state);
            String notifText = "Vaš zahtev je " + text + " uz komentar: " + commentText + ".";
            employeeNotificationController.CreateNotification(usr.PersonalId, Requests[SelectedIndex].Doctor.PersonalId, "Zahtev je obrađen", notifText, "TimeOffProcessed");
            Requests.RemoveAt(SelectedIndex);
        }
    }
}
