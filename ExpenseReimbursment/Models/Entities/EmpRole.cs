using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReimbursment.Models.Entities
{
    public class EmpRole
    {
        private string _roleName;
        private string _roleCode;

        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return _roleCode;
            }
            set
            {
                _roleCode = value;
            }
        }
    }
}