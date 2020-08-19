using BD.DATA.Entity;
using BD.DATA.Map;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BD.REPO
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new BloodGroupMap(modelBuilder.Entity<BloodGroup>());
            new CityMap(modelBuilder.Entity<City>());
            new PostMap(modelBuilder.Entity<Post>());
         
        }
    }
}
