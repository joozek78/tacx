using System;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tacx.Activities.Api.Core;
using Tacx.Activities.Api.Core.Adapters;
using TechTalk.SpecFlow;

namespace Tacx.Activities.Api.AcceptanceTests.Steps
{
    [Binding]
    public class ClockSteps
    {
        private readonly Mock<IClock> _clock;

        public ClockSteps(Mock<IClock> clock)
        {
            _clock = clock;
        }

        [Given(@"the current time is (.*)")]
        public void GivenTheCurrentTimeIs(string currentTime)
        {
            _clock.SetupGet(clock => clock.Now)
                .Returns(DateTimeOffset.Parse(currentTime));
        }
    }
}