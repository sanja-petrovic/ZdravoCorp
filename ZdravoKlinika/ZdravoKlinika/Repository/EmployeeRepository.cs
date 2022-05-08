using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;

namespace ZdravoKlinika.Repository
{
    public class EmployeeRepository
    {
        private EmployeeDataHandler employeeDataHandler;
        private List<Employee> employees;

        public List<Employee> Employees { get => employees; set => employees = value; }
        public EmployeeDataHandler EmployeeDataHandler { get => employeeDataHandler; set => employeeDataHandler = value; }

        public EmployeeRepository()
        {
            EmployeeDataHandler = new EmployeeDataHandler();
            Employees = EmployeeDataHandler.Read();
        }

        public List<Employee> GetAll()
        {
            return Employees;
        }

        public Employee GetById(String id)
        {
            foreach (Employee emp in Employees)
            {
                if (emp.PersonalId == id)
                {
                    return emp;
                }
            }
            return null;
        }
    }
}
