using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IEmployeeNotificationRepository : IRepositoryBase<Model.EmployeeNotification, String>
    {
        public List<Model.EmployeeNotification> GetAllPersonalNotifications(RegisteredUser usr);
        public List<Model.EmployeeNotification> GetSpecificTypeOfNotifications(RegisteredUser user, EmployeeNotificationType type);

    }
}
