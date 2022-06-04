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

        public RegisteredUser? GetUserById(String id) 
        {
            return UserRepository.GetById(id);
        }

        public RegisteredUser? GetUserByEmailAndPassword(String email, String password)
        {
            return UserRepository.GetUserByEmailAndPassword(email, password);
        }

        public void RememberUser(RegisteredUser user)
        {
            this.userRepository.RememberUser(user);
        }

        internal List<RegisteredUser> GetAllEmployees()
        {
            List<RegisteredUser> employees = new List<RegisteredUser>();

            foreach (RegisteredUser user in GetAll())
            {
                if (user.UserType != UserType.Patient)
                {
                    employees.Add(user);
                }
            }

            return employees;
        }

        public RegisteredUser GetRememberedUser()
        {
            return this.userRepository.GetRememberedUser();
        }

        public void ForgetUser()
        {
            this.userRepository.ForgetUser();
        }

        public List<RegisteredUser> GetAll()
        {
            return userRepository.GetAll();
        }
    }
}
