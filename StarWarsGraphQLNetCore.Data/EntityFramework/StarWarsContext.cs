using Microsoft.EntityFrameworkCore;
using StarWarsGraphQLNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsGraphQLNetCore.Data.EntityFramework
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
