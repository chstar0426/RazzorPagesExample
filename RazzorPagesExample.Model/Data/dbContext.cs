using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazzorPagesExample.Model
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public DbSet<RazzorPagesExample.Model.Product> Product { get; set; }
    }
}
