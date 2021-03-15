using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Tacx.Activities.Api.SmokeTests.Contexts;
using TechTalk.SpecFlow;

namespace Tacx.Activities.Api.SmokeTests.Steps
{
    [Binding]
    public class RestSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ActivityIdContext _activityIdContext;
        private readonly Dictionary<string, object> _requestProperties = new();
        private HttpResponseMessage _httpResponseMessage;

        public RestSteps(HttpClient httpClient, ActivityIdContext activityIdContext)
        {
            _httpClient = httpClient;
            _activityIdContext = activityIdContext;
        }
        
        [Given(@"there is a request with the following properties")]
        public void GivenThereIsARequestWithTheFollowingProperties(Table properties)
        {
            
            foreach (var row in properties.Rows)
            {
                var name = row[0];
                var typeName = row[1];
                var stringValue = row[2];
                var method = typeof(Convert).GetMethod($"To{typeName}", new [] {typeof(string)});
                var value = method.Invoke(null, new object[] {stringValue});
                _requestProperties[name] = value;
            }
        }

        [When(@"a request is sent to (\w+) (.*)")]
        public async Task WhenATheRequestIsSentToPostApiActivities(string httpMethod, string endpoint)
        {
            var requestJson = CreateRequestJson();
            _httpResponseMessage = await _httpClient.SendAsync(new HttpRequestMessage(new HttpMethod(httpMethod), endpoint)
            {
                Content = new StringContent(requestJson)
                {
                    Headers = { ContentType = MediaTypeHeaderValue.Parse("application/json")}
                }
            });
        }

        private string CreateRequestJson()
        {
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream))
            {
                writer.WriteStartObject();
                foreach (var (key, value) in _requestProperties)
                {
                    switch (value)
                    {
                        case string str:
                            writer.WriteString(key, str);
                            break;
                        case int intValue:
                            writer.WriteNumber(key, intValue);
                            break;
                        case double doubleValue:
                            writer.WriteNumber(key, doubleValue);
                            break;
                        default:
                            throw new InvalidOperationException(
                                $"property {key} with type {value.GetType()} and value {value} not supported");
                    }
                }
                writer.WriteEndObject();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        [Then(@"the response code is (.*)")]
        public void ThenTheResponseCodeIs201(string expectedCode)
        {
            _httpResponseMessage.StatusCode.Should().Be(Enum.Parse<HttpStatusCode>(expectedCode));
        }

        [Given(@"the request contains a generated ID in property '(.*)'")]
        public void GivenTheRequestContainsAGeneratedIdInPropertyActivityId(string property)
        {
            _activityIdContext.ActivityId = $"smoke-{Guid.NewGuid().ToString()}";
            _requestProperties[property] = _activityIdContext.ActivityId;
        }
    }
}