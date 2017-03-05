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
        ExpenseReimbusementContext _expcontext;
        public DataAccess()
        {
            _expcontext = new ExpenseReimbusementContext();
        } 

        public List<Employee> GetEmployeeList()
        {
           return _expcontext.EmployeeDetails.ToList();
        }

        public List<ExpenseReport> GetExpenseReportsbyApproverId(int empId)
        {
            return _expcontext.ExpReports.Where(exp => exp.ApproverId == empId).ToList();
        }

        public List<ExpenseReport> GetExpenseReportsbyEmpId(int empId)
        {
            return _expcontext.ExpReports.Where(exp => exp.EmpId == empId).ToList();
        }

        public Employee GetEmployeebyEmpId(int empId)
        {
            return _expcontext.EmployeeDetails.Where(emp => emp.EmpID == empId).FirstOrDefault();
        }

        public ExpenseReport GetReportbyReportId(int reportId)
        {
            return _expcontext.ExpReports.Where(exp => exp.ReportId == reportId).FirstOrDefault();
        }
    }
}