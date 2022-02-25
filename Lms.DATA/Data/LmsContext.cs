#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lms.CORE.Entities;

namespace Lms.DATA.Data
{
    public class LmsContext : DbContext
    {
        public LmsContext (DbContextOptions<LmsContext> options)
            : base(options)
        {
        }

        public DbSet<Lms.CORE.Entities.Course> Course { get; set; }

        public DbSet<Lms.CORE.Entities.Module> Module { get; set; }
    }
}
