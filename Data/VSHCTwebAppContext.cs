using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSHCTwebApp.Components.Models;

namespace VSHCTwebApp.Data
{
    public class VSHCTwebAppContext : DbContext
    {
        public VSHCTwebAppContext (DbContextOptions<VSHCTwebAppContext> options)
            : base(options)
        {
        }

        public DbSet<VSHCTwebApp.Components.Models.Note> Note { get; set; } = default!;
    }
}
