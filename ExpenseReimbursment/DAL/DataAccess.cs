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
        public List<ExpenseReport> GetExpenseReportsbyApproverRole(string approverRole)
        {
            var reports = new List<ExpenseReport>();
            var expTypes = _expcontext.ExpenseTypes.Where(et => et.Approver_Rcode == approverRole).ToList();
            foreach (var exp in expTypes)
            {
                 _expcontext.ExpenseReports.Where(rpt => rpt.ExpId == exp.ExpenseCode).ToList().ForEach(e => reports.Add(e));
            }
            return reports;
        }

        public List<ExpenseReport> GetExpenseReportsbyEmpId(int? empId)
        {
            return _expcontext.ExpenseReports.Where(exp => exp.EmpId == empId).ToList();
        }

        public Employee GetEmployeebyEmpId(int? empId)
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

        public string GetUserPasswordbyUserId(int userId)
        {
            return _expcontext.getUserPassword(userId).FirstOrDefault().ToString();
        }

        public int InsertEmployee(EmployeeEntity emp)
        {
           return _expcontext.InsertEmployee(emp.FirstName, emp.MiddleName, emp.Lastname, emp.RoleId, emp.EmailId, emp.ContactNumber, emp.Gender);
        }

        public void UpdateEmployee(EmployeeEntity emp)
        {
            _expcontext.UpdateEmployee(emp.EmpId, emp.RoleId, emp.EmailId, emp.ContactNumber);
        }

        public int InsertReport(ExpenseReportEntity report)
        {
            return _expcontext.InsertExpenseReport(report.ExpTyCode, report.EmpId, report.ExpenseAmt, report.Comments, "Applied");
        }

        public void UpdateReportApprover(ExpenseReportEntity rpt)
        {
            _expcontext.UpdateExpenseReport_Approver(rpt.ReportId, rpt.ApproverId, rpt.ApprovedAmt, rpt.Comments, rpt.Status);
        }

        public void UpdateReportEmployee(ExpenseReportEntity rpt)
        {
            _expcontext.UpdateExpenseReport_Employee(rpt.ReportId, rpt.ExpenseAmt, rpt.Comments);
        }
    }
}