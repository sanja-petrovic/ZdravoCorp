using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class NotifPanelViewModel : ViewModelBase
    {
        private ObservableCollection<NotifViewModel> notifications;
        private int notificationsCount;
        private int height;
        private int borderHeight;
        private string title;
        private Visibility noNotifs;

        public ObservableCollection<NotifViewModel> Notifications { get => notifications; set => SetProperty(ref notifications, value); }
        public int NotificationsCount { get => notificationsCount; set => SetProperty(ref notificationsCount, value); }
        public int Height { get => height; set => SetProperty(ref height, value); }
        public int BorderHeight { get => borderHeight; set => SetProperty(ref borderHeight, value); }
        public string Title { get => title; set => SetProperty(ref title, value); }

        private Controller.EmployeeNotificationController controller;

        public MyICommand ClearCommand { get; set; }
        public Visibility NoNotifs { get => noNotifs; set => SetProperty(ref noNotifs, value); }

        public NotifPanelViewModel()
        {
            controller = new Controller.EmployeeNotificationController();
            //var notifs = this.controller.GetAllPersonalNotifications(Controller.RegisteredUserController.UserToDoctor(App.User).PersonalId);
            Notifications = new ObservableCollection<NotifViewModel>();
            /*foreach(ZdravoKlinika.Model.EmployeeNotification notif in notifs)
            {
                Notifications.Add(new NotifViewModel { Id = notif.NotificationId, Title = notif.NotificationTitle, Text = notif.NotificationText, NotifTime = notif.TimeOfCreation.ToString("dd.MM.yyyy. HH:mm"), Read = notif.Read });
                if(!notif.Read)
                {
                    NotificationsCount++;
                }
            }*/
            ClearCommand = new MyICommand(ClearNotifs);
            Height = Notifications.Count > 0 ? Notifications.Count * 115: 115;
            BorderHeight = Notifications.Count > 0 ? Notifications.Count * 110 : 110;
            Title = NotificationsCount <= 0 ? "Obaveštenja" : "Obaveštenja (" + NotificationsCount + ")";
            NoNotifs = Notifications.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public void MarkAllAsRead()
        {
            foreach(NotifViewModel nvm in Notifications)
            {
                nvm.Read = true;
            }
            NotificationsCount = 0;
            Title = "Obaveštenja";
        }

        public void ClearNotifs()
        {
            /*foreach(NotifViewModel notif in Notifications)
            {
                controller.DeleteNotification(notif.Id);
            }*/
            Notifications.Clear();
            NoNotifs = Visibility.Visible;
            Height = 115;
            BorderHeight = 110;
            Title = "Obaveštenja";

        }
    }
}
