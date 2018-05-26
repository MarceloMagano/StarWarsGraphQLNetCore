using GraphQL.Types;
using StarWarsGraphQL.Core.Models;

namespace StarWarsGraphQL.API.Models
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
