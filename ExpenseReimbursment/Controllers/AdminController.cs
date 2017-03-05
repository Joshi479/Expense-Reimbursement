using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseReimbursment.Models;

namespace ExpenseReimbursment.Controllers
{
    public class AdminController : Controller
    {
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
    }
}