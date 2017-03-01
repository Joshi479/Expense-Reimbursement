using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReimbursment.Models.Entities
{
    public class ExpenseReport
    {
        private int _reportId;
        private string _expTyCode;
        private int _empId;
        private int _expenseAmt;
        private int _approverId;
        private int _approvedAmt;
        private string _comments;
        private string _appliedDate;
        private string _approvedDate;
        private string _status;
        private Employee _employee;
        private Employee _approver;
        private ExpenseType _expType;

        public int ReportId
        {
            get
            {
                return _reportId;
            }
            set
            {
                _reportId = value;
            }
        }

        public string ExpTyCode
        {
            get
            {
                return _expTyCode;
            }
            set
            {
                _expTyCode = value;
            }
        }

        public int EmpId
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

        public int ExpenseAmt
        {
            get
            {
                return _expenseAmt;
            }
            set
            {
                _expenseAmt = value;
            }
        }

        public int ApproverId
        {
            get
            {
                return _approverId;
            }
            set
            {
                _approverId = value;
            }
        }

        public int ApprovedAmt
        {
            get
            {
                return _approvedAmt;
            }
            set
            {
                _approvedAmt = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        public string AppliedDate
        {
            get
            {
                return _appliedDate;
            }
            set
            {
                _appliedDate = value;
            }
        }

        public string ApprovedDate
        {
            get
            {
                return _approvedDate;
            }
            set
            {
                _approvedDate = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
            }
        }

        public Employee Approver
        {
            get
            {
                return _approver;
            }
            set
            {
                _approver = value;
            }
        }

        
        public ExpenseType ExpType
        {
            get
            {
                return _expType;
            }
            set
            {
                _expType = value;
            }
        }

    }
}