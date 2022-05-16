using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZdravoKlinika.View;
using ZdravoKlinika.View.PatientPages;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    internal class PatientApointmentsViewModel : INotifyPropertyChanged
    {
        private string patientid;
        private ImageSource addIcon = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "addPatient.png"));
        private ImageSource commentIcon = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "commentPatient.png"));
        private ImageSource editIcon = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "editPatient.png"));
        private ImageSource documentsIcon = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "pagePatient.png"));
        private ImageSource removeIcon = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "removePatient.png"));
        private PatientAddView patientAddView;
        private PatientEditView patientEditView;
        private List<DateTime> appointmentDates = new List<DateTime>();
        private AppointmentController controller = new AppointmentController();
        private ObservableCollection<Appointment> selectedDateAppointments = new ObservableCollection<Appointment>();
        public PatientApointmentsViewModel(String id)
        {
            patientid = id;
            foreach (Appointment app in Controller.GetAppointmentsByPatientId(patientid))
            {
                AppointmentDates.Add(app.DateAndTime.Date);
            }
        }
        public ImageSource AddIcon
        {
            get
            {
                return addIcon;
            }
            set
            {
                if (this.addIcon != value)
                {
                    this.addIcon = value;
                    NotifyPropertyChanged("addImageSource");
                }
            }
        }

        public ImageSource CommentIcon 
        { get => commentIcon; 
            set
            {
                if(this.commentIcon != value)
                {
                    commentIcon = value;
                    NotifyPropertyChanged("commentImageSource");
                }
            }
           }
        public ImageSource EditIcon 
        { get => editIcon;
            set
            {
                if(this.editIcon != value)
                {
                    editIcon = value;
                    NotifyPropertyChanged("editImageSource");
                
                }
            } 
        }
        public ImageSource DocumentsIcon 
        { get => documentsIcon; 
            set
            {
                if(documentsIcon != value)
                {
                    documentsIcon = value;
                    NotifyPropertyChanged("documentsImageSource");
                }
               
            }
        }
        public ImageSource RemoveIcon 
        { get => removeIcon;
            set
            {
                if(removeIcon != value)
                {
                    removeIcon = value;
                    NotifyPropertyChanged("removeImageSource");
                }
            }
        }

        public PatientAddView PatientAddView { get => patientAddView; set => patientAddView = value; }
        public PatientEditView PatientEditView { get => patientEditView; set => patientEditView = value; }
        public List<DateTime> AppointmentDates { get => appointmentDates; set => appointmentDates = value; }
        public AppointmentController Controller { get => controller; set => controller = value; }
        public ObservableCollection<Appointment> SelectedDateAppointments
        {
            get => selectedDateAppointments;
            set
            {
                selectedDateAppointments = value;
                NotifyPropertyChanged("dates");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetSelectedDateAppointments(DateTime date)
        {
            SelectedDateAppointments = new ObservableCollection<Appointment> (controller.GetAppointmentsByPatientIdForDate(patientid, date));
            
        }
    }
    
}
