using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models;
using ExpenseReimbursment.DAL;
using ExpenseReimbursment.Models.EntityFramework;
using System.Data.Entity.Core.Objects;

namespace ExpenseReimbursment.Controllers
{
    public class EmployeeController : Controller
    {
        //db to entity
        DbtoEntity _de = new DbtoEntity();
        //to get data from db using stored procedures.
        DataAccess _da = new DataAccess();
        // GET: Employee
        public ActionResult Index()
        {
            ReportViewModel model;
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, msg = "Please enter valid details." });
            }
            try {
                emp.EmpId = Convert.ToInt32(Session["userId"]);
                _de.UpdateEmployee(emp);
                return Json(new { success = true, msg = "Employee Details updated successfully.", Id = emp.EmpId });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, msg = "Error occured while updating employee." + ex.Message });
            }            
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
                //rounding off the amount to two decimal places
                expRpt.ExpenseAmt = Math.Round((decimal)expRpt.ExpenseAmt, 2);
               _de.InsertReport(expRpt);
                //begin of changes on 05/01/2017
                var reports = new List<ExpenseReportEntity>();

                reports = _de.GetExpenseReportsbyEmpId(expRpt.EmpId);
                foreach (var exp in reports)
                {
                    if (!exp.Status.ToLower().Equals("approved")) {
                        if (exp.ExpenseAmt < 5000) //auto approve 100%
                        {
                            exp.ApprovedAmt = exp.ExpenseAmt;
                            exp.Comments = "Auto approve < 5k";
                            exp.Status = "Approved";
                            UpdateExpenseReport_Post(exp, false);
                        }
                        else if (5000 <= exp.ExpenseAmt && exp.ExpenseAmt <= 8000)//auto approve 80%
                        {
                            var amt = Convert.ToDouble(exp.ExpenseAmt);
                            amt = amt * 0.8;
                            amt = Math.Round(amt);
                            exp.ApprovedAmt = Convert.ToDecimal(amt);
                            exp.Comments = "Auto approve 5k <= amt <= 8k";
                            exp.Status = "Approved";
                            UpdateExpenseReport_Post(exp, false);
                        }
                        else //auto approve 50%
                        {
                            var amt = Convert.ToDouble(exp.ExpenseAmt);
                            amt = amt * 0.5;
                            amt = Math.Round(amt);
                            exp.ApprovedAmt = Convert.ToDecimal(amt);
                            exp.Comments = "Auto approve amt > 8k";
                            exp.Status = "Approved";
                            UpdateExpenseReport_Post(exp, false);
                        }
                    }
                }
                //end of changes             
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
            var reportList = _de.GetExpenseReportsbyApproverRole(approverRole, Convert.ToInt32(Session["userId"]));
            return PartialView("_ViewReports", reportList);
        }

        public PartialViewResult GetReportDetails(int? reportId)
        {
            var rpt = _de.GtExpenseReportbyReportId(reportId);
            return PartialView("_ReportDetails", rpt);
        }

        [HttpPost]
        [ActionName("UpdateExpenseReport")]
        public JsonResult UpdateExpenseReport_Post(ExpenseReportEntity rpt, bool isOwnRpt)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, msg = "Did you enter the right details? Please enter valid details." });
            }
            try
            {
                string message = "";
                if (isOwnRpt)
                {
                    _de.UpdateReportEmployee(rpt);
                    return Json(new { success = true, own = isOwnRpt, msg = "Report updated successfully." });
                }
                rpt.ApproverId = Convert.ToInt32(Session["userId"]);
                message = ApproveReport(rpt);
                return Json(new { success = true, msg = message });
            }
            catch (Exception ex)
            {
                return Json(new { scuccess = true, msg = "Error Occured. "+ex.Message });
            }            
        }

        public string ApproveReport(ExpenseReportEntity rpt)
        {
            _de.UpdateReportApprover(rpt);
            SMTPSender.ToAddress = _de.GetEmployeeByEmpID(rpt.EmpId).EmailId;
            SMTPSender.Subject = "Report status updated";
            if (rpt.ApprovedAmt == 0)
            {
                SMTPSender.MessageBody = "Approver updated your report status to pending. Please check comments section to proceed with further actions.";
                SMTPSender.SendEmail();
                return "Updated successfully.";
            }
            SMTPSender.MessageBody = "Your report has been approved. Amount will be transferred to your account within one week.";
            SMTPSender.SendEmail();
            return "Report approved and confirmation sent to employee.";
        }
    }
}