using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public abstract class Handler<TModule> : Engine, IHandler, IHandler<TModule>
        where TModule : IModule
    {

#if DEBUG_CORE_DEBUGGER
        protected internal override string EngineID { get { return "INSTANCE " + (this as IHandler).Module.ModuleName + " " + this.UniqueID.ToString().Substring(0, 5).ToUpper(); } }
#endif

        public Handler(TModule module)
        {
            this.Module = module;
        }

        public TModule Module { private set; get; }
        IModule IHandler.Module { get { return this.Module; } }

        protected override bool HandleContinue()
        {
            return base.HandleContinue()
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
