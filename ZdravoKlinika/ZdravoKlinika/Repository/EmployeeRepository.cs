using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class EmployeeRepository : Interfaces.IEmployeeRepository
    {
        private EmployeeDataHandler employeeDataHandler;
        private List<Employee> employees;

        public List<Employee> Employees { get => employees; set => employees = value; }
        public EmployeeDataHandler EmployeeDataHandler { get => employeeDataHandler; set => employeeDataHandler = value; }

        public EmployeeRepository()
        {
            EmployeeDataHandler = new EmployeeDataHandler();
            Employees = EmployeeDataHandler.Read();
            if (Employees == null) Employees = new List<Employee>();
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

        public void Add(Employee item)
        {
            this.employees.Add(item);
            this.employeeDataHandler.Write(employees);
        }

        public void Remove(Employee item)
        {
            this.employees.RemoveAt(this.employees.FindIndex(e => e.PersonalId.Equals(item.PersonalId)));
            this.employeeDataHandler.Write(employees);
        }

        public void Update(Employee item)
        {
            this.employees[this.employees.FindIndex(e => e.PersonalId.Equals(item.PersonalId))] = item;
            this.employeeDataHandler.Write(employees);
        }

        public void RemoveAll()
        {
            this.employees.Clear();
            this.employeeDataHandler.Write(employees);
        }
    }
}
