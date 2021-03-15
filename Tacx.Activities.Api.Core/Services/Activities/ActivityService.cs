using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tacx.Activities.Api.Core.Adapters;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Core.Services.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IFileRepository _fileRepository;

        public ActivityService(IActivityRepository activityRepository, IFileRepository fileRepository)
        {
            _activityRepository = activityRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<Activity> Create(CreateActivityArgs args)
        {
            var activity = ActivityMapper.Map(args);
            // TACX order of those operations is important. They are not atomic, so if the second fails, the first one won't be rolled back
            await _activityRepository.Create(activity);
            await _fileRepository.Upload(activity.ActivityId, ActivityJsonMapper.ToJson(activity));
            return activity;
        }

        public Task<Activity> GetById(string activityId)
        {
            return _activityRepository.GetById(activityId);
        }

        public async Task Delete(string activityId)
        {
            await _activityRepository.Delete(activityId);
            await _fileRepository.Delete(activityId);
        }
    }
}