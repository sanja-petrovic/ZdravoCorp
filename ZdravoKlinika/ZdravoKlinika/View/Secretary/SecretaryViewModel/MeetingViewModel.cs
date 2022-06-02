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
        private int selectedEmployeeIndex;

        public ObservableCollection<RegisteredUser> Employees { get; set; }
        public ICommand AddCommand { 
            get
            {
                return addCommand ?? (addCommand = new MyICommand(() => AddToMeeting(), () => AddCanExecute)); 
            } 
        }

        public bool AddCanExecute {
            get 
            {
                return true;
            }
        }
        public ObservableCollection<RegisteredUserPrint> MeetingData { get; set; }
        public bool RequiredRadio { get => requiredRadio; set => requiredRadio = value; }
        public int SelectedEmployeeIndex { get => selectedEmployeeIndex; set => selectedEmployeeIndex = value; }

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

            required = new List<RegisteredUser>();
            optional = new List<RegisteredUser>();
        }

        private void UpdateMeetingList()
        {
        }

        public void Test(object target, ExecutedRoutedEventArgs e)
        {
            required.Add(employees[0]);
            UpdateMeetingList();
        }
        private void AddToMeeting()
        {
            string ab;
            if (RequiredRadio == true)
            {
                ab = "Obavezan";
            }
            else { 
                ab = "Opcionalan";
            }
            if (selectedEmployeeIndex == -1)
            {
                return;
            }

            RegisteredUserPrint a = new RegisteredUserPrint(Employees[selectedEmployeeIndex], ab);
            MeetingData.Add(a);
            Employees.RemoveAt(selectedEmployeeIndex);
        }
    }
}
