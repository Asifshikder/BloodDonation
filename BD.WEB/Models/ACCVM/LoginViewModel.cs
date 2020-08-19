using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD.WEB.Models.ACCVM
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string IncorrectInput { get; set; }

        public string ReturnUrl { get; set; }
    }
}
