using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Witchblades.Backend.Api.Tests.Base;
using Witchblades.Backend.Data;
using Xunit;

namespace Witchblades.Backend.Api.Tests
{
    public class ArtistsTests
    {
        private readonly CustomWebApplicationFactory _factory;

        public ArtistsTests()
        {
            _factory = new CustomWebApplicationFactory();
        }

        #region Test_GetArtistsWithPagination_WhenDatabaseEmpty
        [Fact]
        public async Task Test_GetArtistsWithPagination_WhenDatabaseEmpty()
        {
            // Arrange
            var client = GetClientOfEmptyService();

            // Act
            var result = await client.GetAsync("api/v1/Artists");
            string json = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            Assert.Equal("", json);
        }
        #endregion

        #region Test_GetArtistsWithPagination
        [Fact]
        public async Task Test_GetArtistsWithPagination()
        {
            // Arrange
            var client = GetClientOfFilledService();

            // Act
            var result = await client.GetAsync("api/v1/Artists");
            string json = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotEqual("", json);
        } 
        #endregion

        #region Private methods
        private HttpClient GetClientOfEmptyService()
        {
            return _factory.CreateClient();
        }

        private HttpClient GetClientOfFilledService()
        {
            return _factory.WithWebHostBuilder(builder => builder.ConfigureServices(t =>
            {
                var sp = t.BuildServiceProvider();
                var context = sp.GetRequiredService<WitchbladesContext>();
                var initializer = new DatabaseInitializer();
                initializer.SeedDatabase(context);
            })).CreateClient();
        }
        #endregion
    }
}