using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models;

namespace ExpenseReimbursment.DAL
{
    public class DbtoEntity
    {
        public DataAccess _da;

        public DbtoEntity()
        {
            _da = new DataAccess();
        }

        public List<EmployeeEntity> GetEmployeeList()
        {
            var empList = _da.GetEmployeeList();
            List<EmployeeEntity> empEntityList = new List<EmployeeEntity>();
            foreach (var emp in empList)
            {
                var employee = new EmployeeEntity();
                employee.EmpId = emp.EmpID;
                employee.EmailId = emp.EmailID;
                employee.EmpRole = GetEmployeeRoleByRoleCode(emp.RoleID);
                employee.RoleId = emp.RoleID;
                employee.FirstName = emp.FirstName;
                employee.Lastname = emp.LastName;
                employee.MiddleName = emp.MiddleName;
                employee.ContactNumber = emp.ContactNum;
                employee.Gender = emp.Gender;
                empEntityList.Add(employee);
            }
            return empEntityList;
        }

        

        public List<ExpenseReportEntity> GetExpenseReportsbyEmpId(int? empId)
        {
            var repList = _da.GetExpenseReportsbyEmpId(empId);
            List<ExpenseReportEntity> reportList = new List<ExpenseReportEntity>();
            foreach (var rep in repList)
            {
                var report = new ExpenseReportEntity();
                report.AppliedDate = rep.AppliedDate.ToString("MM/dd/yyyy");
                report.ApprovedAmt = rep.ApprovedAmt;
                report.ApprovedDate = Convert.ToString(rep.ApprovalDate);
                if (rep.ApproverId != null)
                {
                    report.Approver = GetEmployeeByEmpID(Convert.ToInt32(rep.ApproverId));
                }
                report.EmpId = rep.EmpId;
                report.ApproverId = Convert.ToInt32(rep.ApproverId);
                report.Employee = GetEmployeeByEmpID(Convert.ToInt32(rep.EmpId));
                report.ExpenseAmt = rep.ExpenseAmt;
                report.ExpTyCode = rep.ExpId;
                report.ExpType = GetExpenseTypeByCode(rep.ExpId);
                report.ReportId = rep.ReportId;
                report.Status = rep.Status;
                report.Comments = rep.Comments;
                reportList.Add(report);
            }
            return reportList;
        }

        public List<ExpenseReportEntity> GetExpenseReportsbyApproverRole(string approverRole, int?empId)
        {
            var rptList = _da.GetExpenseReportsbyApproverRole(approverRole, empId);
            List<ExpenseReportEntity> reportList = new List<ExpenseReportEntity>();
            foreach (var rep in rptList)
            {
                var report = new ExpenseReportEntity();
                report.AppliedDate = rep.AppliedDate.ToString("MM/dd/yyyy");
                report.ApprovedAmt = rep.ApprovedAmt;
                if (rep.ApproverId != null)
                {
                    report.Approver = GetEmployeeByEmpID(rep.ApproverId);
                    report.ApprovedDate = ((DateTime)rep.ApprovalDate).ToString("MM/dd/yyyy");
                }
                report.ApproverId = rep.ApproverId;
                report.EmpId = rep.EmpId;
                report.Employee = GetEmployeeByEmpID(rep.EmpId);
                report.ExpenseAmt = rep.ExpenseAmt;
                report.ExpTyCode = rep.ExpId;
                report.ExpType = GetExpenseTypeByCode(rep.ExpId);
                report.ReportId = rep.ReportId;
                report.Status = rep.Status;
                report.Comments = rep.Comments;
                reportList.Add(report);
            }
            return reportList;
        }

        public EmployeeEntity GetEmployeeByEmpID(int? empId)
        {
            var emp = _da.GetEmployeebyEmpId(empId);
            var employee = new EmployeeEntity();
            employee.EmpId = emp.EmpID;
            employee.EmailId = emp.EmailID;
            employee.EmpRole = GetEmployeeRoleByRoleCode(emp.RoleID);
            employee.RoleId = emp.RoleID;
            employee.FirstName = emp.FirstName;
            employee.Lastname = emp.LastName;
            employee.MiddleName = emp.MiddleName;
            employee.ContactNumber = emp.ContactNum;
            employee.Gender = emp.Gender;

            return employee;
        }

