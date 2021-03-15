namespace Tacx.Activities.Api.Contracts
{
    public class ActivityGetResponse
    {
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DistanceKm { get; set; }
        public double DurationSeconds { get; set; }
        public double AverageSpeedKmph { get; set; }
    }
}