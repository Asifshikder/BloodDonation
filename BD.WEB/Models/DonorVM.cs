using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD.WEB.Models
{
    public class DonorVM
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
        public string Contact2 { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public int? BloodGroupID { get; set; }
    }
}
