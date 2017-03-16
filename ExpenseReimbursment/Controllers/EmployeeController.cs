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
        DbtoEntity _de = new DbtoEntity();
        // GET: Employee
        public ActionResult Index()
        {
            ReportViewModel model;
            var empEntity = _de.GetEmployeeByEmpID(Convert.ToInt32(Session["userId"]));
            if (TempData["model"] == null )
            {                
                model = new ReportViewModel();  
            }
            else
                model = (ReportViewModel)TempData["model"];
            
                model.EmpId = Convert.ToInt32(Session["userId"]);
                model.Name = empEntity.FirstName + " " + empEntity.Lastname;
                model.RoleId = empEntity.RoleId;                            
            return View(model);
        }
        [HttpGet]
        public PartialViewResult EmployeeDetails(int? empId)
        {
            var emp = _de.GetEmployeeByEmpID(empId); 
            return PartialView("_EmployeeDetails", emp);
        }

        [HttpGet]
        public PartialViewResult EditEmployeeDetails(EmployeeEntity emp)
        {
            return PartialView("_EditEmployeeDetails", emp);
        }

        [HttpPost]
        [ActionName("EditEmployeeDetails")]
        public JsonResult EditEmployeeDetails_Post(EmployeeEntity emp)
        {

            return Json(new { emialId = emp.EmailId, Phone = emp.ContactNumber, role = emp.EmpRole.RoleName });
        }

        [HttpPost]
        [ActionName("GenerateExpenseReport")]
        public ActionResult GenerateExpenseReport_Post(ReportViewModel expRpt)
        {
            TempData["model"] = expRpt;
            if (!ModelState.IsValid)
            {
                expRpt.Message = "The Data you entered is not valid data. Please try again.";
                return RedirectToAction("Index", expRpt);
            }
            try
            {
                ReportViewModel msgModel = new ReportViewModel();
                expRpt.EmpId = Convert.ToInt32(Session["userId"]);
               _de.InsertReport(expRpt);                
                msgModel.Message = "Report successfully generated";
                TempData["model"] = msgModel;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                expRpt.Message = "Error occured while Generating Report" + ex.InnerException.Message;
            }

            return RedirectToAction("Index");
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