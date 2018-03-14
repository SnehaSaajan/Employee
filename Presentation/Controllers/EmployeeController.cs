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
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }
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
        /// get the list of designations for employee
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDesignations()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "SE", Value = "SE" });
            items.Add(new SelectListItem { Text = "HR", Value = "HR" });
            items.Add(new SelectListItem { Text = "TL", Value = "TL" });
            items.Add(new SelectListItem { Text = "PM", Value = "PM" });
            items.Add(new SelectListItem { Text = "BA", Value = "BA" });
            return items;

        }
        /// <summary>
        /// get the list of BANDS for employee
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBands()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "A1", Value = "A1" });
            items.Add(new SelectListItem { Text = "A2", Value = "A2" });
            items.Add(new SelectListItem { Text = "B1", Value = "B1" });
            items.Add(new SelectListItem { Text = "B2", Value = "B2" });
            items.Add(new SelectListItem { Text = "C1", Value = "C1" });
            items.Add(new SelectListItem { Text = "C2", Value = "C2" });
            return items;

        }

        /// <summary>
        /// maps data transfer object with view model object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeViewModel MapDtotoViewModel(EmployeeDto employee)
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
        /// maps search view model object with data transfer object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeDto MapSearchModeltoDto(SearchViewModel employee)
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
        public EmployeeDto MapViewModeltoDto(EmployeeViewModel employee)
        {
            var model = new EmployeeDto();
            model.EmployeeNumber = employee.EmployeeNumber;
            model.Name = employee.Name;
            model.DateOfJoining = employee.DateOfJoining;
            model.Designation = employee.Designation;
            model.Band = employee.Band;          
            return model;
        }

        // GET: Employee
        [HttpGet]
        public ActionResult Index()
        {
            var model = new List<EmployeeViewModel>();
            var employees = _services.GetEmployees();
            ViewData["Fields"] = GetSearchFields();
            ViewData["Types"] = GetSearchType();
            ViewData["Designations"] = GetDesignations();
            ViewData["Bands"] = GetBands();
            foreach (EmployeeDto record in employees)
            {
                model.Add(MapDtotoViewModel(record));
            }
            ViewData["Search"] = model;
            return View();
        }

       
        //GET :Employee with search condition
        [HttpGet]
        public ActionResult Search(SearchViewModel searchObject,string Bands,string Designations)
        {

            ViewData["Fields"] = GetSearchFields();
            ViewData["Types"] = GetSearchType();
            ViewData["Designations"] = GetDesignations();
            ViewData["Bands"] = GetBands();
           
            try
            {
                var model = new List<EmployeeViewModel>();
                if (searchObject.Field == "3")
                {
                    searchObject.Value = Designations;
                }
                if (searchObject.Field == "4")
                {
                    searchObject.Value = Bands;
                }
                var dto = MapSearchModeltoDto(searchObject);
              
                var employees = _services.SearchEmployees(dto);
                foreach (EmployeeDto record in employees)
                {
                    model.Add(MapDtotoViewModel(record));
                }
                ViewData["Search"] = model;
            }
            catch(Exception)
            {
                TempData["message"] = "Filter not possible";
                return RedirectToAction("Index", "Employee");
            }
            ModelState.Clear();
            return View(searchObject);

        }

        public ActionResult NewEmployee()
        {
            ViewData["Designations"] = GetDesignations();
            ViewData["Bands"] = GetBands();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewEmployee([Bind] EmployeeViewModel employee)
        {           
            ViewData["Designations"] = GetDesignations();
            ViewData["Bands"] = GetBands();
            if (ModelState.IsValid)
            {
                ViewBag.message = _services.AddNewEmployee(MapViewModeltoDto(employee));
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Designations"] = GetDesignations();
                ViewData["Bands"] = GetBands();
                return View();
            }
        
        }

    }
}