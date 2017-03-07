﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpenseReimbursment.Models.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ExpenseReimbursementContext : DbContext
    {
        public ExpenseReimbursementContext()
            : base("name=ExpenseReimbursementContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<ExpenseReport> ExpenseReports { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int InsertEmployee(string firstName, string middleName, string lastName, string roleId, string emailId, string contactNum, string gender)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var middleNameParameter = middleName != null ?
                new ObjectParameter("MiddleName", middleName) :
                new ObjectParameter("MiddleName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var roleIdParameter = roleId != null ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(string));
    
            var emailIdParameter = emailId != null ?
                new ObjectParameter("EmailId", emailId) :
                new ObjectParameter("EmailId", typeof(string));
    
            var contactNumParameter = contactNum != null ?
                new ObjectParameter("ContactNum", contactNum) :
                new ObjectParameter("ContactNum", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertEmployee", firstNameParameter, middleNameParameter, lastNameParameter, roleIdParameter, emailIdParameter, contactNumParameter, genderParameter);
        }
    
        public virtual int InsertExpenseReport(string expId, Nullable<int> empId, Nullable<decimal> expenseAmt, string comments, string status)
        {
            var expIdParameter = expId != null ?
                new ObjectParameter("ExpId", expId) :
                new ObjectParameter("ExpId", typeof(string));
    
            var empIdParameter = empId.HasValue ?
                new ObjectParameter("EmpId", empId) :
                new ObjectParameter("EmpId", typeof(int));
    
            var expenseAmtParameter = expenseAmt.HasValue ?
                new ObjectParameter("ExpenseAmt", expenseAmt) :
                new ObjectParameter("ExpenseAmt", typeof(decimal));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("Comments", comments) :
                new ObjectParameter("Comments", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertExpenseReport", expIdParameter, empIdParameter, expenseAmtParameter, commentsParameter, statusParameter);
        }
    
        public virtual int UpdateEmployee(Nullable<int> empId, string roleId, string emailId, string contactNum)
        {
            var empIdParameter = empId.HasValue ?
                new ObjectParameter("EmpId", empId) :
                new ObjectParameter("EmpId", typeof(int));
    
            var roleIdParameter = roleId != null ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(string));
    
            var emailIdParameter = emailId != null ?
                new ObjectParameter("EmailId", emailId) :
                new ObjectParameter("EmailId", typeof(string));
    
            var contactNumParameter = contactNum != null ?
                new ObjectParameter("ContactNum", contactNum) :
                new ObjectParameter("ContactNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateEmployee", empIdParameter, roleIdParameter, emailIdParameter, contactNumParameter);
        }
    
        public virtual int UpdateExpenseReport_Approver(Nullable<int> reportId, Nullable<int> approverId, Nullable<decimal> approvedAmt, string comments, string status)
        {
            var reportIdParameter = reportId.HasValue ?
                new ObjectParameter("ReportId", reportId) :
                new ObjectParameter("ReportId", typeof(int));
    
            var approverIdParameter = approverId.HasValue ?
                new ObjectParameter("ApproverId", approverId) :
                new ObjectParameter("ApproverId", typeof(int));
    
            var approvedAmtParameter = approvedAmt.HasValue ?
                new ObjectParameter("ApprovedAmt", approvedAmt) :
                new ObjectParameter("ApprovedAmt", typeof(decimal));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("Comments", comments) :
                new ObjectParameter("Comments", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateExpenseReport_Approver", reportIdParameter, approverIdParameter, approvedAmtParameter, commentsParameter, statusParameter);
        }
    
        public virtual int UpdateExpenseReport_Employee(Nullable<int> reportId, Nullable<decimal> expenseAmt, string comments)
        {
            var reportIdParameter = reportId.HasValue ?
                new ObjectParameter("ReportId", reportId) :
                new ObjectParameter("ReportId", typeof(int));
    
            var expenseAmtParameter = expenseAmt.HasValue ?
                new ObjectParameter("ExpenseAmt", expenseAmt) :
                new ObjectParameter("ExpenseAmt", typeof(decimal));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("Comments", comments) :
                new ObjectParameter("Comments", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateExpenseReport_Employee", reportIdParameter, expenseAmtParameter, commentsParameter);
        }
    
        public virtual int UpdateUsers(Nullable<int> empId, string password)
        {
            var empIdParameter = empId.HasValue ?
                new ObjectParameter("EmpId", empId) :
                new ObjectParameter("EmpId", typeof(int));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUsers", empIdParameter, passwordParameter);
        }
    
        //public virtual int uspRandChars(Nullable<int> len, Nullable<byte> min, Nullable<byte> range, string exclude, ObjectParameter output)
        //{
        //    var lenParameter = len.HasValue ?
        //        new ObjectParameter("len", len) :
        //        new ObjectParameter("len", typeof(int));
    
        //    var minParameter = min.HasValue ?
        //        new ObjectParameter("min", min) :
        //        new ObjectParameter("min", typeof(byte));
    
        //    var rangeParameter = range.HasValue ?
        //        new ObjectParameter("range", range) :
        //        new ObjectParameter("range", typeof(byte));
    
        //    var excludeParameter = exclude != null ?
        //        new ObjectParameter("exclude", exclude) :
        //        new ObjectParameter("exclude", typeof(string));
    
        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspRandChars", lenParameter, minParameter, rangeParameter, excludeParameter, output);
        //}
    }
}
