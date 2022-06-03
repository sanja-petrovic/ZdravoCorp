using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IRegisteredUserRepository : IRepositoryBase<RegisteredUser, String>
    {
        public RegisteredUser? GetUserByEmailAndPassword(String email, String password);
        public void RememberUser(RegisteredUser user);
        public RegisteredUser GetRememberedUser();
        public void ForgetUser();

    }
}
