using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public interface IModule : IEngine
    {
        ILoader Loader { get; }
        int MaxConcurrent { set; get; }
        string ModuleName { set; get; }
    }

    public interface IModule<TLoader> : IModule
        where TLoader : ILoader
    {
        new TLoader Loader { get; }
    }
}
