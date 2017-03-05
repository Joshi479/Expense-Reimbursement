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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="ADM")]
        public PartialViewResult RegisterEmployee()
        {
            return PartialView("_Register");
        }

        [HttpPost]
        [Authorize(Roles = "ADM")]
        [ActionName("RegisterEmployee")]
        public PartialViewResult RegisterEmployee_Post(RegisterViewModel model)
        {
            //code to insert into empdetails and user tables
            return PartialView("_Register");
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
        public PartialViewResult ReportList(int empId)
        {
            var reportList = _da.GetExpenseReportsbyApproverId(empId);
            return PartialView("~/Shared/_ReportList.cshtml", reportList);
        }

        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult ReportDetails(int reportId)
        {
            var report = _da.GetReportbyReportId(reportId);
            return PartialView("~/Shared/_ReportDetails.cshtml", report);
        }

        [HttpGet]
        [Authorize(Roles = "ADM")]
        public PartialViewResult EditEmployeeDetails()
        {
            return PartialView("_EditEmployeeDetails");
        }

        [HttpPost]
        [Authorize(Roles = "ADM")]
        [ActionName("EditEmployeeDetails")]
        public JsonResult EditEmployeeDetails_Post(EmployeeEntity emp)
        {
            //code to insert edited employee based on empid
            return Json(new {emialId = emp.EmailId, Phone = emp.ContactNumber, role = emp.EmpRole.RoleName });
        }


    }
}