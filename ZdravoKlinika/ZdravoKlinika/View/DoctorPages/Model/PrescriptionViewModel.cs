using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PrescriptionViewModel : ViewModelBase
    {
        private Prescription prescription;
        private string date;
        private string usage;
        public DelegateCommand DownloadPrescription { get; set; }
        private string patient;

        public PrescriptionViewModel()
        {
        }

        public PrescriptionViewModel(Prescription prescription)
        {
            Prescription = prescription;
            Date = prescription.DateOfCreation.Date.ToString("dd.MM.yyyy.");
            Usage = "Količina: " + prescription.Amount + ", "
                + prescription.Frequency + "X" + prescription.SingleDose + " " + prescription.Repeat + ", "
                + prescription.Duration + " dan(a)" + ", " + prescription.DoctorsNote; ;
            DownloadPrescription = new DelegateCommand(ExecuteExport);
            Patient = Prescription.Patient.GetPatientFullName() + ", " + Prescription.Patient.GetPatientId();

        }

        public void ExecuteExport()
        {
            ZdravoKlinika.Util.PdfCreator pdfCreator = new Util.PdfCreator(Prescription.ToString());
            pdfCreator.CreatePdfForPrescription(this);
        }

        public Prescription Prescription { get => prescription; set => SetProperty(ref prescription, value); }
        public string Usage { get => usage; set => SetProperty(ref usage, value); }
        public string Date { get => date; set => date = value; }
        public string Patient { get => patient; set => patient = value; }
    }
}
