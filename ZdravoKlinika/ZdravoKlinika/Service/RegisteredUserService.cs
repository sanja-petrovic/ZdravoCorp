using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class RegisteredUserService
    {
        RegisteredUserRepository userRepository;

        public RegisteredUserRepository UserRepository { get => userRepository; set => userRepository = value; }

        public RegisteredUserService() 
        {
            userRepository = new RegisteredUserRepository();
        }

        public RegisteredUser? GetUserByEmailAndPassword(String email, String password)
        {
            return UserRepository.GetUserByEmailAndPassword(email, password);
        }
    }
}
