﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using StarWarsGraphQL.API.Models;
using System.Threading.Tasks;

namespace StarWarsGraphQL.API.Controllers
{
    //[Produces("application/json")]
    [Route("graphQL")]
    public class GraphQLController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            Schema schema = new Schema { Query = new StarWarsQuery() };

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