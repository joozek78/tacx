using System;

namespace Tacx.Activities.Api.Core.Adapters
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}