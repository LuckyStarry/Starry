using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.EventArgs
{
    public class EngineStateChangedEventArgs : ServiceEventArgs
    {
        public EngineStateChangedEventArgs(EngineState before, EngineState after)
        {
            this.Before = before;
            this.After = after;
        }

        public EngineState Before { private set; get; }
        public EngineState After { private set; get; }
    }
}
