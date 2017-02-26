using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ExpenseReimbursment.Models.Entities;
using ExpenseReimbursment.Models.Authorization;

namespace ExpenseReimbursment.Models.EntityFramework
{
    public class ExpenseReimbusementContext: DbContext 
    {
        public DbSet<Employee> EmployeeDetails { get; set; }
        public DbSet<Role> EmpRoles { get; set; }
        public DbSet<ExpenseReport> ExpReports {get; set;}
        public DbSet<ExpenseType> ExpTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}