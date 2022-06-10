using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        private EmployeeNotificationController controller;
        private RegisteredUser user;
        private ObservableCollection<EmployeeNotification> requests;
        private ICommand readCommand;
        private int selectedIndex;
        public MainMenuViewModel(RegisteredUser user) 
        {
            controller = new EmployeeNotificationController();
            this.user = user;
            Requests = new ObservableCollection<EmployeeNotification>(controller.GetAllPersonalNotifications(user.PersonalId));
            SelectedIndex = -1;
        }

        public ObservableCollection<EmployeeNotification> Requests { get => requests; set => requests = value; }
        public ICommand ReadCommand
        {
            get
            {
                return readCommand ?? (readCommand = new MyICommand(() => ReadNotification(), () => ReadCanExecute));
            }
        }

        private void ReadNotification()
        {
            if (SelectedIndex == -1)
            {
                return;
            }
             
            Requests.RemoveAt(SelectedIndex);
        }

        public bool ReadCanExecute { get => true;}
        public int SelectedIndex { get => selectedIndex; set => SetProperty(ref selectedIndex ,value); }
    }
}
