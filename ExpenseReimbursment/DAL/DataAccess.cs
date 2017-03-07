using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models.EntityFramework;

namespace ExpenseReimbursment.DAL
{
    public class DataAccess
    {
        ExpenseReimbursementContext _expcontext;
        public DataAccess()
        {
            _expcontext = new ExpenseReimbursementContext();
        } 

        public List<Employee> GetEmployeeList()
        {
           return _expcontext.Employees.ToList();
        }

        public Role GetRoleByRoleCode(string roleCode)
        {
            return _expcontext.Roles.Where(r => r.RoleCode.Equals(roleCode)).FirstOrDefault();
        }
        public List<ExpenseReport> GetExpenseReportsbyApproverId(int empId)
        {
            return _expcontext.ExpenseReports.Where(exp => exp.ApproverId == empId).ToList();
        }

        public List<ExpenseReport> GetExpenseReportsbyEmpId(int empId)
        {
            return _expcontext.ExpenseReports.Where(exp => exp.EmpId == empId).ToList();
        }

        public Employee GetEmployeebyEmpId(int empId)
        {
            return _expcontext.Employees.Where(emp => emp.EmpID == empId).FirstOrDefault();
        }

        public ExpenseReport GetExpenseReportbyReportId(int reportId)
        {
            return _expcontext.ExpenseReports.Where(exp => exp.ReportId == reportId).FirstOrDefault();
        }
        public ExpenseType GetExpenseTypeByCode(string expCode)
        {
            return _expcontext.ExpenseTypes.Where(exp => exp.ExpenseCode.Equals(expCode)).FirstOrDefault();
        }
    }
}