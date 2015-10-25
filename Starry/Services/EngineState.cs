using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services
{
    public enum EngineState
    {
        Unknown = 0,
        Standby,
        Standup,
        Running,
        Stopping,
        Stopped,
        Disposing,
        Disposed
    }
}
