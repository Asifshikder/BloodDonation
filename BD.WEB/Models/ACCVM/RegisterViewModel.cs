using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD.WEB.Models.ACCVM
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords did not match")]
        [Display(Name = "Confirm password")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter valid email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string COntactNumber { get; set; }
        public int CityID { get; set; }
        public int BloodGroupID { get; set; }
        public string ReturnUrl { get; set; }
    }
}
