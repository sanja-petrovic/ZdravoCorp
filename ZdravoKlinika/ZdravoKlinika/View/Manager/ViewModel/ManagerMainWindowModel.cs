using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerMainWindowModel : ManagerViewModelBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private ManagerHomeViewModel homeViewModel = new ManagerHomeViewModel();
        private ManagerEmployeesViewModel employeesViewModel = new ManagerEmployeesViewModel();
        private ManagerRoomsViewModel roomsViewModel = new ManagerRoomsViewModel();
        private ManagerInventoryViewModel inventoryViewModel = new ManagerInventoryViewModel();
        private ManagerScheduleViewModel scheduleViewModel = new ManagerScheduleViewModel();
        private ManagerHelpViewModel helpViewModel = new ManagerHelpViewModel();
        private ManagerProfileViewModel profileViewModel = new ManagerProfileViewModel();
        private ManagerViewModelBase currentViewModel;
        private string page;
        private string currentTime;

        public ManagerMainWindowModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homeViewModel;
            Page = "Početna strana";
            CurrentTime = DateTime.Now.ToShortTimeString();
            StartClock();
        }

        public ManagerViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public string Page
        {
            get { return page; }
            set
            {
                SetProperty(ref page, value);
            }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                SetProperty(ref currentTime, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "employees":
                    Page = "Zaposleni";
                    CurrentViewModel = employeesViewModel;                   
                    break;
                case "rooms":
                    Page = "Prostorije";
                    CurrentViewModel = roomsViewModel;                
                    break;
                case "inventory":
                    Page = "Inventar";
                    CurrentViewModel = inventoryViewModel;                  
                    break;
                case "schedules":
                    Page = "Rasporedi";
                    CurrentViewModel = scheduleViewModel;                   
                    break;
                case "help":
                    Page = "Pomoć";
                    CurrentViewModel = helpViewModel;                   
                    break;
                case "profile":
                    Page = "Profil";
                    CurrentViewModel = profileViewModel;
                    break;
            }
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                UpdateTime();
            };
        }

        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToShortTimeString();
        }
    }
}
