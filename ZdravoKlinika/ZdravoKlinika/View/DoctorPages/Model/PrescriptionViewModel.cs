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
        private string usage;

        public PrescriptionViewModel(Prescription prescription)
        {
            this.prescription = prescription;
            this.usage = "Količina: " + prescription.Amount + ", "
                + prescription.Frequency + "X" + prescription.SingleDose + " " + prescription.Repeat + ", "
                + prescription.Duration + " dan(a)" + ", " + prescription.DoctorsNote; ;
            
        }

        public Prescription Prescription { get => prescription; set => SetProperty(ref prescription, value); }
        public string Usage { get => usage; set => SetProperty(ref usage, value); }
    }
}
