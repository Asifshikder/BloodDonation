using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD.WEB.Models
{
    public class PostVM
    {
        public int ID { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? AddDate { get; set; }
    }
}
