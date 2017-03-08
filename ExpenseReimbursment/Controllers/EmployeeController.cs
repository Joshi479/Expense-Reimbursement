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
        DbtoEntity _de = new DbtoEntity();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult EmployeeDetails(int? empId)
        {
            var emp = _de.GetEmployeeByEmpID(empId); 
            return PartialView("~/Shared/_EmployeeDetails.cshtml", empId);
        }
        [HttpGet]
        public PartialViewResult GenerateExpenseReport()
        {
            ReportViewModel report = new ReportViewModel();
            return PartialView("_GenerateReport", report);
        }

        [HttpPost]
        [ActionName("GenerateExpenseReport")]
        public ActionResult GenerateExpenseReport_Post(ReportViewModel expRpt)
        {
            _de.InsertReport(expRpt);
            return RedirectToAction("GetReportList", new { empId = expRpt.EmpId });
        }

        [HttpGet]
        public PartialViewResult GetReportList(int? empId)
        {
            var reportList = _de.GetExpenseReportsbyEmpId(empId);
            return PartialView("_ViewReports", reportList);
        }

        [HttpGet]
        public PartialViewResult GetReportListApproval(string approverRole)
        {
            var reportList = _de.GetExpenseReportsbyApproverRole(approverRole);
            return PartialView("_ViewReports", reportList);
        }

        [HttpGet]
        public PartialViewResult ApproveExpenseReport()
        {
            return PartialView("~/Shared/_ApproveReport.cshtml");
        }

        [HttpPost]
        [ActionName("ApproveExpenseReport")]
        public JsonResult ApproveExpenseReport_Post(ExpenseReportEntity rpt)
        {
            _de.UpdateReportApprover(rpt);
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
            _de.UpdateReportEmployee(rpt);
            return Json(new { amount = rpt.ExpenseAmt, date = rpt.AppliedDate });
        }
    }
}