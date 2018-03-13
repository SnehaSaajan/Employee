using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;
using DataAccess.Model;
using BusinessLogic.Entities;
using AutoMapper;

namespace BusinessLogic.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private IEmployeeRepository _repository;

        public EmployeeServices(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// maps datalayer object with data transfer object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employeedto</returns>
        public EmployeeDto MapEmployeeModel(Employee employee)
        {
            var dto = new EmployeeDto();
            dto.EmployeeNumber = employee.EmployeeNumber;
            dto.Name = employee.Name;
            dto.DateOfJoining = employee.DateOfJoining;
            dto.Designation = employee.Designation;
            dto.Band = employee.Band;
            return dto;
        }
        /// <summary>
        /// function to get details of all employees
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> GetEmployees()
        {
           
            var dtoList = new List<EmployeeDto>();
            var employees = _repository.GetAllEmployees();          
            foreach (Employee record in employees)
            {
                dtoList.Add(MapEmployeeModel(record));
            }
            return dtoList;
        }
        /// <summary>
        /// maps data transfer object with data layer object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>SearchObject</returns>
        public SearchObject MaptoModel(EmployeeDto employee)
        {
            var model = new SearchObject();
            model.Field = employee.Field;
            model.Type = employee.Type;
            model.Value = employee.Value;
            model.FromDate = employee.FromDate;
            model.ToDate = employee.ToDate;
            model.IsNot = employee.IsNot;
            return model;
        }
        /// <summary>
        ///returns the employee list according to search condition
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>List(Employee)</returns>
        public IEnumerable<EmployeeDto> SearchEmployees(EmployeeDto dto)
        {
            var dtoList = new List<EmployeeDto>();
            var searchObject = MaptoModel(dto);
            var employees = _repository.SearchEmployee(searchObject);
            foreach (Employee record in employees)
            {
                dtoList.Add(MapEmployeeModel(record));
            }
            return dtoList;
        }
    }
}
