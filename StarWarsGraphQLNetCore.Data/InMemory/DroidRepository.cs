using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsGraphQLNetCore.Core.Data;
using StarWarsGraphQLNetCore.Core.Models;

namespace StarWarsGraphQLNetCore.Data.InMemory
{
    public class DroidRepository : IDroidRepository
    {

        private List<Droid> _droids = new List<Droid>
        {
            new Droid{Id = 1, Name="R2-D2"}
        };

        public Task<Droid> Get(int id)
        {
            return Task.FromResult(_droids.FirstOrDefault(d => d.Id == id));
        }
    }
}
