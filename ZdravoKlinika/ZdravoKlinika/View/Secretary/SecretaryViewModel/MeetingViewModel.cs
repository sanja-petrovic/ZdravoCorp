using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class MeetingViewModel : ViewModelBase
    {
        private ObservableCollection<RegisteredUser> employees;
        private RegisteredUserController registeredUserController;
        private MeetingController meetingController;
        private EmployeeNotificationController notificationController;
        private ObservableCollection<RegisteredUserPrint> meetingData;
        private List<RegisteredUser> required;
        private List<RegisteredUser> optional;
        private bool requiredRadio;
        private ICommand addCommand;
        private ICommand deleteCommand;
        private ICommand finishCommand;
        private int selectedEmployeeIndex;
        private int selectedMeetingEmployeeIndex;
        private DateTime selectedMeetingDate;
        private String selectedMeetingTime;
        private RegisteredUser thisUser;

        public ObservableCollection<RegisteredUser> Employees { get; set; }
        public ICommand AddCommand { 
            get
            {
                return addCommand ?? (addCommand = new MyICommand(() => AddToMeeting(), () => AddCanExecute)); 
            } 
        }
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new MyICommand(() => DeleteFromMeeting(), () => DeleteCanExecute));
            }
        }


        public ICommand FinishCommand
        {
            get
            {
                return finishCommand ?? (finishCommand = new MyICommand(() => FinishMeeting(), () => FinishCanExecute));
            }
        }


        public bool AddCanExecute {
            get 
            {
                return true;
            }
        }
        public bool DeleteCanExecute {
            get
            {
                return true;
            }
        }
        public bool FinishCanExecute
        {
            get
            {
                return true;
            }
        }

        public ObservableCollection<RegisteredUserPrint> MeetingData { get; set; }
        public bool RequiredRadio { get => requiredRadio; set => requiredRadio = value; }
        public int SelectedEmployeeIndex { get => selectedEmployeeIndex; set => SetProperty(ref selectedEmployeeIndex, value); }
        public int SelectedMeetingEmployeeIndex { get => selectedMeetingEmployeeIndex; set => SetProperty(ref selectedMeetingEmployeeIndex, value); }
        public DateTime SelectedMeetingDate { get => selectedMeetingDate; set => SetProperty(ref selectedMeetingDate, value); }
        public string SelectedMeetingTime { get => selectedMeetingTime; set => SetProperty(ref selectedMeetingTime, value); }
        public MeetingController MeetingController { get => meetingController; set => meetingController = value; }
        public EmployeeNotificationController NotificationController { get => notificationController; set => notificationController = value; }
        public List<RegisteredUser> Required { get => required; set => required = value; }
        public List<RegisteredUser> Optional { get => optional; set => optional = value; }
        public RegisteredUser ThisUser { get => thisUser; set => thisUser = value; }

        public class RegisteredUserPrint
        {
            private RegisteredUser user;
            string type;

            public RegisteredUserPrint(RegisteredUser user, string type)
            { 
                this.user = user;
                this.type = type;
            }

            public RegisteredUser User { get => user; set => user = value; }
            public string Type { get => type; set => type = value; }
        }

        public MeetingViewModel(RegisteredUser user) 
        {
            registeredUserController = new RegisteredUserController();
            MeetingController = new MeetingController();
            notificationController = new EmployeeNotificationController();

            Employees = new ObservableCollection<RegisteredUser>(registeredUserController.GetAllEmployees());

            MeetingData = new ObservableCollection<RegisteredUserPrint>();
            RequiredRadio = true;
            SelectedEmployeeIndex = -1;
            selectedMeetingEmployeeIndex = -1;
            selectedMeetingDate = DateTime.Now.Date;

            Required = new List<RegisteredUser>();
            Optional = new List<RegisteredUser>();
            ThisUser = user;
        }

        private void AddToMeeting()
        {
            if (SelectedEmployeeIndex == -1)
            {
                return;
            }

            string text;
            if (RequiredRadio == true)
            {
                Required.Add(Employees[selectedEmployeeIndex]);
                text = "Obavezan";
            }
            else
            {
                Optional.Add(Employees[selectedEmployeeIndex]);
                text = "Opcionalan";
            }
            RegisteredUserPrint usr = new RegisteredUserPrint(Employees[selectedEmployeeIndex], text);
            MeetingData.Add(usr);
            Employees.RemoveAt(selectedEmployeeIndex);
        }

        private void DeleteFromMeeting()
        {
            if (SelectedMeetingEmployeeIndex == -1)
            {
                return;
            }

            RegisteredUser usr = MeetingData[selectedMeetingEmployeeIndex].User;
            String text = MeetingData[selectedMeetingEmployeeIndex].Type;

            if (text.Equals("Obavezan"))
            {
                Required.Remove(usr);
            }
            else 
            {
                Optional.Remove(usr);
            }

            MeetingData.RemoveAt(SelectedMeetingEmployeeIndex);
            Employees.Add(usr);

        }

        private void FinishMeeting()
        {
            if (MeetingData.Count < 2)
                return;
            if (selectedMeetingDate.Date < DateTime.Now.Date)
                return;
            Regex regex = new Regex("[0-9]{1,2}[:][0-9]{2}");
            if (!regex.IsMatch(selectedMeetingTime))
            {
                return; 
            
            }
              
            int hours = Int32.Parse(selectedMeetingTime.Split(":")[0]);
            int minutes = Int32.Parse(selectedMeetingTime.Split(":")[1]);
            if (hours > 24 || minutes > 60)
                return;
            DateTime date = selectedMeetingDate.Date;
            date = date.AddHours(hours);
            date = date.AddMinutes(minutes);
            if (date < DateTime.Now)
                return;

            meetingController.CreateMeeting(date, Required, Optional);
            // sending notifications
            foreach (RegisteredUser user in Required)
            {
                notificationController.CreateNotification(ThisUser.PersonalId, user.PersonalId, "Novi sastanak!", "Pozvani ste na sastanak " + SelectedMeetingDate.Day + "." + SelectedMeetingDate.Month + "." + SelectedMeetingDate.Year + " u " + hours+":"+minutes+", prisustvo obavezno.", "MeetingCreated");
            }
            foreach (RegisteredUser user in Optional)
            {
                notificationController.CreateNotification(ThisUser.PersonalId, user.PersonalId, "Novi sastanak!", "Pozvani ste na sastanak " + SelectedMeetingDate.Day + "."+ SelectedMeetingDate.Month + "." + SelectedMeetingDate.Year + " u " + hours + ":" + minutes + ", prisustvo opcionalno.", "MeetingCreated");
            }

            SelectedMeetingTime = "";
            SelectedMeetingDate = DateTime.Now.Date;

            

            foreach (RegisteredUserPrint usr in MeetingData)
            {
                Employees.Add(usr.User);
            }

            MeetingData.Clear();
            Required.Clear();
            Optional.Clear();
            
        }
    }
}
