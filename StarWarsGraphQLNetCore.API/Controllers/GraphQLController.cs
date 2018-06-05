using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarWarsGraphQLNetCore.API.Models;
using StarWarsGraphQLNetCore.Data.InMemory;
using System.Threading.Tasks;

namespace StarWarsGraphQLNetCore.API.Controllers
{
    //[Produces("application/json")]
    [Route("graphQL")]
    public class GraphQLController : Controller
    {
        private ISchema _schema { get; set; }
        private IDocumentExecuter _documentExecuter { get; set; }
        private readonly ILogger _logger;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema, ILogger<GraphQLController> logger)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Got request for GraphiQL. Sending GUI back");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            ExecutionOptions executionOptions = new ExecutionOptions { Schema = _schema, Query = query.Query };
            ExecutionResult result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                _logger.LogError("GraphQL errors: {0}", result.Errors);
                return BadRequest();
            }

            _logger.LogDebug("GraphQL execution result: {result}", JsonConvert.SerializeObject(result.Data));
            return Ok(result);
        }
    }
}