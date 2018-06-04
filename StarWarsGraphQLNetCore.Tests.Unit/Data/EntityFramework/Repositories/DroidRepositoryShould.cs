using StarWarsGraphQLNetCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using StarWarsGraphQLNetCore.Data.EntityFramework;
using StarWarsGraphQLNetCore.Data.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Droids.Add(new Droid { Id = 1, Name = "R2-D2" });
                context.SaveChanges();
            }
            StarWarsContext starWarsContext = new StarWarsContext(options);
            _droidRepository = new DroidRepository(starWarsContext);
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
