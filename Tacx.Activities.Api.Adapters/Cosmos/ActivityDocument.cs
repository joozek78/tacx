using Newtonsoft.Json;

namespace Tacx.Activities.Api.Adapters.Cosmos
{
    public class ActivityDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DistanceKm { get; set; }
        public double DurationSeconds { get; set; }
        public double AverageSpeedKmph { get; set; }
    }
}