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
            InitializeComponent();

        }
    }
}
