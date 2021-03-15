using System;

namespace Tacx.Activities.Api.Core.Adapters
{
    public class Clock: IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}