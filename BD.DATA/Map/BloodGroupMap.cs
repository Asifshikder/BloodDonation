using BD.DATA.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.DATA.Map
{
    public class BloodGroupMap
    {
        public BloodGroupMap(EntityTypeBuilder<BloodGroup> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}
