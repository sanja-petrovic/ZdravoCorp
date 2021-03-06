using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class RegisteredUserRepository : Interfaces.IRegisteredUserRepository
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

            ReadDataFromFile();

        }

        private void ReadDataFromFile()
        {

            List<RegisteredPatient> rpats = RegisteredPatientRepository.GetAll();
            if(rpats != null) 
            {
                foreach (RegisteredPatient pat in rpats)
                {
                    this.Add(pat);
                }
            }
            List<Doctor> docs = DoctorRepository.GetAll();
            if (docs != null)
            {
                foreach (Doctor doc in docs)
                {
                    this.Add(doc);
                }
            }
            List<Employee> emps = EmployeeRepository.GetAll();
            if (emps != null)
            {
                foreach (Employee emp in emps)
                {
                    this.Add(emp);
                }
            }
        }

        public  List<RegisteredUser> GetAll()
        {
            ReadDataFromFile();
            return RegisteredUsers;
        }

        public RegisteredUser? GetUserByEmailAndPassword(String email, String password) 
        {
            RegisteredUser? userToReturn = null;
            foreach (RegisteredUser user in RegisteredUsers)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password)) 
                {
                    userToReturn = user;
                    break;
                }
            }
            return userToReturn;
        }
        public void Add(RegisteredUser newUser)
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

        public RegisteredUser? GetById(string id)
        {
            RegisteredUser? userToReturn = null;
            foreach (RegisteredUser user in RegisteredUsers)
            {
                if (user.PersonalId.Equals(id))
                {
                    userToReturn = user;
                    break;
                }
            }
            return userToReturn;
        }

        public void Remove(RegisteredUser item)
        {
            if (registeredUsers != null)
                registeredUsers.Remove(item);
        }

        public void Update(RegisteredUser item)
        {
            int index = GetIndex(item.PersonalId);
            if (index != -1)
            {
                registeredUsers[index] = item;
            }
        }

        public void RemoveAll()
        {
            if (registeredUsers != null)
                registeredUsers.Clear();
        }
        private int GetIndex(String id)
        {
            int indexToRemove = -1;
            foreach (RegisteredUser user in registeredUsers)
            {
                if (user.PersonalId.Equals(id))
                {
                    indexToRemove = registeredUsers.IndexOf(user);
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception("User does not exist");
            }
            return indexToRemove;
        }

    }
}
