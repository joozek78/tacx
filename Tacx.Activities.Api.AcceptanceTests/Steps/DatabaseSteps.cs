using FluentAssertions;
using Tacx.Activities.Api.Core;
using Tacx.Activities.Api.Core.Domain;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tacx.Activities.Api.AcceptanceTests.Steps
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly InMemoryActivityRepository _activityRepository;

        public DatabaseSteps(InMemoryActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        
        [Given(@"there are no activities in the database")]
        public void GivenThereAreNoActivitiesInTheDatabase()
        {
            _activityRepository.Reset();
        }
        
        [Then(@"there are following activities in the database")]
        public void ThenThereAreFollowingActivitiesInTheDatabase(Table table)
        {
            var expectedItems = table.CreateSet<Activity>();
            _activityRepository.Documents.Values.Should().BeEquivalentTo(expectedItems);
        }
    }
}