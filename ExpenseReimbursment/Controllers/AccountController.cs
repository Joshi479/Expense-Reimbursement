using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ExpenseReimbursment.Models;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models.Authorization;
using ExpenseReimbursment.DAL;
using System.Web.Security;

namespace ExpenseReimbursment.Controllers
{
    public class AccountController : Controller
    {
        private UserEntity _user;
        private UserAuthentication _authUser;
        private DbtoEntity _de;

        public AccountController()
        {
            _user = new UserEntity();
            _authUser = new UserAuthentication();
            _de = new DbtoEntity();
        }   

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult UserValidate(LoginViewModel model)
        {
            string message;
            try
            {               
                if (!ModelState.IsValid && model.UserId == null)
                {
                    message = "Please login with valid credentials.";
                    return Json(new { success = false, responseText = message });
                }
                _user.UserId = Convert.ToInt32(model.UserId);
                _user.Password = model.Password;
                if (_authUser.AuthenticateUser(_user))
                {
                    var role = _authUser.GetUserRole(_user.UserId).RoleCode;
                    if (!role.Equals("ADM"))
                        role = "EMP";
                    if (role.Equals(model.Role))
                    {
                        FormsAuthentication.SetAuthCookie(_user.UserId.ToString(), model.RememberMe);
                        Session["userId"] = _user.UserId;
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Unauthorized Login. Are you trying to login as another role? Please try again" });
                    }
                }
                message = "Password does not match the user password! Please try again.";
                return Json(new { success = false, responseText = message });
            }
            catch (Exception ex)
            {
                message = "Invalid Employee Id! Please try again.";
            }
            return Json(new { success = false, responseText = message });
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {            
            var empEntity = _de.GetEmployeeByEmpID(Convert.ToInt32(model.UserId));
            if (empEntity.RoleId == "ADM")
            {
                return View("~/Views/Admin/Index.cshtml");
            }
            
            return RedirectToAction("Index", "Employee", null);
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return PartialView("_ForgotPassword");
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("ForgotPassword")]
        public ActionResult ForgotPassword_Post(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string error = string.Empty;
                ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(e => error += e.ErrorMessage + "\n");
                return Json(new { success = false, message = error });
            }
            var employee = _de.GetEmployeeByEmpID(model.EmpId);
            if (employee != null && employee.EmailId.Equals(model.Email))
            {
                try
                {
                    string password = _de.ForgotPassword(model.EmpId);
                    SMTPSender.ToAddress = model.Email;
                    SMTPSender.Subject = "Password Recovery for Employee.";
                    SMTPSender.MessageBody = "Your password is given below. Please login to the application using this password.\n\n\n Password: " + password;
                    SMTPSender.SendEmail();
                    return Json(new { success = true, msg = "Password is sent to the employee through an email." });
                }
                catch (Exception ex) {
                    return Json(new { success = false, msg = ex.InnerException.Message });
                }
                
            }
            return Json(new { success = false, msg = "The employee id or email address is not found. Please try again with valid details." });
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return PartialView("_ResetPassword");
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("ResetPassword")]
        public ActionResult ResetPassword_Post(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string error = string.Empty;
                    ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(e => error += e.ErrorMessage + "\n");                    
                    return Json(new { success = false, message = error });
                }
                UserEntity user = new UserEntity();
                user.UserId = Convert.ToInt32(Session["userId"]);
                user.Password = model.Password;
                var role = _authUser.GetUserRole(user.UserId);
                _de.ResetUserPassword(user);
                return Json(new { success = true, role = role.RoleCode, message = "Password successfully reset. Please login with new password." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Password reset failed try again. Error: " + ex.InnerException.Message });
            }            
        }

        
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}