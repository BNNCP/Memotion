using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Models;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Stock> Stocks {get;set;}
        public DbSet<Comment> Comments {get;set;}
    }
}