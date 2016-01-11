using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public interface IModule : IEngine
    {
        IService Service { get; }
        int MaxConcurrent { set; get; }
        int Concurrent { get; }
        string ModuleName { set; get; }
    }

    public interface IModule<TService> : IModule
        where TService : IService
    {
        new TService Service { get; }
    }
}
