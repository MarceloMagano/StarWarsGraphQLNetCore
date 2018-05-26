using GraphQL.Types;
using StarWarsGraphQLNetCore.Core.Models;

namespace StarWarsGraphQLNetCore.API.Models
{
    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Field(x => x.Id).Description("The Id of the Driod");
            Field(x => x.Name, nullable: true).Description("The name of the Driod");
        }
    }
}
