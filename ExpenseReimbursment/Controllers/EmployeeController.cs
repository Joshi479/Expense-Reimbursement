using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models;
using ExpenseReimbursment.DAL;

namespace ExpenseReimbursment.Controllers
{
    public class EmployeeController : Controller
    {
        DataAccess _da = new DataAccess();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult EmployeeDetails(int empId)
        {
            var emp = _da.GetEmployeebyEmpId(empId); 
            return PartialView("~/Shared/_EmployeeDetails.cshtml", empId);
        }
        [HttpGet]
        public PartialViewResult GenerateExpenseReport()
        {
            //code to get Textboxes for approving based on reportId
            return PartialView("_GenerateReport");
        }

        [HttpPost]
        [ActionName("GenerateExpenseReport")]
        public ActionResult GenerateExpenseReport_Post(ExpenseReportEntity expRpt)
        {
            //to insert report in db
            return RedirectToAction("GetReportList", new { empId = expRpt.EmpId });
        }

        [HttpGet]
        public PartialViewResult GetReportList(int empId)
        {
            var reportList = _da.GetExpenseReportsbyEmpId(empId);
            return PartialView("_ViewReports", reportList);
        }

        [HttpGet]
        public PartialViewResult GetReportListApproval(int empId)
        {
            var reportList = _da.GetExpenseReportsbyApproverId(empId);
            return PartialView("_ViewReports");
        }

        [HttpGet]
        public PartialViewResult ApproveExpenseReport()
        {
            //code to get Textboxes for approving based on reportId
            return PartialView("~/Shared/_ApproveReport.cshtml");
        }

        [HttpPost]
        [ActionName("ApproveExpenseReport")]
        public JsonResult ApproveExpenseReport_Post(ExpenseReportEntity rpt)
        {
            //code to insert approved report based on rptId
            return Json(new { status = rpt.Status, amount = rpt.ApprovedAmt, comments = rpt.Comments, date = rpt.ApprovedDate });
        }

        [HttpGet]
        public PartialViewResult UpdateExpenseReport()
        {
            //code to get Textboxes for approving based on reportId
            return PartialView("~/Employee/_UpdateReport.cshtml");
        }

        [HttpPost]
        [ActionName("UpdateExpenseReport")]
        public JsonResult UpdateExpenseReport_Post(ExpenseReportEntity rpt)
        {
            //code to insert approved report based on rptId
            return Json(new { amount = rpt.ExpenseAmt, date = rpt.AppliedDate });
        }
    }
}