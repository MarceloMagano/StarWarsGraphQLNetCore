using StarWarsGraphQLNetCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using StarWarsGraphQLNetCore.Data.EntityFramework;
using StarWarsGraphQLNetCore.Data.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;

namespace StarWarsGraphQLNetCore.Tests.Unit.Data.EntityFramework.Repositories
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;
        public DroidRepositoryShould()
        {
            // Given
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            var options = new DbContextOptionsBuilder<StarWarsContext>()
                .UseInMemoryDatabase(databaseName: "StarWars")
                .Options;
            var dbLogger = new Mock<ILogger<StarWarsContext>>();
            using (StarWarsContext context = new StarWarsContext(options, dbLogger.Object))
            {
                context.Droids.Add(new Droid { Id = 1, Name = "R2-D2" });
                context.SaveChanges();
            }
            StarWarsContext starWarsContext = new StarWarsContext(options, dbLogger.Object);
            var repoLogger = new Mock<ILogger<DroidRepository>>();
            _droidRepository = new DroidRepository(starWarsContext, repoLogger.Object);
        }

        [Fact]
        public async void ReturnR2D2DroidGivenIdOf1()
        {
            //When
            Droid droid = await _droidRepository.Get(1);
            //Then
            Assert.NotNull(droid);
            Assert.Equal("R2-D2", droid.Name);
        }
    }
}
