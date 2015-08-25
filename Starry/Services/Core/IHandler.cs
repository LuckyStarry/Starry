using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public interface IHandler : IEngine
    {
        IModule Module { get; }
    }

    public interface IHandler<TModule> : IHandler
        where TModule : IModule
    {
        new TModule Module { get; }
    }
}
