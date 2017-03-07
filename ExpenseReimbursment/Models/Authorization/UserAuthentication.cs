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
        public bool AuthenticateUser(UserEntity user)
        {
            
                return true;
        }
        public Role GetUserRole(int empId)
        {
            string roleId = expContext.Employees.Where(e => e.EmpID == empId).FirstOrDefault().RoleID;
            return expContext.Roles.Where(r => r.RoleCode == roleId).FirstOrDefault();
        }
    }
}