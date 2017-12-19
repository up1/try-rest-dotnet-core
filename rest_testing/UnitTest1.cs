using System;
using Xunit;

using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Newtonsoft.Json.Linq;
using restapi.Models;

namespace restapi.test
{
    public class UnitTest1
    {

        private readonly HttpClient client;
        private readonly TestServer server;

        public UnitTest1() {
            server = new TestServer(
                new WebHostBuilder().UseStartup<Startup>()
            );
            client = server.CreateClient();
        }

        [Fact]
        public async void size_of_result_should_be_2()
        {
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            dynamic collection = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public async void first_user() {
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            dynamic results = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.Equal(1, results[0].ToObject<User>().id);
            Assert.Equal("User 1", results[0].ToObject<User>().name);
        }

        [Fact]
        public async void second_user()
        {
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            dynamic results = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.Equal(2, results[1].ToObject<User>().id);
            Assert.Equal("User 2", results[1].ToObject<User>().name);
        }
    }
}
