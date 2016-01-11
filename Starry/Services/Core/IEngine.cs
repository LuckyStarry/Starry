using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public interface IEngine : IDisposable
    {
        Guid UniqueID { get; }
        EngineState State { get; }

        void Start();
        void Stop();
        bool IsAlive { get; }
    }
}
