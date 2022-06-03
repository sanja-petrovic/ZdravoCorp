using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DialogHelper;

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
        private string tempDiagnosis;
        private string tempOpinion;

        public MyICommand DownloadAnamnesis { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand SaveCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }

        private AppointmentController controller;
        private DialogHelper.DialogService dialogService;

        public PastViewModel()
        {
            DialogService = new DialogService();
            Controller = new AppointmentController();
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
            SaveCommand = new MyICommand(ExecuteSave);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
            EditCommand = new MyICommand(ExecuteEdit);

            foreach (Prescription p in appointment.Prescriptions)
            {
                this.PrescriptionsFull += p.FullPrescriptionToString();
            }
            this.DoctorPrint = this.doctor.ToString() + ", " + this.doctor.Specialty.ToLower();
            TempDiagnosis = Diagnosis;
            TempOpinion = Opinion;
        }

        public void ExecuteEdit()
        {
            DialogService.ShowEditAnamnesisDialog(this);
        }

        public void ExecuteSave()
        {
            Controller.UpdateAnamnesis(AppointmentId, TempOpinion, TempDiagnosis);
            Opinion = TempOpinion;
            Diagnosis = TempDiagnosis;
            DialogService.CloseDialog(this);
        }

        public void ExecuteGiveUp() {
            TempDiagnosis = Diagnosis;
            TempOpinion = Opinion;
            DialogService.CloseDialog(this);
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
        public AppointmentController Controller { get => controller; set => controller = value; }
        public DialogService DialogService { get => dialogService; set => dialogService = value; }
        public string TempDiagnosis { get => tempDiagnosis; set => tempDiagnosis = value; }
        public string TempOpinion { get => tempOpinion; set => tempOpinion = value; }
    }
}
