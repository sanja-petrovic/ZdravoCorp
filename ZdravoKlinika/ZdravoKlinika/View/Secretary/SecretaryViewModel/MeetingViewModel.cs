using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class MeetingViewModel : ViewModelBase
    {
        private ObservableCollection<RegisteredUser> employees;
        private RegisteredUserController registeredUserController;
        private ObservableCollection<RegisteredUserPrint> meetingData;
        private List<RegisteredUser> required;
        private List<RegisteredUser> optional;
        private bool requiredRadio;
        private ICommand addCommand;
        private ICommand deleteCommand;
        private ICommand finishCommand;
        private int selectedEmployeeIndex;
        private int selectedMeetingEmployeeIndex;

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
                return addCommand ?? (addCommand = new MyICommand(() => FinishMeeting(), () => FinishCanExecute));
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
                if (MeetingData.Count >= 2)
                { 
                    return true;
                }
                return false;
            }
        }

        public ObservableCollection<RegisteredUserPrint> MeetingData { get; set; }
        public bool RequiredRadio { get => requiredRadio; set => requiredRadio = value; }
        public int SelectedEmployeeIndex { get => selectedEmployeeIndex; set => SetProperty(ref selectedEmployeeIndex, value); }
        public int SelectedMeetingEmployeeIndex { get => selectedMeetingEmployeeIndex; set => SetProperty(ref selectedMeetingEmployeeIndex, value); }

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

        public MeetingViewModel() 
        {
            registeredUserController = new RegisteredUserController();
            Employees = new ObservableCollection<RegisteredUser>(registeredUserController.GetAllEmployees());

            MeetingData = new ObservableCollection<RegisteredUserPrint>();
            RequiredRadio = true;
            SelectedEmployeeIndex = -1;
            selectedMeetingEmployeeIndex = -1;

            required = new List<RegisteredUser>();
            optional = new List<RegisteredUser>();
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
                required.Add(Employees[selectedEmployeeIndex]);
                text = "Obavezan";
            }
            else
            {
                optional.Add(Employees[selectedEmployeeIndex]);
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
                required.Remove(usr);
            }
            else 
            {
                optional.Remove(usr);
            }

            MeetingData.RemoveAt(SelectedMeetingEmployeeIndex);
            Employees.Add(usr);

        }

        private void FinishMeeting()
        {
            throw new NotImplementedException();
        }
    }
}
