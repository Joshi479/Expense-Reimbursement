//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ExpenseReport
    {
        public int ReportId { get; set; }
        public string ExpId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<decimal> ExpenseAmt { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public Nullable<decimal> ApprovedAmt { get; set; }
        public string Comments { get; set; }
        public System.DateTime AppliedDate { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public string Status { get; set; }
    
        public virtual Employee EmpApprover { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
