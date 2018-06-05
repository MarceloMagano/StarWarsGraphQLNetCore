using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWarsGraphQLNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsGraphQLNetCore.Data.EntityFramework
{
    public class StarWarsContext : DbContext
    {
        public readonly ILogger _logger;

        public StarWarsContext(DbContextOptions options, ILogger<StarWarsContext> logger) : base(options)
        {
            _logger = logger;
            Database.EnsureCreated();
        }

        public DbSet<Droid> Droids { get; set; }
    }
}
