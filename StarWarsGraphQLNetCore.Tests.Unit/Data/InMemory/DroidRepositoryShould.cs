using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StarWarsGraphQLNetCore.Data.InMemory;
using StarWarsGraphQLNetCore.Core.Models;

namespace StarWarsGraphQLNetCore.Tests.Unit.Data.InMemory
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;
        public DroidRepositoryShould()
        {
            _droidRepository = new DroidRepository();
        }

        [Fact]
        public async void ReturnR2D2DroidGivenIdOf1()
        {
            Droid droid = await _droidRepository.Get(1);

            Assert.NotNull(droid);
            Assert.Equal("R2-D2", droid.Name);
        }
    }
}
