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
    }
}
