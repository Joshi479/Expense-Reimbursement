using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseReimbursment.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Employee Id")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string Role { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string PhnNumber { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        public string Message { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Password must be at least 8 characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match. Please try again")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Employee Id")]
        public int? EmpId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ReportViewModel
    {
        public int? EmpId { get; set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Required]
        [Display(Name = "Expense Amount")]
        public decimal? ExpenseAmt { get; set; }

        [Required]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        public string Message { get; set; }
    }
}
