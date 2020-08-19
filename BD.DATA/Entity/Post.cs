using System;
using System.Collections.Generic;
using System.Text;

namespace BD.DATA.Entity
{
    public class Post : BaseEntity
    {
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? BloodGroupID { get; set; }
    }
}
