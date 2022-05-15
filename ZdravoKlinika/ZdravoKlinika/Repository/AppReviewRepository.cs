using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    internal class AppReviewRepository
    {
        private AppReviewDataHandler appReviewDataHandler;
        private RegisteredPatientRepository registeredPatientRepository;

        public AppReviewRepository()
        {
            appReviewDataHandler = new AppReviewDataHandler();
            registeredPatientRepository = new RegisteredPatientRepository();
            ReadDataFromFile();
        }
        public AppReview GetById(String id)
        {
            AppReview returnValue = null;
            foreach (AppReview review in appReviews)
            {
                if (review.Id == id)
                {
                    UpdateReferences(review);
                    returnValue = review;
                    break;
                }
            }
            return returnValue;
        }

        public List<AppReview> GetAll()
        {
            ReadDataFromFile();
            foreach (AppReview review in appReviews)
            {
                UpdateReferences(review);
            }
            return appReviews;
        }

        private void ReadDataFromFile()
        {
            appReviews = appReviewDataHandler.Read();
            if (appReviews == null) appReviews = new List<AppReview>();
        }

        public void UpdateReferences(AppReview review)
        {
            if (review.RegisteredPatient != null)
                review.RegisteredPatient = registeredPatientRepository.GetById(review.RegisteredPatient.GetPatientId());
        }

        public System.Collections.Generic.List<AppReview> appReviews;

        /// <summary>
        /// Property for collection of AppReview
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.Generic.List<AppReview> AppReviews
        {
            get
            {
                if (appReviews == null)
                    appReviews = new System.Collections.Generic.List<AppReview>();
                return appReviews;
            }
            set
            {
                RemoveAllAppReviews();
                if (value != null)
                {
                    foreach (AppReview oAppReview in value)
                        AddAppReview(oAppReview);
                }
            }
        }

        /// <summary>
        /// Add a new AppReview in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAppReview(AppReview newAppReview)
        {
            if (newAppReview == null)
                return;
            if (this.appReviews == null)
                this.appReviews = new System.Collections.Generic.List<AppReview>();
            if (!this.appReviews.Contains(newAppReview))
                this.appReviews.Add(newAppReview);
        }

        /// <summary>
        /// Remove an existing AppReview from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveAppReview(AppReview oldAppReview)
        {
            if (oldAppReview == null)
                return;
            if (this.appReviews != null)
                if (this.appReviews.Contains(oldAppReview))
                    this.appReviews.Remove(oldAppReview);
        }

        /// <summary>
        /// Remove all instances of AppReview from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllAppReviews()
        {
            if (appReviews != null)
                appReviews.Clear();
        }
    }
}
