using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsGraphQLNetCore.Core.Models
{
    public class StarWarsContext : DbContext
    {
        public StarWarsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Droid> Droids { get; set; }
    }
}
