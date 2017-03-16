using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.DAL;
using ExpenseReimbursment.Models.Entities;



namespace ExpenseReimbursment.Models.Authorization
{
    public class UserAuthentication
    {
        public DbtoEntity _de;

        public UserAuthentication()
        {
            _de = new DbtoEntity();
        }
        public bool AuthenticateUser(UserEntity user)
        {
            var userCred = _de.GetUserCredentialsbyUserId(user.UserId);
            if(user.Password.Equals(userCred.Password))
                return true;
            return false;
        }
        public EmpRole GetUserRole(int? empId)
        {
            return _de.GetEmployeeByEmpID(empId).EmpRole;
        }
    }
}