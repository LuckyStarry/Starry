using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Starry.Services
{
    public abstract class CustomizeModule : Core.Module
    {
        public CustomizeModule(Core.IService service)
            : base(service) { }

        protected sealed override Core.IHandler CreateModuleHandler()
        {
            return new CustomizeHandler(this, this.ModuleBusiness);
        }

        protected abstract void ModuleBusiness(CancellationToken cancellationToken);
    }
}
