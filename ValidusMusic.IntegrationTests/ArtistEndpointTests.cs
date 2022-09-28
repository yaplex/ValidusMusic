using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ValidusMusic.Api.Controllers;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;
using ValidusMusic.DataProvider;
using ValidusMusic.DataProvider.Repository;
using Xunit;

namespace ValidusMusic.IntegrationTests
{
    public class ArtistEndpointTests
    {
        [Fact]
        public async Task Get_Artist_Endpoint_Should_Return_ArtistInfo()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                });

            var client = application.CreateClient();

            var response = await client.GetAsync($"Artist/{5}");

            response.EnsureSuccessStatusCode();
            var resultJson = await response.Content.ReadAsStringAsync(CancellationToken.None);
            Assert.Equal("{\"id\":5,\"name\":\"Alex Shapovalov\",\"albums\":[]}", resultJson);
        }
    }
}