using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using StarWarsGraphQLNetCore.API.Models;
using StarWarsGraphQLNetCore.Data.InMemory;
using System.Threading.Tasks;

namespace StarWarsGraphQLNetCore.API.Controllers
{
    //[Produces("application/json")]
    [Route("graphQL")]
    public class GraphQLController : Controller
    {
        private StarWarsQuery _starWarsQuery { get; set; }

        public GraphQLController(StarWarsQuery starWarsQuery)
        {
            _starWarsQuery = starWarsQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            Schema schema = new Schema { Query = _starWarsQuery };

            ExecutionResult result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}