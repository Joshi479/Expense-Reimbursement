using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReimbursment.Models.Entities
{
    public class ExpenseType
    {
        private string _expCode;
        private string _expName;
        private string _apprCode;
        private Role _empRole;

        public string ExpCode
        {
            get
            {
                return _expCode;
            }
            set
            {
                _expCode = value;
            }
        }

        public string ExpName
        {
            get
            {
                return _expName;
            }
            set
            {
                _expName = value;
            }
        }

        public string ApprCode
        {
            get
            {
                return _apprCode;
            }
            set
            {
                _apprCode = value;
            }
        }

        public Role EmpRole
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