using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD.WEB.Models.ACCVM
{
    public class PasswordVM
    {
        public string Email { get; set; }
        public string UserID { get; set; }
        public string BaseCode { get; set; }
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]

        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Password too short")]
        public string Password { get; set; }

        public string Errormessage { get; set; }

    }
}
