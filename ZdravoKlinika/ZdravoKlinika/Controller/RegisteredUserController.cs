using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class RegisteredUserController
    {
        RegisteredUserService userService;

        public RegisteredUserService UserService { get => userService; set => userService = value; }

        public RegisteredUserController()
        {
            userService = new RegisteredUserService();
        }

        public RegisteredUser? GetUserByEmailAndPassword(String email, String password)
        {
            return UserService.GetUserByEmailAndPassword(email, password);
        }


        public void RememberUser(RegisteredUser user)
        {
            this.userService.RememberUser(user);
        }

        public RegisteredUser GetRememberedUser()
        {
            return this.userService.GetRememberedUser();
        }

        public void ForgetUser()
        {
            this.userService.ForgetUser();
        }

        public List<RegisteredUser> GetAll()
        {
            return userService.GetAll();
        }

        public static Doctor UserToDoctor(RegisteredUser user)
        {
            return RegisteredUserService.UserToDoctor(user);
        }
    }
}
