using GraphQL.Types;
using StarWarsGraphQLNetCore.Core.Data;
using StarWarsGraphQLNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsGraphQLNetCore.API.Models
{
    public class StarWarsQuery : ObjectGraphType
    {
        private IDroidRepository _droidRepository { get; set; }

        public StarWarsQuery(IDroidRepository _droidRepository)
        {
            Field<DroidType>("hero", resolve: context => _droidRepository.Get(1));
        }
    }
}
