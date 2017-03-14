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
        DbtoEntity _de = new DbtoEntity();
        // GET: Admin
        public ActionResult Index()
        {
            RegisterViewModel model = (RegisterViewModel)TempData["model"];
            return View(model);
        }

        [HttpGet]
        public ActionResult RegisterEmployee()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("RegisterEmployee")]
        public ActionResult RegisterEmployee_Post(RegisterViewModel model)
        {
            TempData["model"] = model;
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            try
            {
                RegisterViewModel msgModel = new RegisterViewModel();
                int userId = _de.InsertEmployee(model);
                var userCreds = _de.GetUserCredentialsbyUserId(userId);
                msgModel.Message = "New Employee Registered with User Id "+ userId;
                TempData["model"] = msgModel;
                SMTPSender.ToAddress = model.Email;
                SMTPSender.Subject = "Registration successfull.";
                SMTPSender.MessageBody = "Welcome to GVB Expense Reimbursement tool. You are Sccussfully registered for the reimbursement of your expenses.\n \n Please login into the tool with below credentials.\n\n User Id: " + userCreds.UserId + "\n Password: " + userCreds.Password;
                SMTPSender.SendEmail();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.Message = "Error occured while creating employee" + ex.InnerException.Message ;
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult EmployeeList()
        {
            var empList = _de.GetEmployeeList();
            return PartialView("_EmpList", empList);
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
            
            return Json(new {emialId = emp.EmailId, Phone = emp.ContactNumber, role = emp.EmpRole.RoleName });
        }
        [HttpPost]
        public JsonResult DeactivateEmployee(int empId)
        {
            string message = string.Empty;
            try
            {
                _de.DeactivateEmplyee(empId);
                message = "Employee successfully deactivated.";
                return Json(new { success = true, msg = message });
            }
            catch (Exception ex)
            {
                message = "Employee deactivation failed due to " + ex.InnerException.Message;
            }
            return Json(new { success = false, msg = message });
        }

    }
}