        public ExpenseTypeEntity GetExpenseTypeByCode(string expCode)
        {
            var expType = _da.GetExpenseTypeByCode(expCode);
            var expTypeEntity = new ExpenseTypeEntity();
            expTypeEntity.ApprCode = expType.Approver_Rcode;
            expTypeEntity.ExpCode = expType.ExpenseCode;
            expTypeEntity.ExpName = expType.ExpenseName;
            expTypeEntity.EmpRole = GetEmployeeRoleByRoleCode(expType.Approver_Rcode);

            return expTypeEntity;
        }

        public EmpRole GetEmployeeRoleByRoleCode(string roleCode)
        {
            var empRole = _da.GetRoleByRoleCode(roleCode);
            var empRoleEntity = new EmpRole();
            empRoleEntity.RoleCode = empRole.RoleCode;
            empRoleEntity.RoleName = empRole.RoleName;
            return empRoleEntity;
        }

        public ExpenseReportEntity GtExpenseReportbyReportId(int? reportId)
        {
            var report = _da.GetExpenseReportbyReportId(reportId);
            var repEntity = new ExpenseReportEntity();
            repEntity.ReportId = report.ReportId;
            repEntity.AppliedDate = report.AppliedDate.ToString("MM/dd/yyyy");
            repEntity.EmpId = Convert.ToInt32(report.EmpId);
            repEntity.Employee = GetEmployeeByEmpID(repEntity.EmpId);
            if (report.ApproverId != null && report.ApproverId != 0)
            {
                repEntity.Approver = GetEmployeeByEmpID(Convert.ToInt32(report.ApproverId));
                repEntity.ApprovedDate = report.ApprovalDate.ToString();
            }
                
            repEntity.ApproverId = report.ApproverId;
            repEntity.ExpenseAmt = report.ExpenseAmt;
            repEntity.ExpTyCode = report.ExpId;
            repEntity.ExpType = GetExpenseTypeByCode(report.ExpId);
            repEntity.Status = report.Status;
            repEntity.Comments = report.Comments;

            return repEntity;
        }

        public UserEntity GetUserCredentialsbyUserId(int? empId)
        {
            var user = new UserEntity(); 
            user.UserId = empId;
            user.Password = _da.GetUserPasswordbyUserId(empId);

            return user;
        }

        public int InsertEmployee(RegisterViewModel employee)
        {
            var emp = new EmployeeEntity();
            emp.FirstName = employee.FirstName;
            emp.MiddleName = employee.MiddleName;
            emp.Lastname = employee.LastName;
            emp.RoleId = employee.RoleId;
            emp.EmailId = employee.Email;
            emp.ContactNumber = employee.PhnNumber;
            emp.Gender = employee.Gender;

            return _da.InsertEmployee(emp);             
        }

        public void UpdateEmployee(EmployeeEntity emp)
        {
            _da.UpdateEmployee(emp);
        }

        public int InsertReport(ReportViewModel report)
        {
            var rpt = new ExpenseReportEntity();
            rpt.EmpId = report.EmpId;
            rpt.ExpTyCode = report.ReportType;
            rpt.ExpenseAmt = report.ExpenseAmt;
            rpt.Comments = report.Comments;

            return _da.InsertReport(rpt);
        }

        public void UpdateReportApprover(ExpenseReportEntity rpt)
        {
            _da.UpdateReportApprover(rpt);
        }

        public void UpdateReportEmployee(ExpenseReportEntity rpt)
        {
            _da.UpdateReportEmployee(rpt);
        }

        public void DeactivateEmplyee(int? empId)
        {
            _da.DeactivateEmployee(empId);
        }

        public void ResetUserPassword(UserEntity user)
        {
            _da.ResetUserPassword(user);
        }

        public string ForgotPassword(int? userId)
        {
            return GetUserCredentialsbyUserId(userId).Password;
        }
    }
}