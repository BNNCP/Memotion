using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Data
{
    public class ApplicationDBContext:IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Stock> Stocks {get;set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x=>x.HasKey(p=>new {p.AppUserId, p.StockId}));
            builder.Entity<Portfolio>().HasOne(i=>i.AppUser).WithMany(u=>u.Portfolios).HasForeignKey(p=>p.AppUserId);
            builder.Entity<Portfolio>().HasOne(i=>i.Stock).WithMany(u=>u.Portfolios).HasForeignKey(p=>p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                }
            }; 
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}