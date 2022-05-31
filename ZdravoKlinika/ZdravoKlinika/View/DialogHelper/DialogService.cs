﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.View.DoctorPages;
using ZdravoKlinika.Controller;
using System.Windows;
using System.ComponentModel;

namespace ZdravoKlinika.View.DialogHelper
{
    public class DialogService : IDialogService
    {
        private static Window openDialog;
        private static Window mainWindow;

        public Window OpenDialog { get => openDialog; set => openDialog = value; }
        public static Window MainWindow { get => mainWindow; set => mainWindow = value; }

        public void ShowDialog(string name, Action<string> callback)
        {
            
        }

        public void ShowCreateApptScheduleDialog()
        {
            CreateApptSchedule createApptSchedule = new CreateApptSchedule();
            createApptSchedule.ShowDialog();
        }

        public void ShowTimeOffDialog()
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            timeOffRequestView.ShowDialog();
        }

        public void ShowMedRequestDialog(int requestId)
        {
            ApproveMedView approveMedView = new ApproveMedView(requestId);
            OpenDialog = approveMedView;
            approveMedView.ShowDialog();
        }

        public void CloseDialog()
        {
           OpenDialog.Close();
        }

        public void CloseMainAndOpenSignIn()
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            MainWindow.Close();
            MainWindow = signInWindow;
        }

        public void ShowDoctorWindow()
        {
            DoctorBasePage doctorBasePage = new DoctorBasePage();
            doctorBasePage.Show();
            MainWindow = doctorBasePage;
        }
    }
}