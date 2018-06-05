﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWarsGraphQLNetCore.API.Controllers;
using StarWarsGraphQLNetCore.API.Models;
using System.Threading.Tasks;
using Xunit;

namespace StarWarsGraphQLNetCore.Tests.Unit.Api.Controllers
{
    public class GraphQLControllerShould
    {
        private GraphQLController _graphqlController { get; set; }

        public GraphQLControllerShould()
        {
            // Given
            Mock<IDocumentExecuter> documentExecutor = new Mock<IDocumentExecuter>();
            documentExecutor.Setup(x => x.ExecuteAsync(It.IsAny<ExecutionOptions>())).Returns(Task.FromResult(new ExecutionResult()));
            Mock<ISchema> schema = new Mock<ISchema>();
            var logger = new Mock<ILogger<GraphQLController>>();
            _graphqlController = new GraphQLController(documentExecutor.Object, schema.Object, logger.Object);
        }

        [Fact]
        public void ReturnNotNullViewResult()
        {
            // When
            var result = _graphqlController.Index() as ViewResult;

            // Then
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ReturnNotNullExecutionResult()
        {
            // Given
            var query = new GraphQLQuery { Query = @"{ ""query"": ""query { hero { id name } }""" };

            // When
            var result = await _graphqlController.Post(query);

            // Then
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var executionResult = okObjectResult.Value;
            Assert.NotNull(executionResult);
        }
    }
}
