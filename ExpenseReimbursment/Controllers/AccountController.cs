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
                if (!ModelState.IsValid)
                {
                    message = "Employee Id and Password cannot be empty. Please login with Employee credentials.";
                    return Json(new { success = false, responseText = message });
                }
                _user.UserId = Convert.ToInt32(model.UserId);
                _user.Password = model.Password;
                if (_authUser.AuthenticateUser(_user))
                {
                    FormsAuthentication.SetAuthCookie(_user.UserId.ToString(), model.RememberMe);
                    Session["userId"] = _user.UserId;
                    return Json(new { success = true });
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
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                

                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
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
                _de.ResetUserPassword(user);
                return Json(new { success = true, message = "Password successfully reset. Please login with new password." });
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