using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;

namespace ZdravoKlinika.Repository
{
    public class RegisteredUserRepository
    {
        List<RegisteredUser> registeredUsers;
        RegisteredPatientRepository registeredPatientRepository;
        DoctorRepository doctorRepository;
        EmployeeRepository employeeRepository;

        
        public RegisteredPatientRepository RegisteredPatientRepository { get => registeredPatientRepository; set => registeredPatientRepository = value; }
        public DoctorRepository DoctorRepository { get => doctorRepository; set => doctorRepository = value; }
        public EmployeeRepository EmployeeRepository { get => employeeRepository; set => employeeRepository = value; }
        public List<RegisteredUser> RegisteredUsers { get => registeredUsers; set => registeredUsers = value; }

        public RegisteredUserRepository()
        {
            RegisteredPatientRepository = new RegisteredPatientRepository();
            DoctorRepository = new DoctorRepository();
            EmployeeRepository = new EmployeeRepository();
            FillList();
        }

        private void FillList()
        {

            List<RegisteredPatient> rpats = RegisteredPatientRepository.GetAll();
            if(rpats != null) 
            {
                foreach (RegisteredPatient pat in rpats)
                {
                    this.AddUser(pat);
                }
            }
            List<Doctor> docs = DoctorRepository.GetAll();
            if (docs != null)
            {
                foreach (Doctor doc in docs)
                {
                    this.AddUser(doc);
                }
            }
            List<Employee> emps = EmployeeRepository.GetAll();
            if (emps != null)
            {
                foreach (Employee emp in emps)
                {
                    this.AddUser(emp);
                }
            }
        }

        public RegisteredUser? GetUserByEmailAndPassword(String email, String password) 
        {
            foreach (RegisteredUser user in RegisteredUsers)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password)) 
                {
                    return user;
                }
            }
            return null;
        }

        private void AddUser(RegisteredUser newUser)
        {
            if (newUser == null)
                return;
            if (this.registeredUsers == null)
                this.registeredUsers = new List<RegisteredUser>();
            if (!this.registeredUsers.Contains(newUser))
                this.registeredUsers.Add(newUser);
        }

        public void RememberUser(RegisteredUser user)
        {
            CurrentUserDataHandler dataHandler = new CurrentUserDataHandler();
            dataHandler.Clear();
            dataHandler.Write(user);
        }

        public RegisteredUser GetRememberedUser()
        {
            CurrentUserDataHandler dataHandler = new CurrentUserDataHandler();
            RegisteredUser user = dataHandler.Read();

            return user;
        }

        public void ForgetUser()
        {
            CurrentUserDataHandler dataHandler = new CurrentUserDataHandler();
            dataHandler.Clear();
        }

    }
}
