using System;
using System.Windows;
using System.Windows.Documents;
using PdfSharp.Pdf;
using System.IO;
using System.Windows.Xps.Packaging;
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages;
using ZdravoKlinika.View.DoctorPages.Model;
using ZdravoKlinika.View.Manager.ViewModel;
using ZdravoKlinika.View.Manager.Views;

namespace ZdravoKlinika.Util
{
    public class PdfCreator
    {
        private string defaultPdfName;
        private string pdfFileLocation;
        private string xpsFileLocation;
        private string xpsFileName;

        //default: A4
        private bool customSize = false;
        private double width; 
        private double height;

        public string PdfFileLocation { get => pdfFileLocation; set => pdfFileLocation = value; }
        public string XpsFileLocation { get => xpsFileLocation; set => xpsFileLocation = value; }
        public string XpsFileName { get => xpsFileName; set => xpsFileName = value; }
        public string DefaultPdfName { get => defaultPdfName; set => defaultPdfName = value; }
        public bool CustomSize { get => customSize; set => customSize = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }

        public PdfCreator()
        {
            XpsFileName = "MyXpsReport.xps";
            XpsFileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Documents" + System.IO.Path.DirectorySeparatorChar + XpsFileName;
            DefaultPdfName = "MyPdfReport";
        }

        public PdfCreator(String pdfName)
        {
            XpsFileName = "MyXpsReport.xps";
            XpsFileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Documents" + System.IO.Path.DirectorySeparatorChar + XpsFileName;
            DefaultPdfName = pdfName;
        }

        public void GenerateXps(UIElement myUserControl)
        {
            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(myUserControl);
            if(customSize)
            {
                fixedPage.Width = Width;
                fixedPage.Height = Height;
            }
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);
            XpsDocument xpsd = new XpsDocument(XpsFileLocation, FileAccess.ReadWrite);
            System.Windows.Xps.XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
            xw.Write(fixedDoc);
            xpsd.Close();
        }

        public Microsoft.Win32.SaveFileDialog OpenSaveFileDialog()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = DefaultPdfName;
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF files (.pdf)|*.pdf";

            return dlg;
        }

        public void ConvertXpsToPdfAndDeleteXps()
        {
            PdfSharp.Xps.XpsConverter.Convert(XpsFileLocation, PdfFileLocation, 0);
            File.Delete(XpsFileLocation);
        }

        //the UI element MUST be a user control! 
        public void CreatePdf(UIElement myUserControl)
        {
            GenerateXps(myUserControl); 
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.TrimMargins.All = 10;
            
            Microsoft.Win32.SaveFileDialog dlg = OpenSaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                PdfFileLocation = dlg.FileName;
                document.Save(PdfFileLocation);
                ConvertXpsToPdfAndDeleteXps();
            } else
            {
                File.Delete(XpsFileLocation);
            }
        }

        public void CreatePdfForPrescription(PrescriptionViewModel prescriptionViewModel)
        {
            PrescriptionUserControl prescriptionUserControl = new PrescriptionUserControl(prescriptionViewModel);
            CustomSize = true;
            Width = prescriptionUserControl.Width;
            Height = prescriptionUserControl.Height;
            CreatePdf(prescriptionUserControl);
        }

        public void CreatePdfForAnamnesis(PastViewModel anamnesisViewModel)
        {
            AnamnesisPrint anamnesisUserControl = new AnamnesisPrint(anamnesisViewModel);
            CreatePdf(anamnesisUserControl);
        }

        public void CreatePdfForRoomReport(RoomReportViewModel roomReportViewModel)
        {
            RoomReportUserControl roomReportUserControl = new RoomReportUserControl(roomReportViewModel.Appointments, roomReportViewModel.Room, roomReportViewModel.StartDate, roomReportViewModel.EndDate);
            CreatePdf(roomReportUserControl);
        }

    }
}
