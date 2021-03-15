using System.ComponentModel.DataAnnotations;

namespace Tacx.Activities.Api.Contracts
{
    public class ActivityPostRequest
    {
        [Required]
        public string ActivityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double DistanceKm { get; set; }
        [Required]
        public double DurationSeconds { get; set; }
        [Required]
        public double AverageSpeedKmph { get; set; }
    }
}