using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class PastViewModel : ViewModelBase
    {
        private int appointmentId;
        private string title;
        private string diagnosis;
        private string precription;
        private string prescriptionsFull;
        private string opinion;
        private Doctor doctor;
        private string patient;
        private string doctorPrint;
        private string dateTimeRoom;

        public MyICommand DownloadAnamnesis { get; set; }

        public PastViewModel()
        {
        }

        public void ExportAnamnesis()
        {
            ZdravoKlinika.Util.PdfCreator pdfCreator = new Util.PdfCreator(this.title.Replace(':', '꞉'));
            pdfCreator.CreatePdfForAnamnesis(this);
        }

        public void Init(Appointment appointment)
        {
            this.appointmentId = appointment.AppointmentId;
            this.Doctor = appointment.Doctor;
            this.Patient = appointment.Patient.GetPatientFullName() + ", " + appointment.Patient.GetPatientId();
            this.title = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", " + appointment.Doctor.ToString();
            this.diagnosis = appointment.Diagnoses;
            foreach(Prescription p in appointment.Prescriptions)
            {
                this.precription += p.ToString() ;
                if(appointment.Prescriptions.Last().Id != p.Id)
                {
                    this.precription += ", ";
                }
            }
            this.opinion = appointment.DoctorsNotes;
            this.dateTimeRoom = appointment.DateAndTime.ToString("dd.MM.yyyy. HH:mm") + ", soba " + appointment.Room.Name;

            DownloadAnamnesis = new MyICommand(ExportAnamnesis);

            foreach (Prescription p in appointment.Prescriptions)
            {
                this.PrescriptionsFull += p.FullPrescriptionToString();
            }
            this.DoctorPrint = this.doctor.ToString() + ", " + this.doctor.Specialty.ToLower();
        }

        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string Precription { get => precription; set => SetProperty(ref precription, value); }
        public string Opinion { get => opinion; set => SetProperty(ref opinion, value); }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public string Patient { get => patient; set => SetProperty(ref patient, value); }
        public string DateTimeRoom { get => dateTimeRoom; set => SetProperty(ref dateTimeRoom, value); }
        public string PrescriptionsFull { get => prescriptionsFull; set => SetProperty(ref prescriptionsFull, value); }
        public string DoctorPrint { get => doctorPrint; set => SetProperty(ref doctorPrint, value); }
    }
}
