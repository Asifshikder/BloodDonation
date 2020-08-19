using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.REPO
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=SQL5050.site4now.net;Initial Catalog=DB_A6625F_bddb;User Id=DB_A6625F_bddb_admin;Password=Asif0168");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
