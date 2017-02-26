using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models.EntityFramework;


namespace ExpenseReimbursment.Models.Authorization
{
    public class UserAuthentication
    {
        ExpenseReimbursementContext expContext = new ExpenseReimbursementContext();
        public void AuthenticateUser(User user)
        {
            
        }
    }
}