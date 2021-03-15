namespace Tacx.Activities.Api.Core.Services.Activities
{
    public class CreateActivityArgs
    {
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DistanceKm { get; set; }
        public double DurationSeconds { get; set; }
        public double AverageSpeedKmph { get; set; }
    }
}