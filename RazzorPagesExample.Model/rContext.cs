using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazzorPagesExample.Model
{
    public class rContext : DbContext
    {
        public rContext(DbContextOptions<rContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
