using Nelibur.ObjectMapper;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Core.Services.Activities
{
    public static class ActivityMapper
    {
        static ActivityMapper()
        {
            TinyMapper.Bind<CreateActivityArgs, Activity>();

        }
        public static Activity Map(CreateActivityArgs args) => TinyMapper.Map<Activity>(args);
    }
}