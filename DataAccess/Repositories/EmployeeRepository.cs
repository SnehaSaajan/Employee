using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.DBContext;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// gets all employee details from database
        /// </summary>
        /// <returns>List(Employee)</returns>
        public IEnumerable<Employee> GetAllEmployees()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                var employeeList = context.Employees.ToList();
                return employeeList;
            }
        }
        /// <summary>
        /// gets all employees according to the search conditions
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>List(Employee)</returns>
        public IEnumerable<Employee> SearchEmployee(SearchObject obj)
        {
            var employeeList = new List<Employee>();
            using (EmployeeContext context = new EmployeeContext())
            {
                employeeList = context.Employees.ToList();
            }
            var employeeSearchList = new List<Employee>();
            switch (obj.Field)
            {
                case "0":
                    employeeSearchList = GetEmployeeByNumber(obj, employeeList).ToList();
                    break;
                case "1":
                    employeeSearchList = GetEmployeeByName(obj, employeeList).ToList();
                    break;
                case "2":
                    employeeSearchList = GetEmployeeByDateOfJoining(obj, employeeList).ToList();
                    break;
                case "3":
                    employeeSearchList = GetEmployeeByDesignation(obj, employeeList).ToList();
                    break;
                case "4":
                    employeeSearchList = GetEmployeeByBand(obj, employeeList).ToList();
                    break;
                default:
                    break;
            }

            return employeeSearchList;
        }
        /// <summary>
        /// get all employees according to filtering in employee number
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeByNumber(SearchObject obj, List<Employee> employee)
        {
            var employeeList = new List<Employee>();
            switch (obj.Type)
            {
                case "0":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber > Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber < Convert.ToInt32(obj.Value)).ToList();

                    break;
                case "1":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber < Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber > Convert.ToInt32(obj.Value)).ToList();

                    break;
                case "2":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber >= Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber <= Convert.ToInt32(obj.Value)).ToList();

                    break;
                case "3":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber <= Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber >= Convert.ToInt32(obj.Value)).ToList();

                    break;
                case "4":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber == Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber != Convert.ToInt32(obj.Value)).ToList();

                    break;
                case "5":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.EmployeeNumber != Convert.ToInt32(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.EmployeeNumber == Convert.ToInt32(obj.Value)).ToList();
                    break;
                default:
                    break;
            }
            return employeeList;
        }

        /// <summary>
        /// get all employees according to filtering in date of joining
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeByDateOfJoining(SearchObject obj, List<Employee> employee)
        {
            var employeeList = new List<Employee>();

            switch (obj.Type)
            {
                case "0":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining > obj.FromDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining < obj.FromDate).ToList();

                    break;
                case "1":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining < obj.FromDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining > obj.FromDate).ToList();

                    break;
                case "2":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining >= obj.FromDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining <= obj.FromDate).ToList();

                    break;
                case "3":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining <= obj.FromDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining >= obj.FromDate).ToList();

                    break;
                case "4":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining == obj.FromDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining != obj.FromDate).ToList();

                    break;
                case "7":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.DateOfJoining >= obj.FromDate && p.DateOfJoining < obj.ToDate).ToList();
                    else
                        employeeList = employee.Where(p => p.DateOfJoining < obj.FromDate || p.DateOfJoining > obj.ToDate).ToList();

                    break;
                default:
                    break;
            }
            return employeeList;
        }

        /// <summary>
        /// get all employees according to filtering in employee name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeByName(SearchObject obj, List<Employee> employee)
        {
            var employeeList = new List<Employee>();
            switch (obj.Type)
            {
                case "4":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.Name == obj.Value).ToList();
                    else
                        employeeList = employee.Where(p => p.Name != obj.Value).ToList();

                    break;
                case "5":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.Name != obj.Value).ToList();
                    else
                        employeeList = employee.Where(p => p.Name == obj.Value).ToList();

                    break;
                case "6":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.Name.Contains(obj.Value)).ToList();
                    else
                        employeeList = employee.Where(p => p.Name.Contains(obj.Value)).ToList();

                    break;

                default:
                    break;
            }
            return employeeList;
        }

        /// <summary>
        /// get all employees according to filtering in employee designation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeByDesignation(SearchObject obj, List<Employee> employee)
        {
            var employeeList = new List<Employee>();
            switch (obj.Type)
            {
                case "4":

                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.Designation == obj.Value).ToList();
                    else
                        employeeList = employee.Where(p => p.Name != obj.Value).ToList();

                    break;

                default:
                    break;
            }
            return employeeList;
        }

        /// <summary>
        /// get all employees according to filtering in employee band
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeByBand(SearchObject obj, List<Employee> employee)
        {
            var employeeList = new List<Employee>();
            switch (obj.Type)
            {
                case "4":
                    if (obj.IsNot != true)
                        employeeList = employee.Where(p => p.Band == obj.Value).ToList();
                    else
                        employeeList = employee.Where(p => p.Band != obj.Value).ToList();
                    break;

                default:
                    break;
            }
            return employeeList;
        }
    }
}
