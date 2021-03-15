using System;

namespace Tacx.Activities.Api.Core.Domain
{
    public class Activity
    {
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DistanceKm { get; set; }
        public double DurationSeconds { get; set; }
        public double AverageSpeedKmph { get; set; }

        protected bool Equals(Activity other)
        {
            return ActivityId == other.ActivityId && Name == other.Name && Description == other.Description && DistanceKm.Equals(other.DistanceKm) && DurationSeconds.Equals(other.DurationSeconds) && AverageSpeedKmph.Equals(other.AverageSpeedKmph);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Activity) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ActivityId, Name, Description, DistanceKm, DurationSeconds, AverageSpeedKmph);
        }

        public override string ToString()
        {
            return $"{nameof(ActivityId)}: {ActivityId}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(DistanceKm)}: {DistanceKm}, {nameof(DurationSeconds)}: {DurationSeconds}, {nameof(AverageSpeedKmph)}: {AverageSpeedKmph}";
        }
    }
}