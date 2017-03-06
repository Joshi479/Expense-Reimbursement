using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.Entities;

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

        

        public List<ExpenseReportEntity> GetExpenseReportsbyEmpId(int empId)
        {
            var repList = _da.GetExpenseReportsbyEmpId(empId);
            List<ExpenseReportEntity> reportList = new List<ExpenseReportEntity>();
            foreach (var rep in repList)
            {
                var report = new ExpenseReportEntity();
                report.AppliedDate = rep.AppliedDate.ToString("MM/dd/YYYY");
                report.ApprovedAmt = rep.ApprovedAmt;
                report.ApprovedDate = Convert.ToString(rep.ApprovalDate);
                if (rep.ApproverId != Convert.ToInt32(DBNull.Value))
                {
                    report.Approver = GetEmployeeByEmpID(Convert.ToInt32(rep.ApproverId));
                }
                report.ApproverId = Convert.ToInt32(rep.ApproverId);
                report.Employee = GetEmployeeByEmpID(Convert.ToInt32(rep.EmpId));
                report.ExpenseAmt = rep.ExpenseAmt;
                report.ExpTyCode = rep.ExpId;
                report.ExpType = GetExpenseTypeByCode(rep.ExpId);
                report.ReportId = rep.ReportId;
                report.Status = rep.Status;
                reportList.Add(report);
            }
            return reportList;
        }

        public List<ExpenseReportEntity> GetExpenseReportsbyApproverId(int approverId)
        {
            var empList = _da.GetExpenseReportsbyApproverId(approverId);
            List<ExpenseReportEntity> reportList = new List<ExpenseReportEntity>();
            foreach (var rep in reportList)
            {
                var report = new ExpenseReportEntity();
                report.AppliedDate = rep.AppliedDate;
                report.ApprovedAmt = rep.ApprovedAmt;
                report.ApprovedDate = rep.ApprovedDate;
                if (rep.ApproverId != Convert.ToInt32(DBNull.Value))
                {
                    report.Approver = GetEmployeeByEmpID(rep.ApproverId);
                }
                report.ApproverId = rep.ApproverId;
                report.Employee = GetEmployeeByEmpID(rep.EmpId);
                report.ExpenseAmt = rep.ExpenseAmt;
                report.ExpTyCode = rep.ExpTyCode;
                report.ExpType = GetExpenseTypeByCode(rep.ExpTyCode);
                report.ReportId = rep.ReportId;
                report.Status = rep.Status;
                reportList.Add(report);
            }
            return reportList;
        }

        public EmployeeEntity GetEmployeeByEmpID(int empId)
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

        public ExpenseReportEntity GtExpenseReportbyReportId(int reportId)
        {
            var report = _da.GetExpenseReportbyReportId(reportId);
            var repEntity = new ExpenseReportEntity();
            repEntity.ReportId = report.ReportId;
            repEntity.AppliedDate = report.AppliedDate.ToString("MM/dd/yyyy");
            repEntity.EmpId = Convert.ToInt32(report.EmpId);
            repEntity.Employee = GetEmployeeByEmpID(repEntity.EmpId);
            if (report.ApproverId != Convert.ToInt32(DBNull.Value))
                repEntity.Approver = GetEmployeeByEmpID(Convert.ToInt32(report.ApproverId));
            repEntity.ApproverId = Convert.ToInt32(report.ApproverId);
            repEntity.ExpenseAmt = report.ExpenseAmt;
            repEntity.ExpTyCode = report.ExpId;
            repEntity.ExpType = GetExpenseTypeByCode(report.ExpId);
            repEntity.Status = report.Status;
            repEntity.Comments = report.Comments;

            return repEntity;
        }
    }
}