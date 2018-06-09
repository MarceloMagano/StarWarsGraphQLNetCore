using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StarWarsGraphQLNetCore.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StarWarsGraphQLNetCore.Tests.Integration.Data.EntityFramework
{
    public class StarWarsContextShould
    {
        [Fact]
        public async void ReturnR2D2Droid()
        {
            // Given
            var options = new DbContextOptionsBuilder<StarWarsContext>().UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StarWars;Integrated Security=SSPI;integrated security=true;MultipleActiveResultSets=True;").Options;
            var dbLogger = new Mock<ILogger<StarWarsContext>>();

            using (var db = new StarWarsContext(options, dbLogger.Object))
            {
                // When
                var r2d2 = await db.Droids
                    .Include("CharacterEpisodes.Episode")
                    .Include("CharacterFriends.Friend")
                    .FirstOrDefaultAsync(d => d.Id == 2001);

                // Then
                Assert.NotNull(r2d2);
                Assert.Equal("R2-D2", r2d2.Name);
                Assert.Equal("Astromech", r2d2.PrimaryFunction);
                var episodes = r2d2.CharacterEpisodes.Select(e => e.Episode.Title);
                Assert.Equal(new string[] { "NEWHOPE", "EMPIRE", "JEDI" }, episodes);
                var friends = r2d2.CharacterFriends.Select(e => e.Friend.Name);
                Assert.Equal(new string[] { "Luke Skywalker", "Han Solo", "Leia Organa" }, friends);
            }
        }
    }
}
