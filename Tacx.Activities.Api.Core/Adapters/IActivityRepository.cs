using System.Threading.Tasks;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Core.Adapters
{
    public interface IActivityRepository
    {
        Task Create(Activity activity);
        Task Delete(string activityId);
        Task<Activity> GetById(string activityId);
    }
}