using Nelibur.ObjectMapper;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Adapters.Cosmos
{
    public static class ActivityDocumentMapper
    {
        static ActivityDocumentMapper()
        {
            TinyMapper.Bind<ActivityDocument, Activity>();
            TinyMapper.Bind<Activity, ActivityDocument>();
        }

        public static Activity Map(ActivityDocument activityDocument) => TinyMapper.Map<Activity>(activityDocument);

        public static ActivityDocument Map(Activity activity)
        {
            var activityDocument = TinyMapper.Map<ActivityDocument>(activity);
            activityDocument.Id = activity.ActivityId;
            return activityDocument;
        }
    }
}