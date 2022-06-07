using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Model;
using System.Windows.Controls;
using System.Windows.Media;
using Enterwell.Clients.Wpf.Notifications;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.Messenger
{
    public class Messenger
    {
        public static DoctorPages.Model.MainViewModel DoctorBase { get; set; }

        public Messenger(MainViewModel doctorBase)
        {
            DoctorBase = doctorBase;
        }

        public Messenger() { }

        public static void SuccessMessage(string message)
        {
            DoctorBase.LoadSuccessMessage(message);
        }


    }
}
