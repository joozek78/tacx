using Nelibur.ObjectMapper;
using Tacx.Activities.Api.Contracts;
using Tacx.Activities.Api.Core.Domain;
using Tacx.Activities.Api.Core.Services.Activities;

namespace Tacx.Activities.Api.Controllers
{
    public class ActivityRequestMapper
    {
        static ActivityRequestMapper()
        {
            TinyMapper.Bind<ActivityPostRequest, CreateActivityArgs>();
            TinyMapper.Bind<Activity, ActivityGetResponse>();
        }
        
        public static CreateActivityArgs Map(ActivityPostRequest request)
        {
            return TinyMapper.Map<CreateActivityArgs>(request);
        }

        public static ActivityGetResponse Map(Activity activity)
        {
            return TinyMapper.Map<ActivityGetResponse>(activity);
        }
    }
}