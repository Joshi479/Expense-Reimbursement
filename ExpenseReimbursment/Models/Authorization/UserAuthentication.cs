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
        ExpenseReimbusementContext expContext = new ExpenseReimbusementContext();
        public bool AuthenticateUser(UserEntity user)
        {
            if (expContext.Users.Where(u => u.UserId == user.UserId && u.Password == user.Password).Count() == 0)
                return false;
            else
                return true;
        }
        public Role GetUserRole(int empId)
        {
            string roleId = expContext.EmployeeDetails.Where(e => e.EmpID == empId).FirstOrDefault().RoleID;
            return expContext.EmpRoles.Where(r => r.RoleCode == roleId).FirstOrDefault();
        }
    }
}