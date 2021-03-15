using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tacx.Activities.Api.Core.Adapters;
using Tacx.Activities.Api.Core.Services.Activities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tacx.Activities.Api.AcceptanceTests.Steps
{
    [Binding]
    public class ActivitySteps
    {
        private readonly Mock<IFileRepository> _fileRepository;
        private readonly IActivityService _activityService;

        public ActivitySteps(IServiceProvider serviceProvider, Mock<IFileRepository> fileRepository)
        {
            _fileRepository = fileRepository;
            _activityService = serviceProvider.GetRequiredService<IActivityService>();
        }
        
        [When(@"new activity is created with following properties")]
        public async Task GivenNewActivityIsCreatedWithFollowingProperties(Table table)
        {
            var createActivityArgs = table.CreateInstance<CreateActivityArgs>();
            await _activityService.Create(createActivityArgs);
        }

        [Then(@"a JSON file is uploaded with name '(.*)' and with content equivalent to")]
        public void ThenAjsonFileIsUploadedWithName1AndWithContentEquivalentTo(string expectedFileName, string expectedContent)
        {
            _fileRepository.Invocations.Should().HaveCount(1);
            _fileRepository.Invocations[0].Arguments[0].Should().Be(expectedFileName);
            var actualBytes = (byte[])_fileRepository.Invocations[0].Arguments[1];
            var actualContent = Encoding.UTF8.GetString(actualBytes);
            Normalize(actualContent).Should().Be(Normalize(expectedContent));
        }

        private string Normalize(string input)
        {
            using var document = JsonDocument.Parse(input);
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream))
            {
                document.WriteTo(writer);
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}