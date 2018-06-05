﻿using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using StarWarsGraphQLNetCore.API;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using Xunit;


namespace StarWarsGraphQLNetCore.Tests.Integration.Api.Controllers
{
    public class GraphQLControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GraphQLControllerShould()
        {
            // setting up the server: using Environment "Test" and using the StarWarsGraphQLNetCore.API Startup
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Test").UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ReturnR2D2Droid()
        {
            // Given
            var query = @"{
                ""query"": ""query { hero { id name } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/graphql", content);

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("R2-D2", responseString);
        }
    }
}
