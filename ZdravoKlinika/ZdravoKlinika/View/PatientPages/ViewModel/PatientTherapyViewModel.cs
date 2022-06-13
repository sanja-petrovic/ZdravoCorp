using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientTherapyViewModel : INotifyPropertyChanged
    {
        private String patientId;
        private List<DateTime> notificationDates = new List<DateTime>();
        private List<DateTime> notificationTimes = new List<DateTime>();
        private PatientMedicationNotificationController controller;
        private DateTime selectedDate;
        private PatientMedicationNotification selectedNotification;
        private DateTime currentDate;
        private MyICommand loadNotificationsCommand;
        private MyICommand loadTimesCommand;
        private ObservableCollection<PatientMedicationNotification> notifications;
        private String note;
        private List<DateTime> personalNoteTimes = new List<DateTime>();

        ObservableCollection<PatientReport> reports;
        MyICommand createCommand;
        public PatientTherapyViewModel(String id)
        {
            selectedDate = DateTime.Now.Date;
            PatientId = id;
            Controller = new PatientMedicationNotificationController();
            NotificationDates = Controller.GetNotificationDatesForPatient(PatientId);
            LoadNotificationsCommand = new MyICommand(LoadNotifications, CanExecuteLoadNotifications);
            LoadTimesCommand = new MyICommand(LoadTimes, CanExecuteLoadTimes);


            createCommand = new MyICommand(CreatePdf);
            reports = new ObservableCollection<PatientReport>();
            for (int i = 0; i < 30; i++)
            {
                reports.Add(new PatientReport(DateTime.Now.Date.AddDays(i), "Vitamin C"));
            }
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public string PatientId { get => patientId; set => patientId = value; }
        public List<DateTime> NotificationDates { get => notificationDates; set => notificationDates = value; }
        public DateTime SelectedDate { get => selectedDate;
            set
            {
                selectedDate = value;
                LoadNotificationsCommand.RaiseCanExecuteChanged();
                LoadNotificationsCommand.Execute(this);
            }
        }
        internal PatientMedicationNotificationController Controller { get => controller; set => controller = value; }
        public ObservableCollection<PatientMedicationNotification> Notifications
        {
            get => notifications;

            set
            {
                notifications = value;
                OnPropertyChanged("");
            }
        }
        public DateTime CurrentDate { get => currentDate; set => currentDate = value; }
        public MyICommand LoadNotificationsCommand { get => loadNotificationsCommand; set => loadNotificationsCommand = value; }
        public PatientMedicationNotification SelectedNotification { get => selectedNotification;

            set
            {
                selectedNotification = value;
                
                LoadTimesCommand.RaiseCanExecuteChanged();
                LoadTimesCommand.Execute(this);
                OnPropertyChanged("");
            }
        
        }

        public List<DateTime> NotificationTimes { get => notificationTimes; set => notificationTimes = value; }
        public MyICommand LoadTimesCommand { get => loadTimesCommand; set => loadTimesCommand = value; }
        public string Note { get => note; set => note = value; }
        public List<DateTime> PersonalNoteTimes { get => personalNoteTimes; set => personalNoteTimes = value; }
        public ObservableCollection<PatientReport> Reports { get => reports; set => reports = value; }
        public MyICommand CreateCommand { get => createCommand; set => createCommand = value; }

        public void LoadNotifications(object data)
        {
            Notifications = new ObservableCollection<PatientMedicationNotification>(controller.GetByPatientForDate(patientId, selectedDate));
        }

        public bool CanExecuteLoadNotifications(object data)
        {
            bool retVal = false;
            if (selectedDate != null)
            {
                retVal= true;
            }
            return retVal;
        }

        public void LoadTimes(object data)
        {
            notificationTimes = controller.GetPossibleTriggerTimes(selectedNotification);
        }
        public bool CanExecuteLoadTimes(object data)
        {
            bool retVal = false;
            if(selectedNotification != null)
            {
                retVal = true;  
            }
            return retVal;
        }
        public void CreatePdf(object data)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Table Example";

            PdfPage pdfPage = document.AddPage();
            pdfPage.Height = 842;//842
            pdfPage.Width = 496;

            // Get an XGraphics object for drawing
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            // Text format
            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;
            var tf = new XTextFormatter(graph);

            XFont fontParagraph = new XFont( new Font("Times New Roman", 12.0f, GraphicsUnit.World));
            // Row elements
            int el1_width = 50;
            int el2_width = 400;
            // page structure options
            double lineHeight = 20;
            int marginLeft = 20;
            int marginTop = 20;

            int el_height = 30;
            int rect_height = 17;

            int interLine_X_1 = 2;
            int interLine_X_2 = 2 * interLine_X_1;

            int offSetX_1 = el1_width;
            int offSetX_2 = el1_width + el2_width;

            XSolidBrush rect_style1 = new XSolidBrush(XColors.LightGray);
            XSolidBrush rect_style2 = new XSolidBrush(XColors.MediumPurple);

            graph.DrawRectangle(rect_style2, marginLeft, marginTop, pdfPage.Width - 2 * marginLeft, rect_height);

            tf.DrawString("Datum", fontParagraph, XBrushes.White,
                          new XRect(marginLeft, marginTop, el1_width, el_height), format);

            tf.DrawString("Terapija", fontParagraph, XBrushes.White,
                          new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

            for (int i = 0; i < Reports.Count; i++)
            {
                double dist_Y = lineHeight * (i + 1);
                double dist_Y2 = dist_Y - 2;

                graph.DrawRectangle(rect_style1, marginLeft, marginTop + dist_Y2, el1_width, rect_height);
                tf.DrawString(

                    Reports[i].Date.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft, marginTop + dist_Y, el1_width, el_height),
                    format);

                graph.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
                tf.DrawString(
                    Reports[i].MedicationTitle,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop + dist_Y, el2_width, el_height),
                    format);
            }
            //const string filename = "C:\\Users\\asd\\Desktop\\izvestaj.pdf";
            const string filename = "C:\\Users\\yeet\\Desktop\\izvestaj.pdf";
            document.Save(filename);
        }
    }
    public class PatientReport
    {
        private DateTime date;
        private string medicationTitle;

        public PatientReport()
        {

        }
        public PatientReport(DateTime date, string medication)
        {
            this.Date = date;
            this.MedicationTitle = medication;
        }

        public DateTime Date { get => date; set => date = value; }
        public string MedicationTitle { get => medicationTitle; set => medicationTitle = value; }
    }
}
