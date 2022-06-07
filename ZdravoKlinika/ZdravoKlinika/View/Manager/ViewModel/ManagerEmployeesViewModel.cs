using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerEmployeesViewModel : ManagerViewModelBase
    {
        private RegisteredUserController registeredUserController;
        private ObservableCollection<RegisteredUser> employees;

        public ObservableCollection<RegisteredUser> Employees { get => employees; set => employees = value; }

        public ManagerEmployeesViewModel()
        {
            this.registeredUserController = new RegisteredUserController();
            Employees = new ObservableCollection<RegisteredUser>(this.registeredUserController.GetAllEmployees());
        }
    }
}
