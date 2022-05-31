﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.Service
{
    internal class AppReviewService
    {
        private AppReviewRepository appReviewRepository;

        internal AppReviewRepository AppReviewRepository { get => appReviewRepository; set => appReviewRepository = value; }

        public AppReviewService()
        {
            AppReviewRepository = new AppReviewRepository();
        }
        public void AddReview(RegisteredPatient user, DateTime time, int[] grades, String comment)
        {
            AppReviewRepository.AddAppReview(new AppReview(GetUniqueId(), user, time, grades, comment));
        }

        public void RemoveReview(String id)
        {
            AppReview review = AppReviewRepository.GetById(id);
            AppReviewRepository.RemoveAppReview(review);
        }

        public void RemoveAll()
        {
            AppReviewRepository.RemoveAllAppReviews();
        }

        public AppReview GetById(String id)
        {
            return AppReviewRepository.GetById(id);
        }

        public List<AppReview> GetAll()
        {
            return AppReviewRepository.GetAll();
        }

        public String GetUniqueId()
        {
            bool unique = false;
            String newId = "";
            while (!unique)
            {
                newId = IdGenerator.Generate();
                unique = IsUnique(newId);
            }
            return newId;
        }
        public bool IsUnique(String newId)
        {
            bool returnVal = true;
            List<AppReview> reviews = AppReviewRepository.GetAll();
            foreach (AppReview review in reviews)
            {
                if (review.Id.Equals(newId))
                {
                    returnVal = false;
                    break;
                }
            }
            return returnVal;
        }

    }
}