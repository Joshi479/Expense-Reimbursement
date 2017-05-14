using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models.EntityFramework;
using System.Data.Entity.Core.Objects;

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
           return _expcontext.Employees.Where(e => e.EmpStatus.Equals("Active")).ToList();
        }

        public string GetEmpRolecodebyId(int? empId)
        {
            return _expcontext.Employees.Where(e => e.EmpID == empId).FirstOrDefault().RoleID;
        }
        public Role GetRoleByRoleCode(string roleCode)
        {
            return _expcontext.Roles.Where(r => r.RoleCode.Equals(roleCode)).FirstOrDefault();
        }
        public List<ExpenseReport> GetExpenseReportsbyApproverRole(string approverRole, int? empId)
        {
            var reports = new List<ExpenseReport>();
            var expTypes = _expcontext.ExpenseTypes.Where(et => et.Approver_Rcode == approverRole).ToList();
            foreach (var exp in expTypes)
            {
                 _expcontext.ExpenseReports.Where(rpt => rpt.ExpId == exp.ExpenseCode && rpt.EmpId != empId && !rpt.Status.ToLower().Equals("approved")).ToList().ForEach(e => reports.Add(e));
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

        public ExpenseReport GetExpenseReportbyReportId(int? reportId)
        {
            return _expcontext.ExpenseReports.Where(exp => exp.ReportId == reportId).FirstOrDefault();
        }
        public ExpenseType GetExpenseTypeByCode(string expCode)
        {
            return _expcontext.ExpenseTypes.Where(exp => exp.ExpenseCode.Equals(expCode)).FirstOrDefault();
        }

        public string GetUserPasswordbyUserId(int? userId)
        {
            return _expcontext.getUserPassword(userId).FirstOrDefault().ToString();
        }

        public int InsertEmployee(EmployeeEntity emp)
        {
            ObjectParameter userId = new ObjectParameter("UserId", typeof(int));
            _expcontext.InsertEmployee(emp.FirstName, emp.MiddleName, emp.Lastname, emp.RoleId, emp.EmailId, emp.ContactNumber, emp.Gender, userId);
            return Convert.ToInt32(userId.Value);
        }

        public void UpdateEmployee(EmployeeEntity emp)
        {
            _expcontext.UpdateEmployee(emp.EmpId, emp.EmailId, emp.ContactNumber);
        }

        public int InsertReport(ExpenseReportEntity report)
        {
            return _expcontext.InsertExpenseReport(report.ExpTyCode, report.EmpId, report.ExpenseAmt, report.Comments, "Applied");
        }

        public void UpdateReportApprover(ExpenseReportEntity rpt)
        {
            if (rpt.ApprovedAmt != 0)
            {
                var report = _expcontext.ExpenseReports.Where(e => e.ReportId == rpt.ReportId).First();
                report.ApproverId = rpt.ApproverId;
                report.ApprovedAmt = rpt.ApprovedAmt;
                report.Comments = rpt.Comments;
                report.Status = rpt.Status;
                _expcontext.SaveChanges();
            }
            else
            {
                var report = _expcontext.ExpenseReports.Where(e => e.ReportId == rpt.ReportId).First();
                report.Status = "Pending";
                report.Comments += ";Approver: " + rpt.Comments;
                _expcontext.SaveChanges();
            }
            
        }

        public void UpdateReportEmployee(ExpenseReportEntity rpt)
        {
            var report = _expcontext.ExpenseReports.Where(e => e.ReportId == rpt.ReportId).First();
            report.ExpenseAmt = rpt.ExpenseAmt;
            report.ApprovedAmt = rpt.ApprovedAmt;
            report.Comments += ";Emplyee: "+rpt.Comments;
            _expcontext.SaveChanges();
        }

        public void DeactivateEmployee(int? empId)
        {
            _expcontext.DeleteEmployee(empId);
        }

        public void ResetUserPassword(UserEntity user)
        {
            _expcontext.UpdateUsers(user.UserId, user.Password);
        }
        public string ForgotPassword(int? userId)
        {
            ObjectParameter password = new ObjectParameter("Userpassword", typeof(string));
            _expcontext.updateForgotPassword(userId, password);
            return password.Value.ToString();
        }
    }
}