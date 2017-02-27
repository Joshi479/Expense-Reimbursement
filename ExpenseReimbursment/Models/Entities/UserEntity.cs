using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseReimbursment.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ExpenseReimbursment.Models.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Password { get; set; }
 
    }
}