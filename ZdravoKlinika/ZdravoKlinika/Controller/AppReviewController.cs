using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    internal class AppReviewController
    {
        private AppReviewService appReviewService;

        public AppReviewController()
        {
            appReviewService = new AppReviewService();
        }

        public void AddReview(RegisteredPatient user, DateTime time, int[] grades, String comment)
        {
            appReviewService.AddReview(user, time, grades, comment);
        }

        public void RemoveReview(String id)
        {
           appReviewService.RemoveReview(id);
        }

        public void RemoveAll()
        {
            appReviewService.RemoveAll();
        }

        public AppReview GetById(String id)
        {
           return appReviewService.GetById(id);
        }

        public List<AppReview> GetAll()
        {
           return appReviewService.GetAll();
        }

        public int CountNumberOfGrades(int questionNumber, int gradeToCount)
        {
            return appReviewService.CountNumberOfGrades(questionNumber, gradeToCount);
        }

        public double GetAverageGrade(int questionNumber)
        {
            return appReviewService.GetAverageGrade(questionNumber);
        }
    }
}
