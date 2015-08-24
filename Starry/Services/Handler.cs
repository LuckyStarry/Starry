using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services
{
    public class Handler<TModule> : Core.Engine, Core.IHandler, Core.IHandler<TModule>
        where TModule : Core.IModule
    {
        public Handler(TModule module)
        {
            this.Module = module;
        }

        public TModule Module { private set; get; }
        Core.IModule Core.IHandler.Module { get { return this.Module; } }

        protected override void DoHandle(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class Handler : Handler<Core.IModule>
    {
        public Handler(Core.IModule module)
            : base(module)
        {

        }
    }
}
