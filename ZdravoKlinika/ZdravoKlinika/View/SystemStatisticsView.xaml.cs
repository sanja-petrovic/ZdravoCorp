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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for SystemStatisticsView.xaml
    /// </summary>
    public partial class SystemStatisticsView : Window
    {
        private AppReviewController appReviewController;

        public SystemStatisticsView()
        {
            InitializeComponent();
            this.appReviewController = new AppReviewController();
            SetLabelContent();
        }

        private void SetLabelContent()
        {
            SetFirstQuestionValues();
            SetSecondQuestionValues();
            SetThirdQuestionValues();
            SetFourthQuestionValues();
            SetFifthQuestionValues();
            SetSixthQuestionValues();
            SetSeventhQuestionValues();
        }

        private void SetFirstQuestionValues()
        {
            firstQuestionFirstField.Content = appReviewController.CountNumberOfGrades(0, 1).ToString();
            firstQuestionSecondField.Content = appReviewController.CountNumberOfGrades(0, 2).ToString();
            firstQuestionThirdField.Content = appReviewController.CountNumberOfGrades(0, 3).ToString();
            firstQuestionFourthField.Content = appReviewController.CountNumberOfGrades(0, 4).ToString();
            firstQuestionFifthField.Content = appReviewController.CountNumberOfGrades(0, 5).ToString();
            firstQuestionAverage.Content = appReviewController.GetAverageGrade(0).ToString("0.00");
        }

        private void SetSecondQuestionValues()
        {
            secondQuestionFirstField.Content = appReviewController.CountNumberOfGrades(1, 1).ToString();
            secondQuestionSecondField.Content = appReviewController.CountNumberOfGrades(1, 2).ToString();
            secondQuestionThirdField.Content = appReviewController.CountNumberOfGrades(1, 3).ToString();
            secondQuestionFourthField.Content = appReviewController.CountNumberOfGrades(1, 4).ToString();
            secondQuestionFifthField.Content = appReviewController.CountNumberOfGrades(1, 5).ToString();
            secondQuestionAverage.Content = appReviewController.GetAverageGrade(1).ToString("0.00");
        }
        
        private void SetThirdQuestionValues()
        {
            thirdQuestionFirstField.Content = appReviewController.CountNumberOfGrades(2, 1).ToString();
            thirdQuestionSecondField.Content = appReviewController.CountNumberOfGrades(2, 2).ToString();
            thirdQuestionThirdField.Content = appReviewController.CountNumberOfGrades(2, 3).ToString();
            thirdQuestionFourthField.Content = appReviewController.CountNumberOfGrades(2, 4).ToString();
            thirdQuestionFifthField.Content = appReviewController.CountNumberOfGrades(2, 5).ToString();
            thirdQuestionAverage.Content = appReviewController.GetAverageGrade(2).ToString("0.00");
        }

        private void SetFourthQuestionValues()
        {
            fourthQuestionFirstField.Content = appReviewController.CountNumberOfGrades(3, 1).ToString();
            fourthQuestionSecondField.Content = appReviewController.CountNumberOfGrades(3, 2).ToString();
            fourthQuestionThirdField.Content = appReviewController.CountNumberOfGrades(3, 3).ToString();
            fourthQuestionFourthField.Content = appReviewController.CountNumberOfGrades(3, 4).ToString();
            fourthQuestionFifthField.Content = appReviewController.CountNumberOfGrades(3, 5).ToString();
            fourthQuestionAverage.Content = appReviewController.GetAverageGrade(3).ToString("0.00");
        }

        private void SetFifthQuestionValues()
        {
            fifthQuestionFirstField.Content = appReviewController.CountNumberOfGrades(4, 1).ToString();
            fifthQuestionSecondField.Content = appReviewController.CountNumberOfGrades(4, 2).ToString();
            fifthQuestionThirdField.Content = appReviewController.CountNumberOfGrades(4, 3).ToString();
            fifthQuestionFourthField.Content = appReviewController.CountNumberOfGrades(4, 4).ToString();
            fifthQuestionFifthField.Content = appReviewController.CountNumberOfGrades(4, 5).ToString();
            fifthQuestionAverage.Content = appReviewController.GetAverageGrade(4).ToString("0.00");
        }

        private void SetSixthQuestionValues()
        {
            sixthQuestionFirstField.Content = appReviewController.CountNumberOfGrades(5, 1).ToString();
            sixthQuestionSecondField.Content = appReviewController.CountNumberOfGrades(5, 2).ToString();
            sixthQuestionThirdField.Content = appReviewController.CountNumberOfGrades(5, 3).ToString();
            sixthQuestionFourthField.Content = appReviewController.CountNumberOfGrades(5, 4).ToString();
            sixthQuestionFifthField.Content = appReviewController.CountNumberOfGrades(5, 5).ToString();
            sixthQuestionAverage.Content = appReviewController.GetAverageGrade(5).ToString("0.00");
        }

        private void SetSeventhQuestionValues()
        {
            seventhQuestionFirstField.Content = appReviewController.CountNumberOfGrades(6, 1).ToString();
            seventhQuestionSecondField.Content = appReviewController.CountNumberOfGrades(6, 2).ToString();
            seventhQuestionThirdField.Content = appReviewController.CountNumberOfGrades(6, 3).ToString();
            seventhQuestionFourthField.Content = appReviewController.CountNumberOfGrades(6, 4).ToString();
            seventhQuestionFifthField.Content = appReviewController.CountNumberOfGrades(6, 5).ToString();
            seventhQuestionAverage.Content = appReviewController.GetAverageGrade(6).ToString("0.00");
        }
    }
}
