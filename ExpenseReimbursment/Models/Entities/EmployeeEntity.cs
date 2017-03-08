using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReimbursment.Models.Entities
{
    public class EmployeeEntity
    {
        private int? _empId;
        private string _firstName;
        private string _middleName;
        private string _lastname;
        private string _gender;
        private string _roleId;
        private string _emailId;
        private string _contactNumber;
        private EmpRole _empRole;

        public int? EmpId
        {
            get
            {
                return _empId;
            }
            set
            {
                _empId = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                _middleName = value;
            }
        }

        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        public string RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }

        public string EmailId
        {
            get
            {
                return _emailId;
            }
            set
            {
                _emailId = value;
            }
        }

        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                _contactNumber = value;
            }
        }

        public EmpRole EmpRole
        {
            get
            {
                return _empRole;
            }
            set
            {
                _empRole = value;
            }
        }
    }
}