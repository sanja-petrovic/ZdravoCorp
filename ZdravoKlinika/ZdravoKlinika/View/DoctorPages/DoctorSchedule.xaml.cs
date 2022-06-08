using Prism.Commands;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class DoctorSchedule : UserControl
    {
        private DateTime selected;
        private Doctor doctor;

        private DialogHelper.DialogService dialogService;

        public DoctorSchedule()
        {
            DataContext = this;
            dialogService = new DialogHelper.DialogService();
            this.doctor = RegisteredUserController.UserToDoctor(App.User);
            Selected = DateTime.Today;
            InitializeComponent();
            ApptTabPanel.Parent1 = this;
            ApptTabPanel.Doctor = doctor;
            ApptTabPanel.Selected = DateTime.Today;
            TimeOffView.Load();
            this.Focus();
        }
        private DelegateCommand showCreateDialog;
        private DelegateCommand showTimeOffDialog;

        public DateTime Selected { get => selected; set { selected = value; if(ApptTabPanel != null) ApptTabPanel.Selected = Selected; } }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public DelegateCommand ShowCreateDialog => showCreateDialog ?? (showCreateDialog = new DelegateCommand(ExecuteShowCreateDialog));
        public DelegateCommand ShowTimeOffDialog => showTimeOffDialog ?? (showTimeOffDialog = new DelegateCommand(ExecuteShowTimeOffDialog));



        private void ExecuteShowTimeOffDialog()
        {
            dialogService.ShowTimeOffDialog();
            TimeOffView.Load();
        }

        private void ExecuteShowCreateDialog()
        {
            dialogService.ShowCreateApptScheduleDialog(ApptTabPanel.Selected);
            ApptTabPanel.ViewModel.InfoChange();
            ApptTabPanel.HiddenTb.Visibility = ApptTabPanel.TabTab.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

    }
}
