using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseReimbursment.Models;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.DAL;

namespace ExpenseReimbursment.Controllers
{
    public class AdminController : Controller
    {
        DataAccess _da = new DataAccess();
        DbtoEntity _de = new DbtoEntity();
        // GET: Admin
        public ActionResult Index(string message = "")
        {
            return View(message);
        }

        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult RegisterEmployee()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.Message = String.Empty;
            return PartialView("_Register", model);
        }

        [HttpPost]
        [Authorize(Roles = "ADM")]
        [ActionName("RegisterEmployee")]
        public ActionResult RegisterEmployee_Post(RegisterViewModel model)
        {
            string Message = string.Empty;
            try
            {
                var userId = _de.InsertEmployee(model);
                Message = "New Employee created with employee Id" + userId.ToString();
            }
            catch (Exception ex)
            {
                Message = "Error occured while creating employee" + ex.InnerException.Message ;
            }
            
            return RedirectToAction("Index", new { Message });
        }

        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult EmployeeList()
        {
            var empList = _da.GetEmployeeList();
            return PartialView("_EmpList", empList);
        }
        
        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult ReportDetails(int reportId)
        {
            var report = _da.GetExpenseReportbyReportId(reportId);
            return PartialView("~/Shared/_ReportDetails.cshtml", report);
        }

        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult EditEmployeeDetails(EmployeeEntity emp)
        {
            return PartialView("_EditEmployeeDetails", emp);
        }

        [HttpPost]
        [Authorize(Roles = "ADM")]
        [ActionName("EditEmployeeDetails")]
        public JsonResult EditEmployeeDetails_Post(EmployeeEntity emp)
        {
            
            return Json(new {emialId = emp.EmailId, Phone = emp.ContactNumber, role = emp.EmpRole.RoleName });
        }


    }
}