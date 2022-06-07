using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerProfileViewModel : ManagerViewModelBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private ManagerInfoViewModel infoViewModel = new ManagerInfoViewModel();
        private ManagerChangePasswordViewModel changePasswordViewModel = new ManagerChangePasswordViewModel();
        private ManagerFeedbackViewModel feedbackViewModel = new ManagerFeedbackViewModel();
        private ManagerLogoutViewModel logoutViewModel = new ManagerLogoutViewModel();
        private ManagerViewModelBase currentViewModel;

        public ManagerViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public ManagerProfileViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = infoViewModel;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "info":
                    CurrentViewModel = infoViewModel;
                    break;
                case "changePassword":
                    CurrentViewModel = changePasswordViewModel;
                    break;
                case "feedback":
                    CurrentViewModel = feedbackViewModel;
                    break;
                case "logout":
                    CurrentViewModel = logoutViewModel;
                    break;
            }
        }
    }
}
