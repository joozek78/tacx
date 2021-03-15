using System.Text.Json;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Core.Services.Activities
{
    public static class ActivityJsonMapper
    {
        public static byte[] ToJson(Activity activity)
        {
            return JsonSerializer.SerializeToUtf8Bytes(activity);
        }
    }
}