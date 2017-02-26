using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.EntityFramework;

namespace ExpenseReimbursment.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}