using GraphQL.Types;
using StarWarsGraphQL.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsGraphQL.API.Models
{
    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery()
        {
            Field<DroidType>("hero", resolve: context => new Droid { Id = 1, Name = "R2-D2" });
        }
    }
}
