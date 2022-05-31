using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class Navigator
    {

        public Frame MainFrame { get; set; }

        public Prism.Commands.DelegateCommand HomeCommand { get; set; }
        private DoctorHomePage doctorHomePage;
        private DoctorSchedule doctorSchedule;
        private DoctorMedicationsView doctorMedicationsView;
        private DoctorProfileView doctorProfileView;
        private DoctorAllPatientsView doctorAllPatientsView;
        private FeedbackView feedbackView;

        public Navigator(Frame mainFrame)
        {
            MainFrame = mainFrame;
            doctorHomePage = new DoctorHomePage();
            doctorSchedule = new DoctorSchedule();
            doctorMedicationsView = new DoctorMedicationsView();
            doctorProfileView = new DoctorProfileView();
            doctorAllPatientsView = new DoctorAllPatientsView();
            feedbackView = new FeedbackView();

            HomeCommand = new Prism.Commands.DelegateCommand(ExecuteHome);
        }

        public void ExecuteHome()
        {
            MainFrame.Navigate(doctorHomePage);
        }
    }
}
