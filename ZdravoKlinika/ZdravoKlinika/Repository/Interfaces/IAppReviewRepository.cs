using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IAppReviewRepository : IRepositoryBase<Model.AppReview, String>
    {
        public void UpdateReferences(Model.AppReview review);

    }
}
