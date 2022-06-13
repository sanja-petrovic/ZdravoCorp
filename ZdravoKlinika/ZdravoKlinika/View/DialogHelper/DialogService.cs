using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.DoctorPages;
using ZdravoKlinika.Controller;
using System.Windows;
using System.ComponentModel;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DialogHelper
{
    public class DialogService
    {
        private static List<Window> openDialogs;

        public List<Window> OpenDialogs { get => openDialogs; set => openDialogs = value; }

        public DialogService()
        {
            if(openDialogs == null) openDialogs = new List<Window>();
        }

        public void ShowCreateApptScheduleDialog(DateTime selectedDate)
        {
            CreateApptSchedule createApptSchedule = new CreateApptSchedule(selectedDate);
            openDialogs.Add(createApptSchedule);
            createApptSchedule.ShowDialog();
        }

        public static void ShowHelp()
        {
            Shortcuts shortcuts = new Shortcuts();
            shortcuts.Show();
        }

        public void ShowCreateApptRecordDialog(String id)
        {
            CreateApptRecord createApptRecord = new CreateApptRecord(id);
            openDialogs.Add(createApptRecord);
            createApptRecord.ShowDialog();
        }

        public static void CloseDialog(ViewModelBase viewModel)
        {
            int index = -1;
            foreach (Window w in openDialogs)
            {
                if(w.DataContext == viewModel)
                {
                    w.Close();
                    index = openDialogs.IndexOf(w);
                    break;
                }
            }

            if (index != -1)
            {
                openDialogs.RemoveAt(index);
            }
        }

        public void ShowTimeOffDialog()
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            OpenDialogs.Add(timeOffRequestView);
            timeOffRequestView.ShowDialog();
        }

        public void ShowMedRequestDialog(int requestId)
        {
            ApproveMedView approveMedView = new ApproveMedView(requestId);
            approveMedView.Show();
            OpenDialogs.Add(approveMedView);
        }


        public void ShowLogApptDialog(int apptId)
        {
            LogAppointmentDialog logAppointmentDialog = new LogAppointmentDialog { SelectedAppointmentId = apptId };
            logAppointmentDialog.Init();
            OpenDialogs.Add(logAppointmentDialog);
            logAppointmentDialog.ShowDialog();
        }

        public void ShowEditAnamnesisDialog(PastViewModel viewModel)
        {
            EditAnamnesisWindow editAnamnesisWindow = new EditAnamnesisWindow(viewModel);
            editAnamnesisWindow.Show();
            OpenDialogs.Add(editAnamnesisWindow);
        }

        public void ShowPrompt(string title, string message, Action action, string confirmText="Potvrdite", string giveUpText="Odustanite")
        {
            PromptDialog promptDialog = new PromptDialog(new PromptViewModel(title, message, action, confirmText, giveUpText));
            OpenDialogs.Add(promptDialog);
            promptDialog.ShowDialog();
        }

        public void ShowMedication(MedViewModel viewModel)
        {
            MedView medView = new MedView(viewModel);
            medView.Show();
            OpenDialogs.Add(medView);

        }

        public void ShowAddDiagnosis(String medicalRecordId)
        {
            AddDiagnosisView diagnosisView = new AddDiagnosisView(medicalRecordId);
            OpenDialogs.Add(diagnosisView);
            diagnosisView.ShowDialog();
        }

        public void ShowPrescribeDialog(String patientId)
        {
            TherapyTab viewModel = new TherapyTab();
            viewModel.LoadFromRecord(patientId);
            PrescriptionView prescriptionView = new PrescriptionView(viewModel);
            OpenDialogs.Add(prescriptionView);
            prescriptionView.ShowDialog();
        }

        public void ShowEditAppt(AppointmentViewModel viewModel)
        {
            EditUpcomingAppt editView = new EditUpcomingAppt(viewModel);
            OpenDialogs.Add(editView);
            editView.ShowDialog();
        }
    }
}
