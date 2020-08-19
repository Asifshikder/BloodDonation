using System;

namespace BD.DATA
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Status { get; set; }
    }
}
