using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;

namespace BD.DATA.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string  ContactNumber { get; set; }
        public string  Address { get; set; }
        public int?  CityID { get; set; }
        public int? BloodGroupID { get; set; }

    }
}
