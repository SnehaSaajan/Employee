using BusinessLogic.Entities;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IEmployeeServices
    {
        EmployeeDto MapEmployeeModel(Employee employee);
        IEnumerable<EmployeeDto> GetEmployees();
        IEnumerable<EmployeeDto> SearchEmployees(EmployeeDto dto);
        Employee MapDtoModel(EmployeeDto dto);
        string AddNewEmployee(EmployeeDto dto);
    }
}
