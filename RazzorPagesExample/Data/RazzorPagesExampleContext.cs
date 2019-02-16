using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazzorPagesExample.Model;

namespace RazzorPagesExample.Models
{
    public class RazzorPagesExampleContext : DbContext
    {
        public RazzorPagesExampleContext (DbContextOptions<RazzorPagesExampleContext> options)
            : base(options)
        {
        }

        public DbSet<RazzorPagesExample.Model.Product> Product { get; set; }
    }
}
