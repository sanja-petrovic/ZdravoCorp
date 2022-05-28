using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages.Model;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.IO;
using ZdravoKlinika.Controller;
using System.Windows.Xps.Packaging;
using PdfSharp.Xps;
using System.Diagnostics;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for PrescribedView.xaml
    /// </summary>
    public partial class PrescribedView : Window
    {
        private PrescriptionViewModel viewModel;
        public PrescribedView()
        {
            PrescriptionController pc = new PrescriptionController();
            Prescription prescription = pc.GetById(1);
            viewModel = new PrescriptionViewModel(prescription);
            DataContext = viewModel;
            //viewModel.Load(prescription);
            InitializeComponent();
            idk();

        }

        public void idk()
        {
            AnamnesisView anamnesisView = new AnamnesisView();
            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(anamnesisView);
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);
            //Create any other required pages here

            //View the document
            /*Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "MyReport"; // Default file name
            dlg.DefaultExt = ".xps"; // Default file extension
            dlg.Filter = "XPS Documents (.xps)|*.xps"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();*/
            string filename = "myreport.xps";
            // Process save file dialog box results
            FixedDocument doc = fixedDoc;
            String fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Documents" + System.IO.Path.DirectorySeparatorChar + filename;
            XpsDocument xpsd = new XpsDocument(fileLocation, FileAccess.ReadWrite);
            System.Windows.Xps.XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
            xw.Write(doc);
            xpsd.Close();
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "MojRecept"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF files (.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
                if(result == true)
            {
                String fileName2 = dlg.FileName;
                String fileLocation2 = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Documents" + System.IO.Path.DirectorySeparatorChar + fileName2;
                document.Save(fileName2);
                PdfSharp.Xps.XpsConverter.Convert(fileLocation, fileName2, 0);
                File.Delete(fileLocation);
            }

        }
    }
}
