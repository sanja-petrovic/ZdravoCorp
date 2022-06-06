using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class NotifViewModel : ViewModelBase
    {
        private string id;
        private string title;
        private string text;
        private string notifTime;
        private SolidColorBrush backgroundColor;
        private bool read;

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Text { get => text; set => SetProperty(ref text, value); }
        public string NotifTime { get => notifTime; set => SetProperty(ref notifTime, value); }
        public string Id { get => id; set => SetProperty(ref id, value); }
        public SolidColorBrush BackgroundColor { get => backgroundColor; set => SetProperty(ref backgroundColor, value); }
        public bool Read
        {
            get => read; 
            set
            {
                SetProperty(ref read, value);
                if (read)
                {
                    BackgroundColor = (SolidColorBrush)Brushes.Transparent;
                } else
                {
                    BackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#fddfea");
                }
            }
        }

    }
}
