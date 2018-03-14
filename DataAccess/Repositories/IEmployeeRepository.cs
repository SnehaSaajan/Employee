using DataAccess.Model;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> SearchEmployee(SearchObject obj);
        int AddEmployee(Employee employee);
    }
}
