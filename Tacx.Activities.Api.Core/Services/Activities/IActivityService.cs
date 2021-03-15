using System.Threading.Tasks;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Core.Services.Activities
{
    public interface IActivityService
    {
        Task<Activity> Create(CreateActivityArgs args);
        Task<Activity> GetById(string activityId);
        Task Delete(string activityId);
    }
}