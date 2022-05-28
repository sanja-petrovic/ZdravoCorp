using System;
using System.Windows;
using System.Windows.Documents;
using PdfSharp.Pdf;
using System.IO;
using System.Windows.Xps.Packaging;

namespace ZdravoKlinika.Util
{
    public class PdfCreator
    {
        private string pdfFileLocation;
        private string xpsFileLocation;
        private string xpsFileName;

        public string PdfFileLocation { get => pdfFileLocation; set => pdfFileLocation = value; }
        public string XpsFileLocation { get => xpsFileLocation; set => xpsFileLocation = value; }
        public string XpsFileName { get => xpsFileName; set => xpsFileName = value; }

        public PdfCreator()
        {
            XpsFileName = "MyXpsReport.xps";
            XpsFileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Documents" + System.IO.Path.DirectorySeparatorChar + XpsFileName;
        }

        public void GenerateXps(UIElement myUserControl)
        {
            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(myUserControl);
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
            dlg.FileName = "MyPdfReport";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF files (.pdf)|*.pdf";

            return dlg;
        }

        public void ConvertXpsToPdfAndDeleteXps()
        {
            PdfSharp.Xps.XpsConverter.Convert(XpsFileLocation, PdfFileLocation, 0);
            File.Delete(XpsFileLocation);
        }

        public void CreatePdf(UIElement myUserControl)
        {
            GenerateXps(myUserControl); 
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            Microsoft.Win32.SaveFileDialog dlg = OpenSaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                PdfFileLocation = dlg.FileName;
                document.Save(PdfFileLocation);
                ConvertXpsToPdfAndDeleteXps();
            }
        }

    }
}
