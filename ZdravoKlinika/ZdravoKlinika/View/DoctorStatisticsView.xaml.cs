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
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for DoctorStatisticsView.xaml
    /// </summary>
    public partial class DoctorStatisticsView : Window
    {
        private AppointmentController appointmentController;
        private List<Appointment> doctorsAppointments;

        public DoctorStatisticsView()
        {
            InitializeComponent();
            this.appointmentController = new AppointmentController();
            InitializeDoctorsComboBox();
        }

        private void InitializeDoctorsComboBox()
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            List<Doctor> doctors = doctorRepository.GetAll();
            doctorsComboBox.ItemsSource = doctors;
        }

        private void doctorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor doctor = (Doctor)doctorsComboBox.SelectedItem;
            SetLabelContentForDoctor(doctor);            
        }

        private void SetLabelContentForDoctor(Doctor doctor)
        {
            SetFirstQuestionValues(doctor);
            SetSecondQuestionValues(doctor);
            SetThirdQuestionValues(doctor);
        }

        private void SetFirstQuestionValues(Doctor doctor)
        {
            firstQuestionFirstField.Content = this.appointmentController.CountNumberOfGradesForDoctor(0, 1, doctor).ToString();
            firstQuestionSecondField.Content = this.appointmentController.CountNumberOfGradesForDoctor(0, 2, doctor).ToString();
            firstQuestionThirdField.Content = this.appointmentController.CountNumberOfGradesForDoctor(0, 3, doctor).ToString();
            firstQuestionFourthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(0, 4, doctor).ToString();
            firstQuestionFifthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(0, 5, doctor).ToString();
            firstQuestionAverage.Content = this.appointmentController.GetAverageGradeForDoctor(0, doctor).ToString("0.00");
        }

        private void SetSecondQuestionValues(Doctor doctor)
        {
            secondQuestionFirstField.Content = this.appointmentController.CountNumberOfGradesForDoctor(1, 1, doctor).ToString();
            secondQuestionSecondField.Content = this.appointmentController.CountNumberOfGradesForDoctor(1, 2, doctor).ToString();
            secondQuestionThirdField.Content = this.appointmentController.CountNumberOfGradesForDoctor(1, 3, doctor).ToString();
            secondQuestionFourthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(1, 4, doctor).ToString();
            secondQuestionFifthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(1, 5, doctor).ToString();
            secondQuestionAverage.Content = this.appointmentController.GetAverageGradeForDoctor(1, doctor).ToString("0.00");
        }

        private void SetThirdQuestionValues(Doctor doctor)
        {
            thirdQuestionFirstField.Content = this.appointmentController.CountNumberOfGradesForDoctor(2, 1, doctor).ToString();
            thirdQuestionSecondField.Content = this.appointmentController.CountNumberOfGradesForDoctor(2, 2, doctor).ToString();
            thirdQuestionThirdField.Content = this.appointmentController.CountNumberOfGradesForDoctor(2, 3, doctor).ToString();
            thirdQuestionFourthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(2, 4, doctor).ToString();
            thirdQuestionFifthField.Content = this.appointmentController.CountNumberOfGradesForDoctor(2, 5, doctor).ToString();
            thirdQuestionAverage.Content = this.appointmentController.GetAverageGradeForDoctor(2, doctor).ToString("0.00");
        }
    }
}
