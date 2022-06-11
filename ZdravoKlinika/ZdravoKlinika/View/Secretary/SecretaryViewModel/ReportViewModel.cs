using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        DateTime fromDateTime;
        DateTime toDateTime;

        ObservableCollection<Appointment> apps;
        AppointmentController appointmentController;

        String fromTime;
        String toTime;

        ICommand serachCommand;
        ICommand createCommand;
        public ReportViewModel()
        {
            fromDateTime = DateTime.Now.Date;
            toDateTime = DateTime.Now.Date;
            appointmentController = new AppointmentController();
            fromTime = "";
            toTime = "";
        }

        public string ToTime { get => toTime; set => toTime = value; }
        public DateTime ToDateTime { get => toDateTime; set => toDateTime = value; }
        public DateTime FromDateTime { get => fromDateTime; set => fromDateTime = value; }
        public string FromTime { get => fromTime; set => fromTime = value; }

        public ICommand SerachCommand
        {
            get
            {
                return serachCommand ?? (serachCommand = new MyICommand(() => Search(), () => SearchCanExecute));
            }
        }
        public ICommand CreatePDF
        {
            get
            {
                return createCommand ?? (createCommand = new MyICommand(() => CreatePdf(), () => CreateCanExecute));
            }
        }

        private bool Validate() 
        {
            Regex regex = new Regex("[0-9]{1,2}[:][0-9]{2}");
            if (!regex.IsMatch(fromTime))
            {
                return false;

            }

            int hours = Int32.Parse(fromTime.Split(":")[0]);
            int minutes = Int32.Parse(fromTime.Split(":")[1]);
            if (hours >= 24 || minutes >= 60)
                return false;
            DateTime date = fromDateTime.Date;
            date = date.AddHours(hours);
            date = date.AddMinutes(minutes);
            FromDateTime = date;

            Regex regex2 = new Regex("[0-9]{1,2}[:][0-9]{2}");
            if (!regex2.IsMatch(toTime))
            {
                return false;

            }

            int hours2 = Int32.Parse(toTime.Split(":")[0]);
            int minutes2 = Int32.Parse(toTime.Split(":")[1]);
            if (hours >= 24 || minutes >= 60)
                return false;
            DateTime date2 = toDateTime.Date;
            date2 = date2.AddHours(hours2);
            date2 = date2.AddMinutes(minutes2);
            ToDateTime = date2;
            return true;
        }

        private void Search()
        {
            if (!Validate()) 
            {
                return;
            }
            Apps = new ObservableCollection<Appointment>(appointmentController.GetAllInTimeFrame(FromDateTime, ToDateTime));
        }

        private void CreatePdf() 
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Table Example";

            for (int p = 0; p < 1; p++)
            {
                // Page Options
                PdfPage pdfPage = document.AddPage();
                pdfPage.Height = 842;//842
                pdfPage.Width = 496;
                
                if (Apps.Count * 20 > 842) 
                {
                    pdfPage.Height = Apps.Count * 20 + 60;
                }
                // Get an XGraphics object for drawing
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);

                // Text format
                XStringFormat format = new XStringFormat();
                format.LineAlignment = XLineAlignment.Near;
                format.Alignment = XStringAlignment.Near;
                var tf = new XTextFormatter(graph);

                XFont fontParagraph = new XFont("Verdana", 8, XFontStyle.Regular);

                // Row elements
                int el1_width = 80;
                int el2_width = 100;
                int el3_width = 100;
                int el4_width = 80;
                int el5_width = 80;

                // page structure options
                double lineHeight = 20;
                int marginLeft = 20;
                int marginTop = 20;

                int el_height = 30;
                int rect_height = 17;

                int interLine_X_1 = 2;
                int interLine_X_2 = 2 * interLine_X_1;
                int interLine_X_3 = 2 * interLine_X_2;
                int interLine_X_4 = 2 * interLine_X_3;
                int interLine_X_5 = 2 * interLine_X_4;

                int offSetX_1 = el1_width;
                int offSetX_2 = el1_width + el2_width;
                int offSetX_3 = el1_width + el2_width + el3_width;
                int offSetX_4 = el1_width + el2_width + el3_width + el4_width;
                int offSetX_5 = el1_width + el2_width + el3_width + el4_width + el5_width;

                XSolidBrush rect_style1 = new XSolidBrush(XColors.LightGray);
                XSolidBrush rect_style2 = new XSolidBrush(XColors.DarkGreen);
                XSolidBrush rect_style3 = new XSolidBrush(XColors.Red);

                graph.DrawRectangle(rect_style2, marginLeft, marginTop, pdfPage.Width - 2 * marginLeft, rect_height);

                tf.DrawString("Datum", fontParagraph, XBrushes.White,
                              new XRect(marginLeft, marginTop, el1_width, el_height), format);

                tf.DrawString("Doktor", fontParagraph, XBrushes.White,
                              new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

                tf.DrawString("Pacijent", fontParagraph, XBrushes.White,
                              new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop, el3_width, el_height), format);

                tf.DrawString("Soba", fontParagraph, XBrushes.White,
                              new XRect(marginLeft + offSetX_3 + 2 * interLine_X_3, marginTop, el4_width, el_height), format);

                tf.DrawString("Tip operacije", fontParagraph, XBrushes.White,
                              new XRect(marginLeft + offSetX_4 + 2 * interLine_X_4, marginTop, el5_width, el_height), format);

                for (int i = 0; i < Apps.Count; i++)
                {
                    double dist_Y = lineHeight * (i + 1);
                    double dist_Y2 = dist_Y - 2;


                    graph.DrawRectangle(rect_style1, marginLeft, marginTop + dist_Y2, el1_width, rect_height);
                    tf.DrawString(

                        Apps[i].DateAndTime.ToString("g"),
                        fontParagraph,
                        XBrushes.Black,
                        new XRect(marginLeft, marginTop + dist_Y, el1_width, el_height),
                        format);

                    graph.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
                    tf.DrawString(
                        Apps[i].Doctor.ToString(),
                        fontParagraph,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop + dist_Y, el2_width, el_height),
                        format);


                    graph.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el3_width, rect_height);
                    tf.DrawString(
                        Apps[i].Patient.GetPatientFullName(),
                        fontParagraph,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop + dist_Y, el3_width, el_height),
                        format);

                    graph.DrawRectangle(rect_style1, marginLeft + offSetX_3 + interLine_X_3, dist_Y2 + marginTop, el4_width, rect_height);
                    tf.DrawString(
                        Apps[i].Room.Name,
                        fontParagraph,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_3 + 2 * interLine_X_3, marginTop + dist_Y, el4_width, el_height),
                        format);

                    graph.DrawRectangle(rect_style1, marginLeft + offSetX_4 + interLine_X_4, dist_Y2 + marginTop, el5_width, rect_height);
                    tf.DrawString(
                        Apps[i].getTranslatedType(),
                        fontParagraph,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_4 + 2 * interLine_X_4, marginTop + dist_Y, el5_width, el_height),
                        format);



                }


            }


            const string filename = "C:\\Users\\asd\\Desktop\\izvestaj.pdf";
            document.Save(filename);

            //byte[] bytes = null;
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    document.Save(stream, true);
            //    bytes = stream.ToArray();
            //}

            //SendFileToResponse(bytes, "HelloWorld_test.pdf");
        }

        public bool SearchCanExecute { get => true; }
        public ObservableCollection<Appointment> Apps { get => apps; set => SetProperty(ref apps, value);}
        public bool CreateCanExecute { get => true;}

    }
}
