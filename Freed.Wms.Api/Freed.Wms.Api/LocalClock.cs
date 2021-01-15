using Microsoft.Extensions.Internal;
using System;

namespace Freed.Wms.Api
{
    internal class LocalClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.Now;
    }
}