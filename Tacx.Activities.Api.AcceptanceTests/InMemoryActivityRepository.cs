using System.Collections.Generic;
using System.Threading.Tasks;
using Tacx.Activities.Api.Core;
using Tacx.Activities.Api.Core.Adapters;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.AcceptanceTests
{
    public class InMemoryActivityRepository : IActivityRepository
    {
        private readonly Dictionary<string, Activity> _documents = new();
        public IReadOnlyDictionary<string, Activity> Documents => _documents;
        public int NextId { get; set; } = 1;

        public Task Create(Activity activity)
        {
            _documents[activity.ActivityId] = activity;
            return Task.CompletedTask;
        }

        public Task Delete(string activityId)
        {
            _documents.Remove(activityId);
            return  Task.CompletedTask;
        }

        public Task<Activity> GetById(string activityId)
        {
            return Task.FromResult(_documents[activityId]);
        }

        public void Reset()
        {
            _documents.Clear();
        }
    }
}