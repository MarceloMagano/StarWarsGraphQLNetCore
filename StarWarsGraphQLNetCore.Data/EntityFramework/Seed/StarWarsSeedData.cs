using Microsoft.Extensions.Logging;
using StarWarsGraphQLNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarWarsGraphQLNetCore.Data.EntityFramework.Seed
{
    public static class StarWarsSeedData
    {
        public static void EnsureSeedData(this StarWarsContext db)
        {
            db._logger.LogInformation("Seeding database");
            if (!db.Droids.Any())
            {
                db._logger.LogInformation("Seeding droids");
                var droid = new Droid
                {
                    Name = "R2-D2"
                };
                db.Droids.Add(droid);
                db.SaveChanges();
            }

        }
    }
}
