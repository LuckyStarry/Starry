using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Starry.Services
{
    internal class CustomizeModuleLightly : CustomizeModule
    {
        internal CustomizeModuleLightly(Action action, Core.IService service)
            : base(service)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action = action;
        }

        private Action action;

        protected override void ModuleBusiness()
        {
            this.action.Invoke();
        }
    }
}
