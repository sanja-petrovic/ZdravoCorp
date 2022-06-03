using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DiagnosisViewModel : ViewModelBase
    {
        private string diagnosis;
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand GiveUpCommand { get; set; }
        public string Diagnosis { get => diagnosis; set => SetProperty(ref diagnosis, value); }
        public string MedicalRecordId { get => medicalRecordId; set => medicalRecordId = value; }

        private Controller.MedicalRecordController controller;
        private string medicalRecordId;

        public DiagnosisViewModel(String medicalRecordId)
        {
            MedicalRecordId = medicalRecordId;
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            GiveUpCommand = new MyICommand(ExecuteGiveUp);
            controller = new Controller.MedicalRecordController();
        }

        public void ExecuteConfirm()
        {
            controller.AddDiagnosis(Diagnosis, MedicalRecordId);
            DialogHelper.DialogService.CloseDialog(this);
        }

        public void ExecuteGiveUp()
        {
            DialogHelper.DialogService.CloseDialog(this);
        }
    }

}
