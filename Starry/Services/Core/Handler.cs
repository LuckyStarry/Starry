using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public abstract class Handler<TModule> : Engine, IHandler, IHandler<TModule>
        where TModule : IModule
    {
        public Handler(TModule module)
        {
            this.Module = module;
        }

        public TModule Module { private set; get; }
        IModule IHandler.Module { get { return this.Module; } }

        protected override bool WorkContinue()
        {
            return base.WorkContinue()
                && this.Module != null
                && this.Module.State == EngineState.Running
                && this.Module.IsAlive;
        }
    }

    public abstract class Handler : Handler<IModule>
    {
        public Handler(IModule module)
            : base(module)
        {

        }
    }
}
