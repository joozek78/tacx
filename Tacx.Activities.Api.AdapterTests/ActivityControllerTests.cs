using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Tacx.Activities.Api.Core.Services.Activities;

namespace Tacx.Activities.Api.AdapterTests
{
    public class ActivityControllerTests: IDisposable
    {
        private Mock<IActivityService> _activityService;
        private WebApplicationFactory<Startup> _webApplicationFactory;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _activityService = new Mock<IActivityService>();
            _webApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => builder
                    .ConfigureTestServices(services =>
                    {
                        services.AddSingleton(_activityService.Object);
                    }));
            _httpClient = _webApplicationFactory.CreateClient();
        }
        
        [Test]
        public async Task ShouldAcceptPostRequest()
        {
            var requestBody = new
            {
                ActivityId = "1",
                Name = "cycling",
                Description = "Just me and my bike",
                DistanceKm = 10,
                DurationSeconds = 12,
                AverageSpeedKmph = 0.2,
            };
            
            var httpResponseMessage = await RequestWithBody(HttpMethod.Post, "/api/activities", requestBody);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var expectedArgs = new CreateActivityArgs()
            {
                ActivityId = "1",
                Name = "cycling",
                Description = "Just me and my bike",
                DistanceKm = 10,
                DurationSeconds = 12,
                AverageSpeedKmph = 0.2,
            };
            VerifyCreateOrUpdateCalled(expectedArgs);
        }

        [Test]
        public async Task ShouldNotAcceptInvalidRequest()
        {
            var requestBody = new
            {
            };
            
            var httpResponseMessage = await RequestWithBody(HttpMethod.Post, "/api/activities", requestBody);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private void VerifyCreateOrUpdateCalled(CreateActivityArgs expectedArgs)
        {
            _activityService.Invocations.Should().HaveCount(1);
            _activityService.Invocations[0].Arguments[0].Should().BeEquivalentTo(expectedArgs);
        }

        private async Task<HttpResponseMessage> RequestWithBody(HttpMethod httpMethod, string uri, object inputValue)
        {
            return await _httpClient.SendAsync(new HttpRequestMessage(httpMethod, uri)
            {
                Content = JsonContent.Create(inputValue)
            });
        }

        public void Dispose()
        {
            _webApplicationFactory?.Dispose();
        }
    }
}