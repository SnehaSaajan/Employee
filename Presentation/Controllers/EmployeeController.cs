using BusinessLogic.Entities;
using BusinessLogic.Services;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Presentation.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeServices _services;
        /// <summary>
        /// get the list of all fields for each search
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSearchFields()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Number", Value = "0" });
            items.Add(new SelectListItem { Text = "Name", Value = "1" });
            items.Add(new SelectListItem { Text = "Joining Date", Value = "2" });
            items.Add(new SelectListItem { Text = "Designation", Value = "3" });
            items.Add(new SelectListItem { Text = "Band", Value = "4" });
            return items;

        }
        /// <summary>
        /// get the list of all types with which the filter condition works
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSearchType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = ">", Value = "0" });
            items.Add(new SelectListItem { Text = "<", Value = "1" });
            items.Add(new SelectListItem { Text = ">=", Value = "2" });
            items.Add(new SelectListItem { Text = "<=", Value = "3" });
            items.Add(new SelectListItem { Text = "=", Value = "4" });
            items.Add(new SelectListItem { Text = "<>", Value = "5" });
            items.Add(new SelectListItem { Text = "LIKE", Value = "6" });
            items.Add(new SelectListItem { Text = "BETWEEN", Value = "7" });
            return items;

        }
        /// <summary>
        /// maps data transfer object with view model object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeViewModel MaptoViewModel(EmployeeDto employee)
        {
            var model = new EmployeeViewModel();

            model.EmployeeNumber = employee.EmployeeNumber;
            model.Name = employee.Name;
            model.DateOfJoining = employee.DateOfJoining;
            model.Designation = employee.Designation;
            model.Band = employee.Band;
            return model;
        }
        /// <summary>
        /// maps view model object with data transfer object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeDto MaptoDto(SearchViewModel employee)
        {
            var model = new EmployeeDto();
            model.Field = employee.Field;
            model.Type = employee.Type;
            model.Value = employee.Value;
            model.FromDate = employee.FromDate;
            model.ToDate = employee.ToDate;
            model.IsNot = employee.IsNot;
            return model;
        }

        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }

        // GET: Employee
        [HttpGet]
        public ActionResult Index()
        {

            var model = new List<EmployeeViewModel>();
            var employees = _services.GetEmployees();
            ViewData["Fields"] = GetSearchFields();
            ViewData["Types"] = GetSearchType();
            foreach (EmployeeDto record in employees)
            {
                model.Add(MaptoViewModel(record));
            }
            ViewData["Search"] = model;
            return View();
        }
        //GET :Employee with search condition
        [HttpGet]
        public ActionResult Search(SearchViewModel searchObject)
        {

            ViewData["Fields"] = GetSearchFields();
            ViewData["Types"] = GetSearchType();
            var model = new List<EmployeeViewModel>();
            var dto = MaptoDto(searchObject);
            if (searchObject.Field != "2" && searchObject.Value==null)
            {
                return RedirectToAction("Index", "Employee");
            }
            if (searchObject.Field == "0" && !(searchObject.Value.All(char.IsDigit)))
            {
                return RedirectToAction("Index", "Employee");
            }

            var employees = _services.SearchEmployees(dto);
            foreach (EmployeeDto record in employees)
            {
                model.Add(MaptoViewModel(record));
            }
            ViewData["Search"] = model;
            ModelState.Clear();
            return View(searchObject);

        }
    }
}